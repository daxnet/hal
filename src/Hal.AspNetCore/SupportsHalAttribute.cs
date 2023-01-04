// ---------------------------------------------------------------------------
//  _    _          _
// | |  | |   /\   | |
// | |__| |  /  \  | |
// |  __  | / /\ \ | |
// | |  | |/ ____ \| |____
// |_|  |_/_/    \_\______|
//
// A C#/.NET Core implementation of Hypertext Application Language
// http://stateless.co/hal_specification.html
//
// MIT License
//
// Copyright (c) 2017 Sunny Chen
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// ---------------------------------------------------------------------------
using Hal.Builders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Dynamic;
using System.Net;
using System.Reflection;
using System.Text;

namespace Hal.AspNetCore
{
    /// <summary>
    /// Represents the filter attribute that applies HAL document on the response JSON.
    /// </summary>
    public class SupportsHalAttribute : ResultFilterAttribute
    {
        #region Private Fields

        private static readonly JsonSerializerSettings _jsonSerializerSettings = new()
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.None,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        };

        private readonly ILogger<SupportsHalAttribute> _logger;
        private readonly SupportsHalOptions _options;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <c>SupportsHalAttribute</c> class.
        /// </summary>
        /// <param name="options">The options that is used for configuring the HAL support.</param>
        public SupportsHalAttribute(IOptions<SupportsHalOptions> options, ILogger<SupportsHalAttribute> logger) =>
            (Order, _options, _logger) = (2, options.Value, logger);

        #endregion Public Constructors

        #region Public Methods

        /// <inheritdoc/>
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (!_options.Enabled)
            {
                if (IsPagedResult(context.Result, out var pagedResult, out _))
                {
                    var dataCollectionName = MakeCamelCase(controllerActionDescriptor?.ControllerName ?? "data");
                    ExpandoObject resultObj = new();
                    resultObj.TryAdd("page", pagedResult!.PageNumber);
                    resultObj.TryAdd("size", pagedResult!.PageSize);
                    resultObj.TryAdd("totalPages", pagedResult!.TotalPages);
                    resultObj.TryAdd("totalCount", pagedResult!.TotalRecords);
                    resultObj.TryAdd(dataCollectionName, pagedResult!);
                    context.Result = new OkObjectResult(resultObj);
                }

                await base.OnResultExecutionAsync(context, next);
            }
            else
            {
                var originalStatusCode = (int)HttpStatusCode.OK;
                var selfLinkItem = context.HttpContext.Request.GetEncodedUrl();
                Resource resource;
                if (IsPagedResult(context.Result, out var pagedResult, out var entityType))
                {
                    var state = pagedResult!;
                    var size = state.PageSize;
                    var number = state.PageNumber;
                    var totalElements = state.TotalRecords;
                    var totalPages = state.TotalPages;

                    var linkItemBuilder = new ResourceBuilder()
                        .WithState(new { page = new { number, size, totalElements, totalPages } })
                        .AddSelfLink().WithLinkItem(selfLinkItem);

                    var firstLinkItem = GenerateLink(context, new Dictionary<string, StringValues> { { "page", 1.ToString() } });
                    linkItemBuilder = linkItemBuilder.AddLink("first").WithLinkItem(firstLinkItem);

                    var lastLinkItem = GenerateLink(context, new Dictionary<string, StringValues> { { "page", totalPages.ToString() } });
                    linkItemBuilder = linkItemBuilder.AddLink("last").WithLinkItem(lastLinkItem);
                    if (number > 1 && number <= totalPages)
                    {
                        string? prevLinkItem = GenerateLink(context, new Dictionary<string, StringValues> { { "page", (number - 1).ToString() } });
                        linkItemBuilder = linkItemBuilder.AddLink("prev").WithLinkItem(prevLinkItem);
                    }

                    if (number >= 1 && number < totalPages)
                    {
                        string? nextLinkItem = GenerateLink(context, new Dictionary<string, StringValues> { { "page", (number + 1).ToString() } });
                        linkItemBuilder = linkItemBuilder.AddLink("next").WithLinkItem(nextLinkItem);
                    }

                    var embeddedResourceName = MakeCamelCase(controllerActionDescriptor?.ControllerName ?? "data");
                    IBuilder resourceBuilder;
                    if (HasIdProperty(entityType))
                    {
                        var controllerTypeInfo = controllerActionDescriptor!.ControllerTypeInfo;
                        var controllerRouteAttributeType = controllerTypeInfo.CustomAttributes?.FirstOrDefault(x => x.AttributeType == typeof(RouteAttribute));
                        var controllerRouteName = controllerRouteAttributeType?.ConstructorArguments[0].Value?.ToString() ?? controllerActionDescriptor?.ControllerName;
                        if (!string.IsNullOrEmpty(controllerRouteName) && controllerRouteName.Contains("[controller]"))
                        {
                            controllerRouteName = controllerRouteName.Replace("[controller]", controllerActionDescriptor?.ControllerName);
                        }

                        var newState = ConvertListToHalResourceObjects(context, state);

                        resourceBuilder = linkItemBuilder.AddEmbedded(embeddedResourceName)
                            .Resource(new ResourceBuilder().WithState(newState));
                    }
                    else
                    {
                        resourceBuilder = linkItemBuilder.AddEmbedded(embeddedResourceName)
                            .Resource(new ResourceBuilder().WithState(state));
                    }
                    resource = resourceBuilder.Build();
                }
                else if (context.Result is ObjectResult objectResult && objectResult.Value != null)
                {
                    originalStatusCode = objectResult.StatusCode ?? (int)HttpStatusCode.OK;
                    IBuilder? resourceBuilder;
                    if (objectResult.Value is IEnumerable enumerableResult)
                    {
                        var newState = ConvertListToHalResourceObjects(context, enumerableResult);
                        resourceBuilder = new ResourceBuilder()
                            .WithState(new { count = enumerableResult.Cast<object>().Count() })
                            .AddSelfLink().WithLinkItem(selfLinkItem)
                            .AddEmbedded(MakeCamelCase(controllerActionDescriptor?.ControllerName ?? "data"))
                            .Resource(new ResourceBuilder().WithState(newState));
                    }
                    else
                    {
                        resourceBuilder = new ResourceBuilder()
                            .WithState(objectResult.Value)
                            .AddSelfLink().WithLinkItem(selfLinkItem);
                    }

                    resource = resourceBuilder.Build();
                }
                else
                {
                    await base.OnResultExecutionAsync(context, next);
                    return;
                }

                var json = resource.ToString(_jsonSerializerSettings);
                var bytes = Encoding.UTF8.GetBytes(json);
                context.HttpContext.Response.ContentLength = bytes.Length;
                context.HttpContext.Response.ContentType = "application/hal+json; charset=utf-8";
                using var ms = new MemoryStream(bytes);
                context.HttpContext.Response.StatusCode = originalStatusCode;
                await ms.CopyToAsync(context.HttpContext.Response.Body);
                return;
            }
        }

        #endregion Public Methods

        #region Private Methods

        private static bool IsPagedResult(IActionResult result, out IPagedResult? pagedResult, out Type? entityType)
        {
            var isPagedResult = result is OkObjectResult okObj &&
                okObj.Value != null &&
                okObj.Value.GetType().IsGenericType &&
                okObj.Value.GetType().GetGenericTypeDefinition() == typeof(PagedResult<>);
            if (isPagedResult)
            {
                entityType = ((OkObjectResult)result).Value?.GetType().GetGenericArguments().First();
                pagedResult = (result as OkObjectResult)?.Value as IPagedResult;
            }
            else
            {
                entityType = null;
                pagedResult = null;
            }

            return isPagedResult;
        }

        private static string MakeCamelCase(string src) => $"{src[0].ToString().ToLower()}{src[1..]}";

        private IEnumerable<JObject> ConvertListToHalResourceObjects(ResultExecutingContext context, IEnumerable state)
        {
            var newState = new List<JObject>();
            var entityObjectSerializer = JsonSerializer.Create(_jsonSerializerSettings);
            foreach (var obj in state)
            {
                if (obj != null && TryGetIdPropertyValue(obj, out var idValue, out var idPropertyType))
                {
                    JObject jobj = JObject.FromObject(obj, entityObjectSerializer);
                    jobj.Add("_links", JToken.FromObject(new
                    {
                        self = new
                        {
                            href = $"{GenerateLink(context, useIdRoute: true, idValue: idValue, idPropertyType: idPropertyType)}"
                        }
                    }));
                    var modifiedObj = jobj.ToObject<JObject>();
                    if (modifiedObj != null)
                    {
                        newState.Add(modifiedObj);
                    }
                }
            }

            return newState;
        }

        private string GenerateLink(ResultExecutingContext context,
            IEnumerable<KeyValuePair<string, StringValues>>? querySubstitution = null,
            bool useIdRoute = false,
            object? idValue = null,
            Type? idPropertyType = null)
        {
            var request = context.HttpContext.Request;
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug(request.Dump());
            }
            var scheme = request.Scheme;
            var host = request.Host;
            var pathBase = request.PathBase;
            var path = request.Path;
            if (useIdRoute && idValue != null && idPropertyType != null)
            {
                if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                {
                    var methods = from m in controllerActionDescriptor.ControllerTypeInfo.DeclaredMethods
                                  let parameters = m.GetParameters()
                                  where m.IsPublic &&
                                  (typeof(IActionResult).IsAssignableFrom(m.ReturnType) || typeof(Task<IActionResult>).IsAssignableFrom(m.ReturnType)) &&
                                  parameters.Length == 1 &&
                                  parameters[0].ParameterType == idPropertyType
                                  select new
                                  {
                                      Method = m,
                                      Parameter = parameters[0]
                                  };
                    if (methods.Any())
                    {
                        MethodInfo? getByIdMethod = null;
                        ParameterInfo? getByIdMethodParameter = null;
                        if (methods.Count() > 1)
                        {
                            var methodSig = methods.FirstOrDefault(m => m.Method.IsDefined(typeof(GetByIdMethodImplAttribute), false));
                            if (methodSig != null)
                            {
                                getByIdMethod = methodSig.Method;
                                getByIdMethodParameter = methodSig.Parameter;
                            }
                        }
                        else
                        {
                            getByIdMethod = methods.First().Method;
                            getByIdMethodParameter = methods.First().Parameter;
                        }

                        if (getByIdMethod != null && getByIdMethodParameter != null && context.Controller is ControllerBase ctlr)
                        {
                            var dict = new Dictionary<string, object>
                            {
                                { getByIdMethodParameter.Name!, idValue }
                            };

                            path = ctlr.Url.Action(getByIdMethod.Name, controllerActionDescriptor.ControllerName, dict);
                        }
                    }
                }
            }

            var substQuery = new Dictionary<string, StringValues>();

            if (querySubstitution != null)
            {
                if (request.Query?.Count > 0)
                {
                    request.Query.ToList().ForEach(q => substQuery.Add(q.Key, q.Value));
                    foreach (var subst in querySubstitution)
                    {
                        if (substQuery.ContainsKey(subst.Key))
                        {
                            substQuery[subst.Key] = subst.Value;
                        }
                        else
                        {
                            substQuery.Add(subst.Key, subst.Value);
                        }
                    }
                }
                else
                {
                    querySubstitution.ToList().ForEach(kvp => substQuery.Add(kvp.Key, kvp.Value));
                }
            }

            return UriHelper.BuildAbsolute(scheme, host, pathBase, path, QueryString.Create(substQuery));
        }

        private bool HasIdProperty(Type? entityType) => entityType != null &&
            (from property in entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
             where property.Name == _options.IdPropertyName &&
                 property.CanRead
             select property).Count() == 1;

        private bool TryGetIdPropertyValue(object? obj, out object? val, out Type? idPropertyType)
        {
            if (obj == null)
            {
                val = null;
                idPropertyType = null;
                return false;
            }

            var idProperty = (from property in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                              where property.Name == _options.IdPropertyName && property.CanRead
                              select property).FirstOrDefault();
            if (idProperty == null)
            {
                val = null;
                idPropertyType = null;
                return false;
            }

            val = idProperty.GetValue(obj);
            idPropertyType = idProperty.PropertyType;
            return true;
        }

        #endregion Private Methods
    }
}
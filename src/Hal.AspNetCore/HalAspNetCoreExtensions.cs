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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace Hal.AspNetCore
{
    /// <summary>
    /// Represents the class that holds the extension methods for HAL support in ASP.NET Core.
    /// </summary>
    public static class HalAspNetCoreExtensions
    {

        #region Public Methods

        /// <summary>
        /// Adds the HAL support to the ASP.NET Core application.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/> instance to which the HAL support is added.</param>
        /// <param name="options">The HAL options.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddHalSupport(this IServiceCollection serviceCollection, Action<SupportsHalOptions> options)
        {
            serviceCollection.Configure(options);
            serviceCollection.Configure<JsonSerializerSettings>(settings =>
            {
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.Formatting = Formatting.None;
                settings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            });
            serviceCollection.AddScoped<SupportsHalAttribute>();
            return serviceCollection;
        }

        /// <summary>
        /// Adds the HAL support to the ASP.NET Core application.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/> instance to which the HAL support is added.</param>
        /// <param name="options">The HAL options.</param>
        /// <param name="jsonSerializerSettings">The settings of the Json serializer that determines how the result JSON should be generated.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddHalSupport(this IServiceCollection serviceCollection, Action<SupportsHalOptions> options, Action<JsonSerializerSettings> jsonSerializerSettings)
        {
            serviceCollection.Configure(options);
            serviceCollection.Configure(jsonSerializerSettings);
            serviceCollection.AddScoped<SupportsHalAttribute>();
            return serviceCollection;
        }

        /// <summary>
        /// Adds the HAL support to the ASP.NET Core application with default options.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/> instance to which the HAL support is added.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddHalSupport(this IServiceCollection serviceCollection)
        {
            serviceCollection.Configure<SupportsHalOptions>(options =>
            {
                options.Enabled = true;
                options.IdPropertyName = "Id";
            });
            serviceCollection.Configure<JsonSerializerSettings>(settings =>
            {
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.Formatting = Formatting.None;
                settings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            });
            serviceCollection.AddScoped<SupportsHalAttribute>();
            return serviceCollection;
        }

        /// <summary>
        /// Adds the HAL support to the ASP.NET Core application.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/> instance to which the HAL support is added.</param>
        /// <param name="configSection">The configuration section that holds the HAL configuration options.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddHalSupport(this IServiceCollection serviceCollection, IConfigurationSection configSection)
        {
            serviceCollection.Configure<SupportsHalOptions>(configSection);
            serviceCollection.Configure<JsonSerializerSettings>(settings =>
            {
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.Formatting = Formatting.None;
                settings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            });
            serviceCollection.AddScoped<SupportsHalAttribute>();
            return serviceCollection;
        }

        /// <summary>
        /// Adds the HAL support to the ASP.NET Core application.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/>; instance to which the HAL support is added.</param>
        /// <param name="configSection">The configuration section that holds the HAL configuration options.</param>
        /// <param name="jsonSerializerSettings">The settings of the Json serializer that determines how the result JSON should be generated.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddHalSupport(this IServiceCollection serviceCollection, IConfigurationSection configSection, Action<JsonSerializerSettings> jsonSerializerSettings)
        {
            serviceCollection.Configure<SupportsHalOptions>(configSection);
            serviceCollection.Configure(jsonSerializerSettings);
            serviceCollection.AddScoped<SupportsHalAttribute>();
            return serviceCollection;
        }

        #endregion Public Methods

    }
}
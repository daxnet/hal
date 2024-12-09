// ---------------------------------------------------------------------------
//  _    _          _      
// | |  | |   /\   | |     
// | |__| |  /  \  | |     
// |  __  | / /\ \ | |     
// | |  | |/ ____ \| |____ 
// |_|  |_/_/    \_\______|
//
// A C#/.NET Core implementation of Hypertext Application Language
// https://stateless.group/hal_specification.html
// 
// MIT License
//
// Copyright (c) 2017-2025 Sunny Chen
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

using System.Collections.Generic;
using System.Linq;

namespace Hal.Builders;

/// <summary>
/// Represents that the implemented classes are HAL resource builders that
/// will add an embedded resource to the embedded resource collection of
/// the building HAL resource.
/// </summary>
/// <seealso cref="Hal.Builders.IBuilder" />
public interface IEmbeddedResourceItemBuilder : IBuilder
{
    /// <summary>
    /// Gets the name of the embedded resource collection.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    string Name { get; }
}

/// <summary>
/// Represents an internal implementation of the <see cref="IEmbeddedResourceItemBuilder"/> class.
/// </summary>
/// <seealso cref="Hal.Builders.Builder" />
/// <seealso cref="Hal.Builders.IEmbeddedResourceItemBuilder" />
internal sealed class EmbeddedResourceItemBuilder : Builder, IEmbeddedResourceItemBuilder
{
    #region Private Fields
    private readonly string _name;
    private readonly List<IBuilder> _resourceBuilders = new();
    #endregion

    #region Ctor        
    /// <summary>
    /// Initializes a new instance of the <see cref="EmbeddedResourceItemBuilder"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="name">The name of the embedded resource collection.</param>
    /// <param name="resourceBuilders">The resource builders that will build the embedded resource.</param>
    public EmbeddedResourceItemBuilder(IBuilder context, string name, params IBuilder[] resourceBuilders) : base(context)
    {
        _name = name;
        _resourceBuilders.AddRange(resourceBuilders);
    }
    #endregion

    #region Public Properties        
    /// <summary>
    /// Gets the name of the embedded resource collection.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name => _name;
    #endregion

    #region Protected Methods

    /// <summary>
    /// Builds the <see cref="Resource" /> instance.
    /// </summary>
    /// <param name="resource"></param>
    /// <returns>
    /// The <see cref="Resource" /> instance to be built.
    /// </returns>
    protected override Resource DoBuild(Resource resource)
    {
        var embeddedResource =
            resource.EmbeddedResources?.FirstOrDefault(x =>
                !string.IsNullOrEmpty(x.Name) && x.Name!.Equals(_name));
        if (embeddedResource == null)
        {
            embeddedResource = new EmbeddedResource
            {
                Name = _name,
                Resources = new ResourceCollection()
            };
                
            _resourceBuilders.ForEach(rb => embeddedResource.Resources.Add(rb.Build()));
            resource.EmbeddedResources?.Add(embeddedResource);
        }
        else
        {
            _resourceBuilders.ForEach(rb => embeddedResource.Resources.Add(rb.Build()));
        }

        return resource;
    }

    #endregion
}
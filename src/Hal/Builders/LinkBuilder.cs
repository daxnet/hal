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

using System.Linq;

namespace Hal.Builders;

/// <summary>
/// Represents that the implemented classes are HAL resource builders
/// that are responsible for adding the <see cref="ILink"/> instance
/// to the HAL resource.
/// </summary>
/// <seealso cref="Hal.Builders.IBuilder" />
public interface ILinkBuilder : IBuilder
{
    /// <summary>
    /// Gets the relation of the resource location.
    /// </summary>
    /// <value>
    /// The relation of the resource location.
    /// </value>
    string Rel { get; }

    /// <summary>
    /// Gets a value indicating whether the generated Json representation should be in an array
    /// format, even if the number of items is only one.
    /// </summary>
    /// <value>
    /// <c>true</c> if the generated Json representation should be in an array
    /// format; otherwise, <c>false</c>.
    /// </value>
    bool EnforcingArrayConverting { get; }
}

/// <summary>
/// Represents an internal implementation of <see cref="ILinkBuilder"/> interface.
/// </summary>
/// <seealso cref="Hal.Builders.Builder" />
/// <seealso cref="Hal.Builders.ILinkBuilder" />
internal sealed class LinkBuilder : Builder, ILinkBuilder
{
    #region Private Fields
    private readonly string _rel;
    private readonly bool _enforcingArrayConverting;
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="LinkBuilder"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="rel">The relation of the resource location.</param>
    /// <param name="enforcingArrayConverting">The value indicating whether the generated Json representation should be in an array
    /// format, even if the number of items is only one.</param>
    public LinkBuilder(IBuilder context, string rel, bool enforcingArrayConverting) 
        : base(context)
    {
        _rel = rel;
        _enforcingArrayConverting = enforcingArrayConverting;
    }

    #region Public Properties
    /// <summary>
    /// Gets the relation of the resource location.
    /// </summary>
    /// <value>
    /// The relation of the resource location.
    /// </value>
    public string Rel => _rel;

    /// <summary>
    /// Gets a value indicating whether the generated Json representation should be in an array
    /// format, even if the number of items is only one.
    /// </summary>
    /// <value>
    /// <c>true</c> if the generated Json representation should be in an array
    /// format; otherwise, <c>false</c>.
    /// </value>
    public bool EnforcingArrayConverting => _enforcingArrayConverting;
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
        if (resource.Links == null)
        {
            resource.Links = new LinkCollection();
        }

        var link = resource.Links.FirstOrDefault(x => x.Rel.Equals(_rel));
        if (link == null)
        {
            resource.Links.Add(new Link(_rel));
        }

        return resource;
    }
    #endregion
}
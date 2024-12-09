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

using Hal.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hal;

/// <summary>
/// Represents the link item of a link in the HAL.
/// </summary>
public sealed class LinkItem : ILinkItem
{
    #region Private Fields
    private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();
    #endregion

    #region Ctor
    /// <summary>
    /// Initializes a new instance of the <see cref="LinkItem"/> class.
    /// </summary>
    /// <param name="href">The href attribute of the link item.</param>
    public LinkItem(string href)
    {
        Href = href;
    }
    #endregion

    #region Public Properteis
    /// <summary>
    /// Gets or sets the href attribute of a link item. This value is required.
    /// </summary>
    /// <remarks>
    /// Its value is either a URI [RFC3986] or a URI Template [RFC6570].
    /// If the value is a URI Template then the <see cref="Link"/> Object SHOULD have a
    /// "templated" attribute whose value is true.
    /// </remarks>
    public string Href { get; set; }

    /// <summary>
    /// Gets or sets the name attribute of a link item. This value is optional.
    /// </summary>
    /// <remarks>
    /// Its value MAY be used as a secondary key for selecting Link Objects
    /// which share the same relation type.
    /// </remarks>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets a <see cref="bool"/> value which indicates if the <c>Href</c> property
    /// is a URI template. This value is optional.
    /// </summary>
    /// <remarks>
    /// Its value SHOULD be considered false if it is undefined or any other
    /// value than true.
    /// </remarks>
    public bool? Templated { get; set; }

    /// <summary>
    /// Gets or sets the media type expected when dereferencing the target source. This value
    /// is optional.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets a URL which provides further information about the deprecation. This value is
    /// optional.
    /// </summary>
    /// <remarks>
    /// Its presence indicates that the link is to be deprecated (i.e.
    /// removed) at a future date.Its value is a URL that SHOULD provide
    /// further information about the deprecation.
    /// A client SHOULD provide some notification (for example, by logging a
    /// warning message) whenever it traverses over a link that has this
    /// property.The notification SHOULD include the deprecation property's
    /// value so that a client manitainer can easily find information about
    /// the deprecation.
    /// </remarks>
    public string? Deprecation { get; set; }

    /// <summary>
    /// Gets or sets the URI that hints about the profile of the target resource. This
    /// value is optional.
    /// </summary>
    public string? Profile { get; set; }

    /// <summary>
    /// Gets or sets a <see cref="string"/> value which is intended for labelling
    /// the link with a human-readable identifier. This value is optional.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets a <see cref="string"/> value which is intending for indicating
    /// the language of the target resource.
    /// </summary>
    public string? Hreflang { get; set; }

    /// <summary>
    /// Gets a list of additional properties assigned to the current link item.
    /// </summary>
    public IEnumerable<KeyValuePair<string, object>> Properties => _properties;
    #endregion

    #region Public Methods
    /// <summary>
    /// Adds a property to the additional properties collection.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="value">The value of the property.</param>
    public void AddProperty(string name, object value)
    {
        _properties.Add(name, value);
    }

    /// <summary>
    /// Removes all the properties from the properties collection.
    /// </summary>
    public void ClearProperties()
    {
        _properties.Clear();
    }

    /// <summary>
    /// Gets the property value by using the specified name.
    /// </summary>
    /// <param name="name">The name of the property which the property value should be returned.</param>
    /// <returns>The value of the property.</returns>
    public object GetProperty(string name)
    {
        return _properties[name];
    }

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() => ToString(new JsonSerializerSettings
    {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore
    });

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <param name="jsonSerializerSettings">The serialization settings.</param>
    /// <returns>The string representation of the current instance.</returns>
    public string ToString(JsonSerializerSettings jsonSerializerSettings)
    {
        jsonSerializerSettings.Converters = new[] { new LinkItemConverter() };
        return JsonConvert.SerializeObject(this, jsonSerializerSettings);
    }

    #endregion

}
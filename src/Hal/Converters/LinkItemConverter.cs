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

using Newtonsoft.Json;
using System;
using System.Linq;

namespace Hal.Converters;

/// <summary>
/// Represents the JSON converter for link items.
/// </summary>
/// <seealso cref="Newtonsoft.Json.JsonConverter" />
public sealed class LinkItemConverter : JsonConverter
{
    /// <summary>
    /// Determines whether this instance can convert the specified object type.
    /// </summary>
    /// <param name="objectType">Type of the object.</param>
    /// <returns>
    /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
    /// </returns>
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(LinkItem);
    }

    /// <summary>
    /// Reads the JSON representation of the object.
    /// </summary>
    /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="existingValue">The existing value of object being read.</param>
    /// <param name="serializer">The calling serializer.</param>
    /// <returns>
    /// The object value.
    /// </returns>
    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer) => null;

    /// <summary>
    /// Writes the JSON representation of the object.
    /// </summary>
    /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
    /// <param name="value">The value.</param>
    /// <param name="serializer">The calling serializer.</param>
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        LinkItem li = (LinkItem)value!;
        writer.WriteStartObject();
        if (!string.IsNullOrEmpty(li.Name) || serializer.NullValueHandling == NullValueHandling.Include)
        {
            writer.WritePropertyName("name");
            writer.WriteValue(li.Name);
        }

        if (!string.IsNullOrEmpty(li.Href) || serializer.NullValueHandling == NullValueHandling.Include)
        {
            writer.WritePropertyName("href");
            writer.WriteValue(li.Href);
        }

        if (li.Templated.HasValue || serializer.NullValueHandling == NullValueHandling.Include)
        {
            writer.WritePropertyName("templated");
            writer.WriteValue(li.Templated);
        }

        if (!string.IsNullOrEmpty(li.Type) || serializer.NullValueHandling == NullValueHandling.Include)
        {
            writer.WritePropertyName("type");
            writer.WriteValue(li.Type);
        }

        if (!string.IsNullOrEmpty(li.Deprecation) || serializer.NullValueHandling == NullValueHandling.Include)
        {
            writer.WritePropertyName("deprecation");
            writer.WriteValue(li.Deprecation);
        }

        if (!string.IsNullOrEmpty(li.Title) || serializer.NullValueHandling == NullValueHandling.Include)
        {
            writer.WritePropertyName("title");
            writer.WriteValue(li.Title);
        }

        if (!string.IsNullOrEmpty(li.Profile) || serializer.NullValueHandling == NullValueHandling.Include)
        {
            writer.WritePropertyName("profile");
            writer.WriteValue(li.Profile);
        }

        if (!string.IsNullOrEmpty(li.Hreflang) || serializer.NullValueHandling == NullValueHandling.Include)
        {
            writer.WritePropertyName("hreflang");
            writer.WriteValue(li.Hreflang);
        }

        if (li.Properties.Count() > 0)
        {
            foreach(var p in li.Properties)
            {
                writer.WritePropertyName(p.Key);
                serializer.Serialize(writer, p.Value);
            }
        }

        writer.WriteEndObject();
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter" /> can read JSON.
    /// </summary>
    /// <value>
    /// <c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter" /> can read JSON; otherwise, <c>false</c>.
    /// </value>
    public override bool CanRead => false;
}
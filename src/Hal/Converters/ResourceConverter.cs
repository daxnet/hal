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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Hal.Converters
{
    /// <summary>
    /// Represents the JSON converter for resources.
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    public sealed class ResourceConverter : JsonConverter
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
            return objectType == typeof(Resource);
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
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return null;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var resource = (Resource)value;
            JToken obj = null;

            if (resource.State != null)
            {
                obj = JToken.FromObject(resource.State);
                if (obj != null && obj.Type == JTokenType.Array)
                {
                    obj.WriteTo(writer);
                    return;
                }
            }

            writer.WriteStartObject();

            if (resource.Links != null && resource.Links.Count > 0)
            {
                serializer.Serialize(writer, resource.Links);
            }

            if (obj != null)
            {
                if (obj.Type != JTokenType.Object)
                {
                    obj.WriteTo(writer);
                }

                var @object = (JObject)obj;
                foreach (var prop in @object.Properties())
                {
                    prop.WriteTo(writer);
                }
            }

            if (resource.EmbeddedResources != null && resource.EmbeddedResources.Count() > 0)
            {
                writer.WritePropertyName("_embedded");
                writer.WriteStartObject();
                foreach(var embeddedResource in resource.EmbeddedResources)
                {
                    writer.WritePropertyName(embeddedResource.Name);
                    if (embeddedResource.Resources != null && embeddedResource.Resources.Count > 0)
                    {
                        if (embeddedResource.Resources.Count == 1)
                        {
                            //writer.WriteStartObject();
                            var first = embeddedResource.Resources.First();
                            WriteJson(writer, first, serializer);
                            //writer.WriteEndObject();
                        }
                        else
                        {
                            writer.WriteStartArray();
                            foreach(var current in embeddedResource.Resources)
                            {
                                WriteJson(writer, current, serializer);
                            }
                            writer.WriteEndArray();
                        }
                    }
                }
                writer.WriteEndObject();
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
}

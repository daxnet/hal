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

using Hal.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hal
{
    /// <summary>
    /// Represents a resource in HAL.
    /// </summary>
    /// <seealso cref="Hal.IResource" />
    public sealed class Resource : IResource
    {
        #region Private Fields
        private static readonly List<JsonConverter> converters = new List<JsonConverter>
        {
            new LinkItemConverter(), new LinkItemCollectionConverter(), new LinkConverter(),
            new LinkCollectionConverter(), new ResourceConverter()
        };

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        public Resource() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        /// <param name="state">The state of the resource.</param>
        public Resource(object? state)
        {
            this.State = state;
        }

        #region Public Properties        
        /// <summary>
        /// Gets the embedded resources.
        /// </summary>
        /// <value>
        /// The embedded resources.
        /// </value>
        public EmbeddedResourceCollection? EmbeddedResources { get; set; }

        /// <summary>
        /// Gets or sets the links.
        /// </summary>
        /// <value>
        /// The links.
        /// </value>
        public LinkCollection? Links { get; set; }

        /// <summary>
        /// Gets or sets the state of the resource, usually it is the object
        /// that holds the domain information.
        /// </summary>
        /// <value>
        /// The state of the resource.
        /// </value>
        public object? State { get; set; }
        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => ToString(new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented
        });

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="jsonSerializerSettings">The serialization settings.</param>
        /// <returns>The string representation of the current instance.</returns>
        public string ToString(JsonSerializerSettings jsonSerializerSettings)
        {
            if (jsonSerializerSettings.Converters.Count == 0)
                jsonSerializerSettings.Converters = converters;
            return JsonConvert.SerializeObject(this, jsonSerializerSettings);
        }
        #endregion
    }
}

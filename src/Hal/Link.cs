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

using Hal.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hal
{
    /// <summary>
    /// Represents the link in the HAL.
    /// </summary>
    /// <seealso cref="Hal.ILink" />
    public sealed class Link : ILink
    {
        #region Ctor        
        /// <summary>
        /// Initializes a new instance of the <see cref="Link"/> class.
        /// </summary>
        /// <param name="rel">The relation of the resource location.</param>
        public Link(string rel)
        {
            this.Rel = rel;
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the relation.
        /// </summary>
        /// <value>
        /// The relation.
        /// </value>
        public string Rel { get; set; }

        /// <summary>
        /// Gets or sets the link items that belongs to the current link.
        /// </summary>
        /// <value>
        /// The link items.
        /// </value>
        public LinkItemCollection Items { get; set; }
        #endregion

        #region Public Methods        
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter>
                {
                    new LinkItemConverter(),
                    new LinkItemCollectionConverter(),
                    new LinkConverter()
                }
            };

            return JsonConvert.SerializeObject(this, serializerSettings);
        }
        #endregion

    }
}

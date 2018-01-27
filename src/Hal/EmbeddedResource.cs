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

namespace Hal
{
    /// <summary>
    /// Represents the embedded resource in HAL.
    /// </summary>
    /// <seealso cref="Hal.IEmbeddedResource" />
    public sealed class EmbeddedResource : IEmbeddedResource
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedResource"/> class.
        /// </summary>
        public EmbeddedResource()
        {
            this.Resources = new ResourceCollection();
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the name of the embedded resource.
        /// </summary>
        /// <value>
        /// The name of the embedded resource.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the collection of resources that is represented by current embedded resource
        /// instance.
        /// </summary>
        /// <value>
        /// The collection of resources that is represented by current embedded resource
        /// instance.
        /// </value>
        public ResourceCollection Resources { get; set; }

        /// <summary>
        /// Indicates whether the embedded resource state should be always converted as an array
        /// even if there is only one state for that embedded resource.
        /// </summary>
        public bool EnforcingArrayConverting{ get; set; }
        #endregion
    }
}

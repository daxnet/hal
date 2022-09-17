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

namespace Hal.Builders
{
    /// <summary>
    /// Represents the base class for all the HAL builders.
    /// </summary>
    /// <seealso cref="Hal.Builders.IBuilder" />
    public abstract class Builder : IBuilder
    {
        #region Private Fields
        private readonly IBuilder context;
        #endregion

        #region Ctor        
        /// <summary>
        /// Initializes a new instance of the <see cref="Builder"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected Builder(IBuilder context)
        {
            this.context = context;
        }
        #endregion

        #region Public Methods        
        /// <summary>
        /// Builds the <see cref="Resource" /> instance.
        /// </summary>
        /// <returns>
        /// The <see cref="Resource" /> instance to be built.
        /// </returns>
        public Resource Build()
        {
            var resource = this.context.Build();
            return this.DoBuild(resource);
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Builds the <see cref="Resource" /> instance.
        /// </summary>
        /// <returns>
        /// The <see cref="Resource" /> instance to be built.
        /// </returns>
        protected abstract Resource DoBuild(Resource resource);
        #endregion
    }
}

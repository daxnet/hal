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

using System;
using System.Linq;

namespace Hal.Builders
{
    /// <summary>
    /// Represents that the implemented classes are HAL resource builders
    /// that will initialize the <see cref="IEmbeddedResource"/> collection
    /// on the building resource.
    /// </summary>
    /// <seealso cref="Hal.Builders.IBuilder" />
    public interface IEmbeddedResourceBuilder : IBuilder
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
    /// Represents an internal implementation of the <see cref="IEmbeddedResourceBuilder"/> class.
    /// </summary>
    /// <seealso cref="Hal.Builders.Builder" />
    /// <seealso cref="Hal.Builders.IEmbeddedResourceBuilder" />
    internal sealed class EmbeddedResourceBuilder : Builder, IEmbeddedResourceBuilder
    {
        #region Private Fields
        private readonly string name;
        private readonly bool enforcingArrayConverting;
        #endregion

        #region Ctor        
        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedResourceBuilder"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name of the embedded resource collection.</param>
        /// <param name="enforcingArrayConverting">The <see cref="Boolean"/> value which indicates whether the embedded resource state
        /// should be always converted as an array even if there is only one state for that embedded resource.</param>
        public EmbeddedResourceBuilder(IBuilder context, string name, bool enforcingArrayConverting = false) : base(context)
        {
            this.name = name;
            this.enforcingArrayConverting = enforcingArrayConverting;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the name of the embedded resource collection.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => this.name;
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
            if (resource.EmbeddedResources == null)
            {
                resource.EmbeddedResources = new EmbeddedResourceCollection(this.enforcingArrayConverting);
            }

            var embeddedResource = resource.EmbeddedResources.FirstOrDefault(x => !string.IsNullOrEmpty(x.Name) && x.Name!.Equals(this.name));
            if (embeddedResource == null)
            {
                embeddedResource = new EmbeddedResource
                {
                    Name = this.name
                };

                resource.EmbeddedResources.Add(embeddedResource);
            }

            return resource;
        }
        #endregion
    }
}

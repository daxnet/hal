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

using System.Collections.Generic;
using System.Linq;

namespace Hal.Builders
{
    /// <summary>
    /// Represents that the implemented classes are the builders
    /// that are responsible for adding the <see cref="ILinkItem"/>
    /// objects to the HAL resource.
    /// </summary>
    /// <seealso cref="Hal.Builders.IBuilder" />
    public interface ILinkItemBuilder : IBuilder
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
    /// Represents an internal implementation of <see cref="ILinkItemBuilder"/>.
    /// </summary>
    /// <seealso cref="Hal.Builders.Builder" />
    /// <seealso cref="Hal.Builders.ILinkItemBuilder" />
    internal sealed class LinkItemBuilder : Builder, ILinkItemBuilder
    {
        #region Private Fields
        private readonly string rel;
        private readonly string href;
        private readonly string name;
        private readonly bool? templated;
        private readonly string type;
        private readonly string deprecation;
        private readonly string profile;
        private readonly string title;
        private readonly string hreflang;
        private readonly bool enforcingArrayConverting;
        private readonly IDictionary<string, object> additionalProperties;
        #endregion

        #region Ctor        
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkItemBuilder"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="rel">The relation of the resource location.</param>
        /// <param name="href">The href attribute of a link item.</param>
        /// <param name="name">The name attribute of a link item.</param>
        /// <param name="templated">The <see cref="bool"/> value which indicates if the <c>Href</c> property
        /// is a URI template.</param>
        /// <param name="type">The media type expected when dereferencing the target source.</param>
        /// <param name="deprecation">The URL which provides further information about the deprecation.</param>
        /// <param name="profile">The URI that hints about the profile of the target resource.</param>
        /// <param name="title">The <see cref="string"/> value which is intended for labelling
        /// the link with a human-readable identifier.</param>
        /// <param name="hreflang">The <see cref="string"/> value which is intending for indicating
        /// the language of the target resource.</param>
        /// <param name="enforcingArrayConverting">The value indicating whether the generated Json representation should be in an array
        /// format, even if the number of items is only one.</param>
        /// <param name="additionalProperties">The additional properties.</param>
        public LinkItemBuilder(IBuilder context, string rel, string href,
            string name = null, bool? templated = null, string type = null,
            string deprecation = null, string profile = null, string title = null,
            string hreflang = null, bool enforcingArrayConverting = false, 
            IDictionary<string, object> additionalProperties = null) : base(context)
        {
            this.rel = rel;
            this.href = href;
            this.name = name;
            this.templated = templated;
            this.type = type;
            this.deprecation = deprecation;
            this.profile = profile;
            this.title = title;
            this.hreflang = hreflang;
            this.enforcingArrayConverting = enforcingArrayConverting;
            this.additionalProperties = additionalProperties;
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets the relation of the resource location.
        /// </summary>
        /// <value>
        /// The relation of the resource location.
        /// </value>
        public string Rel => this.rel;

        /// <summary>
        /// Gets a value indicating whether the generated Json representation should be in an array
        /// format, even if the number of items is only one.
        /// </summary>
        /// <value>
        /// <c>true</c> if the generated Json representation should be in an array
        /// format; otherwise, <c>false</c>.
        /// </value>
        public bool EnforcingArrayConverting => this.enforcingArrayConverting;
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
            var link = resource.Links.FirstOrDefault(x => x.Rel.Equals(this.rel));
            if (link == null)
            {
                link = new Link(this.rel);
                resource.Links.Add(link);
            }

            if (link.Items == null)
            {
                link.Items = new LinkItemCollection(this.enforcingArrayConverting);
            }

            var linkItem = new LinkItem(this.href)
            {
                Deprecation = this.deprecation,
                Hreflang = this.hreflang,
                Name = this.name,
                Profile = this.profile,
                Templated = this.templated,
                Title = this.title,
                Type = this.type
            };

            if (this.additionalProperties!= null && this.additionalProperties.Count > 0)
            {
                foreach(var property in this.additionalProperties)
                {
                    linkItem.AddProperty(property.Key, property.Value);
                }
            }

            link.Items.Add(linkItem);

            return resource;
        }
        #endregion
    }
}

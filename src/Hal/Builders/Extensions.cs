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

using System;
using System.Collections.Generic;

namespace Hal.Builders
{
    /// <summary>
    /// Represents the class that provides the extension methods to form
    /// the Fluent API of the building of HAL resources.
    /// </summary>
    public static class Extensions
    {
        #region IResourceBuilder Extensions        
        /// <summary>
        /// Assigns the object state to the resource that is going to be built.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="state">The object state.</param>
        /// <returns></returns>
        public static IResourceStateBuilder WithState(this IResourceBuilder builder, object state)
        {
            return new ResourceStateBuilder(builder, state);
        }
        #endregion

        #region IResourceStateBuilder Extensions        
        /// <summary>
        /// Adds a link to the building resource.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="rel">The relation of the resource location.</param>
        /// <returns></returns>
        public static ILinkBuilder AddLink(this IResourceStateBuilder builder, string rel)
        {
            return new LinkBuilder(builder, rel, false);
        }

        /// <summary>
        /// Adds the "self" link.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static ILinkBuilder AddSelfLink(this IResourceStateBuilder builder)
        {
            return new LinkBuilder(builder, "self", false);
        }

        /// <summary>
        /// Adds the "curies" link.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static ILinkBuilder AddCuriesLink(this IResourceStateBuilder builder)
        {
            return new LinkBuilder(builder, "curies", true);
        }

        /// <summary>
        /// Adds the embedded resource collection to the building resource.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="name">The name of the embedded resource collection.</param>
        /// <param name="enforcingArrayConverting">The <see cref="Boolean"/> value which indicates whether the embedded resource state
        /// should be always converted as an array even if there is only one state for that embedded resource.</param>
        /// <returns></returns>
        public static IEmbeddedResourceBuilder AddEmbedded(this IResourceStateBuilder builder, string name, bool enforcingArrayConverting = false)
        {
            return new EmbeddedResourceBuilder(builder, name, enforcingArrayConverting);
        }
        #endregion

        #region ILinkBuilder Extensions        
        /// <summary>
        /// Adds a link item to the currently building link.
        /// </summary>
        /// <param name="linkBuilder">The link builder.</param>
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
        /// <param name="additionalProperties">The additional properties.</param>
        /// <returns></returns>
        public static ILinkItemBuilder WithLinkItem(this ILinkBuilder linkBuilder, string href,
            string name = null, bool? templated = null, string type = null,
            string deprecation = null, string profile = null, string title = null,
            string hreflang = null,
            IDictionary<string, object> additionalProperties = null)
        {
            return new LinkItemBuilder(linkBuilder, linkBuilder.Rel, href, name, templated,
                type, deprecation, profile, title, hreflang, linkBuilder.EnforcingArrayConverting, additionalProperties);
        }
        #endregion

        #region ILinkItemBuilder Extensions

        /// <summary>
        /// Adds a link to the building resource.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="rel">The relation of the resource location.</param>
        /// <returns></returns>
        public static ILinkBuilder AddLink(this ILinkItemBuilder builder, string rel)
        {
            return new LinkBuilder(builder, rel, false);
        }

        /// <summary>
        /// Adds the "self" link.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static ILinkBuilder AddSelfLink(this ILinkItemBuilder builder)
        {
            return new LinkBuilder(builder, "self", false);
        }

        /// <summary>
        /// Adds the "curies" link.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static ILinkBuilder AddCuriesLink(this ILinkItemBuilder builder)
        {
            return new LinkBuilder(builder, "curies", true);
        }

        /// <summary>
        /// Adds a link item to the currently building link.
        /// </summary>
        /// <param name="linkBuilder">The link builder.</param>
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
        /// <param name="additionalProperties">The additional properties.</param>
        /// <returns></returns>
        public static ILinkItemBuilder WithLinkItem(this ILinkItemBuilder builder, string href,
            string name = null, bool? templated = null, string type = null,
            string deprecation = null, string profile = null, string title = null,
            string hreflang = null,
            IDictionary<string, object> additionalProperties = null)
        {
            return new LinkItemBuilder(builder, builder.Rel, href, name, templated,
                type, deprecation, profile, title, hreflang, builder.EnforcingArrayConverting, additionalProperties);
        }

        /// <summary>
        /// Adds the embedded resource collection to the building resource.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="name">The name of the embedded resource collection.</param>
        /// <param name="enforcingArrayConverting">The <see cref="Boolean"/> value which indicates whether the embedded resource state
        /// should be always converted as an array even if there is only one state for that embedded resource.</param>
        /// <returns></returns>
        public static IEmbeddedResourceBuilder AddEmbedded(this ILinkItemBuilder builder, string name, bool enforcingArrayConverting = false)
        {
            return new EmbeddedResourceBuilder(builder, name, enforcingArrayConverting);
        }
        #endregion

        #region IEmbeddedResourceBuilder Extensions        
        /// <summary>
        /// Adds the embedded resource to the embedded resource collection of the building resource.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="resourceBuilder">The resource builder that will build the embedded resource.</param>
        /// <returns></returns>
        public static IEmbeddedResourceItemBuilder Resource(this IEmbeddedResourceBuilder builder, IBuilder resourceBuilder)
        {
            return new EmbeddedResourceItemBuilder(builder, builder.Name, resourceBuilder);
        }
        #endregion

        #region IEmbeddedResourceItemBuilder Extensions

        /// <summary>
        /// Adds the embedded resource to the embedded resource collection of the building resource.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="resourceBuilder">The resource builder that will build the embedded resource.</param>
        /// <returns></returns>
        public static IEmbeddedResourceItemBuilder Resource(this IEmbeddedResourceItemBuilder builder, IBuilder resourceBuilder)
        {
            return new EmbeddedResourceItemBuilder(builder, builder.Name, resourceBuilder);
        }

        /// <summary>
        /// Adds a link to the building resource.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="rel">The relation of the resource location.</param>
        /// <returns></returns>
        public static ILinkBuilder AddLink(this IEmbeddedResourceItemBuilder builder, string rel)
        {
            return new LinkBuilder(builder, rel, false);
        }

        /// <summary>
        /// Adds the "self" link.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static ILinkBuilder AddSelfLink(this IEmbeddedResourceItemBuilder builder)
        {
            return new LinkBuilder(builder, "self", false);
        }

        /// <summary>
        /// Adds the "curies" link.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static ILinkBuilder AddCuriesLink(this IEmbeddedResourceItemBuilder builder)
        {
            return new LinkBuilder(builder, "curies", true);
        }

        /// <summary>
        /// Adds the embedded resource collection to the building resource.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="name">The name of the embedded resource collection.</param>
        /// <param name="enforcingArrayConverting">The <see cref="Boolean"/> value which indicates whether the embedded resource state
        /// should be always converted as an array even if there is only one state for that embedded resource.</param>
        /// <returns></returns>
        public static IEmbeddedResourceBuilder AddEmbedded(this IEmbeddedResourceItemBuilder builder, string name, bool enforcingArrayConverting = false)
        {
            return new EmbeddedResourceBuilder(builder, name, enforcingArrayConverting);
        }
        #endregion
    }
}

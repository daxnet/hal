using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal.Builders
{
    public static class Extensions
    {
        #region IResourceBuilder Extensions
        public static IResourceStateBuilder WithState(this IResourceBuilder builder, object state)
        {
            return new ResourceStateBuilder(builder, state);
        }
        #endregion

        #region IResourceStateBuilder Extensions
        public static ILinkBuilder AddLink(this IResourceStateBuilder builder, string rel)
        {
            return new LinkBuilder(builder, rel, false);
        }

        public static ILinkBuilder AddSelfLink(this IResourceStateBuilder builder)
        {
            return new LinkBuilder(builder, "self", false);
        }

        public static ILinkBuilder AddCuriesLink(this IResourceStateBuilder builder)
        {
            return new LinkBuilder(builder, "curies", true);
        }
        #endregion

        #region ILinkBuilder Extensions
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
        public static ILinkBuilder AddLink(this ILinkItemBuilder builder, string rel)
        {
            return new LinkBuilder(builder, rel, false);
        }

        public static ILinkBuilder AddSelfLink(this ILinkItemBuilder builder)
        {
            return new LinkBuilder(builder, "self", false);
        }

        public static ILinkBuilder AddCuriesLink(this ILinkItemBuilder builder)
        {
            return new LinkBuilder(builder, "curies", true);
        }

        public static ILinkItemBuilder WithLinkItem(this ILinkItemBuilder builder, string href,
            string name = null, bool? templated = null, string type = null,
            string deprecation = null, string profile = null, string title = null,
            string hreflang = null,
            IDictionary<string, object> additionalProperties = null)
        {
            return new LinkItemBuilder(builder, builder.Rel, href, name, templated,
                type, deprecation, profile, title, hreflang, builder.EnforcingArrayConverting, additionalProperties);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal.Builders
{
    public static class Extensions
    {
        public static ILinkBuilder AddLink(this IResourceBuilder builder, string rel)
        {
            return new LinkBuilder(builder, rel);
        }

        public static ILinkItemBuilder WithLinkItem(this ILinkBuilder linkBuilder, string href,
            string name = null, bool? templated = null, string type = null,
            string deprecation = null, string profile = null, string title = null,
            string hreflang = null, bool enforcingArrayConverting = false,
            IDictionary<string, object> additionalProperties = null)
        {
            return new LinkItemBuilder(linkBuilder, linkBuilder.Rel, href, name, templated,
                type, deprecation, profile, title, hreflang, enforcingArrayConverting, additionalProperties);
        }
    }
}

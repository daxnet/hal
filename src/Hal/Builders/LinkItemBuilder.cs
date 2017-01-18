using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal.Builders
{
    public interface ILinkItemBuilder : IBuilder
    {
        string Rel { get; }
        bool EnforcingArrayConverting { get; }
    }

    internal sealed class LinkItemBuilder : Builder, ILinkItemBuilder
    {
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

        public string Rel => this.rel;

        public bool EnforcingArrayConverting => this.enforcingArrayConverting;

        protected override Resource DoBuild(Resource resource)
        {
            var link = resource.Links.FirstOrDefault(x => x.Rel.Equals(this.rel));
            if (link == null)
            {
                throw new InvalidOperationException("Cannot find the link");
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
    }
}

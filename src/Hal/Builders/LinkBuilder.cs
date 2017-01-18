using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal.Builders
{
    public interface ILinkBuilder : IBuilder
    {
        string Rel { get; }

        bool EnforcingArrayConverting { get; }
    }

    internal sealed class LinkBuilder : Builder, ILinkBuilder
    {
        private readonly string rel;
        private readonly bool enforcingArrayConverting;

        public LinkBuilder(IBuilder context, string rel, bool enforcingArrayConverting) 
            : base(context)
        {
            this.rel = rel;
            this.enforcingArrayConverting = enforcingArrayConverting;
        }

        public string Rel => this.rel;

        public bool EnforcingArrayConverting => this.enforcingArrayConverting;

        protected override Resource DoBuild(Resource resource)
        {
            if (resource.Links == null)
            {
                resource.Links = new LinkCollection();
            }

            var link = resource.Links.FirstOrDefault(x => x.Rel.Equals(this.rel));
            if (link == null)
            {
                resource.Links.Add(new Link(this.rel));
            }

            return resource;
        }
    }
}

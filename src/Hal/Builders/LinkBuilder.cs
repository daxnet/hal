using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal.Builders
{
    public interface ILinkBuilder : IBuilder
    {
        string Rel { get; }
    }

    internal sealed class LinkBuilder : Builder, ILinkBuilder
    {
        private readonly string rel;

        public LinkBuilder(IBuilder context, string rel) : base(context)
        {
            this.rel = rel;
        }

        public string Rel => this.rel;

        protected override Resource DoBuild(Resource resource)
        {
            var link = resource.Links.FirstOrDefault(x => x.Rel.Equals(this.rel));
            if (link == null)
            {
                resource.Links.Add(new Link(this.rel));
            }

            return resource;
        }
    }
}

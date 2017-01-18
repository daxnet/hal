using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal.Builders
{
    public interface IEmbeddedResourceBuilder : IBuilder { }

    internal sealed class EmbeddedResourceBuilder : Builder, IEmbeddedResourceBuilder
    {
        private readonly string name;
        private readonly IBuilder resourceBuilder;

        public EmbeddedResourceBuilder(IBuilder context, string name, IBuilder resourceBuilder) : base(context)
        {
            this.name = name;
            this.resourceBuilder = resourceBuilder;
        }

        protected override Resource DoBuild(Resource resource)
        {
            // TODO: Add resource build logic here
            return resource;
        }
    }
}

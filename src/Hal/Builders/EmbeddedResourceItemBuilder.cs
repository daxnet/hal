using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal.Builders
{
    public interface IEmbeddedResourceItemBuilder : IBuilder
    {
        string Name { get; }
    }

    internal sealed class EmbeddedResourceItemBuilder : Builder, IEmbeddedResourceItemBuilder
    {
        private readonly string name;
        private readonly IBuilder resourceBuilder;

        public EmbeddedResourceItemBuilder(IBuilder context, string name, IBuilder resourceBuilder) : base(context)
        {
            this.name = name;
            this.resourceBuilder = resourceBuilder;
        }

        public string Name => this.name;

        protected override Resource DoBuild(Resource resource)
        {
            var embeddedResource = resource.EmbeddedResources.First(x => x.Name.Equals(this.name));
            if (embeddedResource == null)
            {
                embeddedResource = new EmbeddedResource
                {
                    Name = this.name,
                    Resources = new ResourceCollection
                    {
                        this.resourceBuilder.Build()
                    }
                };
                resource.EmbeddedResources.Add(embeddedResource);
            }
            else
            {
                embeddedResource.Resources.Add(this.resourceBuilder.Build());
            }

            return resource;
        }
    }
}

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
            if (resource.EmbeddedResources == null)
            {
                resource.EmbeddedResources = new EmbeddedResourceCollection();
            }

            var embeddedResource = resource.EmbeddedResources.FirstOrDefault(x => x.Name.Equals(this.name));
            if (embeddedResource != null)
            {
                embeddedResource.Resources.Add(this.resourceBuilder.Build());
            }
            else
            {
                var newEmbeddedResource = new EmbeddedResource
                {
                    Name = this.name,
                    Resources = new ResourceCollection
                    {
                        this.resourceBuilder.Build()
                    }
                };

                resource.EmbeddedResources = new EmbeddedResourceCollection { newEmbeddedResource };
            }

            return resource;
        }
    }
}

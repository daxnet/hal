using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal.Builders
{
    public interface IEmbeddedResourceBuilder : IBuilder
    {
        string Name { get; }
    }

    internal sealed class EmbeddedResourceBuilder : Builder, IEmbeddedResourceBuilder
    {
        private readonly string name;

        public EmbeddedResourceBuilder(IBuilder context, string name) : base(context)
        {
            this.name = name;
        }

        public string Name => this.name;

        protected override Resource DoBuild(Resource resource)
        {
            if (resource.EmbeddedResources == null)
            {
                resource.EmbeddedResources = new EmbeddedResourceCollection();
            }

            var embeddedResource = resource.EmbeddedResources.FirstOrDefault(x => x.Name.Equals(this.name));
            if (embeddedResource == null)
            {
                embeddedResource = new EmbeddedResource
                {
                    Name = this.name
                };

                resource.EmbeddedResources.Add(embeddedResource);
            }

            return resource;
        }
    }
}

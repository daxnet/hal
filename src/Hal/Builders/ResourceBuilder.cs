using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal.Builders
{
    public interface IResourceBuilder : IBuilder { }

    public sealed class ResourceBuilder : IResourceBuilder
    {
        private readonly Resource resource = new Resource();

        public ResourceBuilder()
        {
            
        }

        public Resource Build()
        {
            return resource;
        }
    }
}

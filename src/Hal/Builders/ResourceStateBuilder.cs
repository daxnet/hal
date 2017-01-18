using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal.Builders
{
    public interface IResourceStateBuilder : IBuilder { }

    internal sealed class ResourceStateBuilder : Builder, IResourceStateBuilder
    {
        private readonly object state;

        public ResourceStateBuilder(IBuilder context, object state) : base(context)
        {
            this.state = state;
        }

        protected override Resource DoBuild(Resource resource)
        {
            resource.State = this.state;
            return resource;
        }
    }
}

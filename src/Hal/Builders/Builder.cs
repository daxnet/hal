using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal.Builders
{
    public abstract class Builder : IBuilder
    {
        private readonly IBuilder context;

        protected Builder(IBuilder context)
        {
            this.context = context;
        }

        public IBuilder Context => context;

        public Resource Build()
        {
            var resource = this.context.Build();
            return this.DoBuild(resource);
        }

        protected abstract Resource DoBuild(Resource resource);
    }
}

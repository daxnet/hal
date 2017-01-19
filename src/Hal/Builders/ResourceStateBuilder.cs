using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal.Builders
{
    /// <summary>
    /// Represents that the implemented classes are HAL resource builders
    /// that 
    /// </summary>
    /// <seealso cref="Hal.Builders.IBuilder" />
    public interface IResourceStateBuilder : IBuilder { }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Hal.Builders.Builder" />
    /// <seealso cref="Hal.Builders.IResourceStateBuilder" />
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

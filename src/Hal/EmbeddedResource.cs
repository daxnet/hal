using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal
{
    public sealed class EmbeddedResource : IEmbeddedResource
    {
        public string Name { get; set; }

        public ResourceCollection Resources { get; set; }
    }
}

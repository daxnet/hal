using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal
{
    public interface IEmbeddedResource
    {
        string Name { get; set; }

        ResourceCollection Resources { get; set; }
    }
}

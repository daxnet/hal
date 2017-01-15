using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal
{
    public interface IResource
    {
        object State { get; set; }

        LinkCollection Links { get; set; }

        IEnumerable<IEmbeddedResource> EmbeddedResources { get; }

        string ToJson();
    }
}

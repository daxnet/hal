using System.Collections.Generic;

namespace Hal
{
    /// <summary>
    /// Represents that the implemented classes are HAL resources.
    /// </summary>
    public interface IResource
    {
        object State { get; set; }

        LinkCollection Links { get; set; }

        IEnumerable<IEmbeddedResource> EmbeddedResources { get; }

        string ToJson();
    }
}

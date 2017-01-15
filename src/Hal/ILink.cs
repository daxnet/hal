using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal
{
    public interface ILink
    {
        string Rel { get; set; }

        LinkItemCollection Items { get; set; }
    }
}

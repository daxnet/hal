using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal.Builders
{
    public interface IBuilder
    {
        Resource Build();
    }
}

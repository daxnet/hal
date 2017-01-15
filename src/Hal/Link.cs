using Hal.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hal
{
    public sealed class Link : ILink
    {
        public LinkItemCollection Items { get; set; }

        public string Rel { get; set; }
    }
}

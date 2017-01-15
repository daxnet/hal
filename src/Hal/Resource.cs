using Hal.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal
{
    public sealed class Resource : IResource
    {
        private static readonly List<JsonConverter> converters = new List<JsonConverter>
        {
            new LinkItemConverter(), new LinkItemCollectionConverter(), new LinkConverter(),
            new LinkCollectionConverter(), new ResourceConverter()
        };

        private readonly List<IEmbeddedResource> embeddedResources = new List<IEmbeddedResource>();

        public void AddEmbeddedResource(IEmbeddedResource embeddedResource)
        {
            this.embeddedResources.Add(embeddedResource);
        }

        public IEnumerable<IEmbeddedResource> EmbeddedResources => embeddedResources;

        public LinkCollection Links { get; set; }

        public object State { get; set; }

        public string ToJson()
        {
            var settings = new JsonSerializerSettings
            {
                Converters = converters,
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            return JsonConvert.SerializeObject(this, settings);
        }
    }
}

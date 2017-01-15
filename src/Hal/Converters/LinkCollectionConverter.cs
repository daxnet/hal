using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal.Converters
{
    public sealed class LinkCollectionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(LinkCollection);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var linkCollection = (LinkCollection)value;
            writer.WritePropertyName("_links");
            writer.WriteStartObject();
            foreach(var link in linkCollection)
            {
                serializer.Serialize(writer, link);
            }
            writer.WriteEndObject();
        }

        public override bool CanRead => false;
    }
}

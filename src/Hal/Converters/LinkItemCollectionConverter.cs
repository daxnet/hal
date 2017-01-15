using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal.Converters
{
    public sealed class LinkItemCollectionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(LinkItemCollection);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var collection = (LinkItemCollection)value;

            if (collection.Count == 1)
            {
                if (collection.EnforcingArraryConverting)
                {
                    writer.WriteStartArray();
                }

                serializer.Serialize(writer, collection.First());

                if (collection.EnforcingArraryConverting)
                {
                    writer.WriteEndArray();
                }
            }
            else
            {
                writer.WriteStartArray();
                foreach (var item in collection)
                {
                    serializer.Serialize(writer, item);
                }
                writer.WriteEndArray();
            }
        }

        public override bool CanRead => false;
    }
}

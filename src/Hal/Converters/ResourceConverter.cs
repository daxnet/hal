using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal.Converters
{
    public sealed class ResourceConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Resource);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var resource = (Resource)value;

            writer.WriteStartObject();

            if (resource.Links != null && resource.Links.Count > 0)
            {
                serializer.Serialize(writer, resource.Links);
            }

            if (resource.State != null)
            {
                //serializer.Serialize(writer, resource.State);
                var obj = JToken.FromObject(resource.State);
                if (obj.Type != JTokenType.Object)
                {
                    obj.WriteTo(writer);
                }

                var @object = (JObject)obj;
                foreach (var prop in @object.Properties())
                {
                    prop.WriteTo(writer);
                }
            }

            if (resource.EmbeddedResources != null && resource.EmbeddedResources.Count() > 0)
            {
                writer.WritePropertyName("_embedded");
                writer.WriteStartObject();
                foreach(var embeddedResource in resource.EmbeddedResources)
                {
                    writer.WritePropertyName(embeddedResource.Name);
                    if (embeddedResource.Resources != null && embeddedResource.Resources.Count > 0)
                    {
                        if (embeddedResource.Resources.Count == 1)
                        {
                            //writer.WriteStartObject();
                            var first = embeddedResource.Resources.First();
                            WriteJson(writer, first, serializer);
                            //writer.WriteEndObject();
                        }
                        else
                        {
                            writer.WriteStartArray();
                            foreach(var current in embeddedResource.Resources)
                            {
                                WriteJson(writer, current, serializer);
                            }
                            writer.WriteEndArray();
                        }
                    }
                }
                writer.WriteEndObject();
            }

            writer.WriteEndObject();
        }

        public override bool CanRead => false;
    }
}

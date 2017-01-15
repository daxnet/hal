using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hal.Converters
{
    public sealed class LinkItemConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(LinkItem);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            LinkItem li = (LinkItem)value;
            writer.WriteStartObject();
            if (!string.IsNullOrEmpty(li.Name) || serializer.NullValueHandling == NullValueHandling.Include)
            {
                writer.WritePropertyName("name");
                writer.WriteValue(li.Name);
            }

            if (!string.IsNullOrEmpty(li.Href) || serializer.NullValueHandling == NullValueHandling.Include)
            {
                writer.WritePropertyName("href");
                writer.WriteValue(li.Href);
            }

            if (li.Templated.HasValue || serializer.NullValueHandling == NullValueHandling.Include)
            {
                writer.WritePropertyName("templated");
                writer.WriteValue(li.Templated);
            }

            if (!string.IsNullOrEmpty(li.Type) || serializer.NullValueHandling == NullValueHandling.Include)
            {
                writer.WritePropertyName("type");
                writer.WriteValue(li.Type);
            }

            if (!string.IsNullOrEmpty(li.Deprecation) || serializer.NullValueHandling == NullValueHandling.Include)
            {
                writer.WritePropertyName("deprecation");
                writer.WriteValue(li.Deprecation);
            }

            if (!string.IsNullOrEmpty(li.Title) || serializer.NullValueHandling == NullValueHandling.Include)
            {
                writer.WritePropertyName("title");
                writer.WriteValue(li.Title);
            }

            if (!string.IsNullOrEmpty(li.Profile) || serializer.NullValueHandling == NullValueHandling.Include)
            {
                writer.WritePropertyName("profile");
                writer.WriteValue(li.Profile);
            }

            if (!string.IsNullOrEmpty(li.Hreflang) || serializer.NullValueHandling == NullValueHandling.Include)
            {
                writer.WritePropertyName("hreflang");
                writer.WriteValue(li.Hreflang);
            }

            if (li.Properties.Count() > 0)
            {
                foreach(var p in li.Properties)
                {
                    writer.WritePropertyName(p.Key);
                    serializer.Serialize(writer, p.Value);
                }
            }

            writer.WriteEndObject();
        }

        public override bool CanRead => false;
    }
}

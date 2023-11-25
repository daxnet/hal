﻿using System.Collections.Generic;
using Hal.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace Hal.Tests.Converters
{
    public class ResourceConverterTests
    {
        [Fact]
        public void Resource_state_serialization_should_use_current_serializer()
        {
            var serializer = JsonSerializer.Create(new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Converters = new List<JsonConverter>()
                {
                    new ResourceConverter(),
                    new LinkCollectionConverter(),
                    new LinkConverter(),
                    new LinkItemCollectionConverter(),
                    new LinkItemConverter()
                }
            });
            var resource = new Resource(new { Id = 1234 });

            var result = JToken.FromObject(resource, serializer);

            var expected = new JObject(new JProperty("id", 1234));
            Assert.True(JToken.DeepEquals(expected, result), $"Expected {result} to be equal to {expected}.");
        }

        [Fact]
        public void Resource_state_serialization_with_StringEnumConverter_should_convert_enum_as_string()
        {
            var serializer = JsonSerializer.Create(new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Converters = new List<JsonConverter>()
                {
                    new ResourceConverter(),
                    new LinkCollectionConverter(),
                    new LinkConverter(),
                    new LinkItemCollectionConverter(),
                    new LinkItemConverter(),
                    new StringEnumConverter()
                }
            });
            var resource = new Resource(new { Status = Status.Active });

            var result = JToken.FromObject(resource, serializer);

            var expected = new JObject(new JProperty("status", "Active"));
            Assert.True(JToken.DeepEquals(expected, result), $"Expected {result} to be equal to {expected}.");
        }

        private enum Status
        {
            Active = 0,
            Inactive = 1
        }
    }
}

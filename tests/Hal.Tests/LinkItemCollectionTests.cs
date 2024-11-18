using Newtonsoft.Json.Linq;
using Xunit;

namespace Hal.Tests
{
    public class LinkItemCollectionTests
    {
        [Fact]
        public void LinkItemCollectionToStringTest()
        {
            var linkItem = new LinkItem("/orders")
            {
                Name = "ea",
                Templated = true
            };
            linkItem.AddProperty("age", 10);

            var collection = new LinkItemCollection
            {
                linkItem
            };

            var expected = JToken.Parse("""
                                        {
                                          "name" : "ea",
                                          "href" : "/orders",
                                          "templated" : true,
                                          "age" : 10
                                        }
                                        """);
            var actual = JToken.Parse(collection.ToString());
            Assert.True(JToken.DeepEquals(expected, actual));
        }

        [Fact]
        public void LinkItemCollectionWithMultipleItemsToStringTest()
        {
            var linkItem1 = new LinkItem("/orders")
            {
                Name = "ea",
                Templated = true
            };
            linkItem1.AddProperty("age", 10);

            var linkItem2 = new LinkItem("/customers");

            var collection = new LinkItemCollection
            {
                linkItem1, linkItem2
            };

            var expected = JToken.Parse("""
                                        [ {
                                          "name" : "ea",
                                          "href" : "/orders",
                                          "templated" : true,
                                          "age" : 10
                                        }, {
                                          "href" : "/customers"
                                        } ]
                                        """);
            var actual = JToken.Parse(collection.ToString());
            Assert.True(JToken.DeepEquals(expected, actual));
        }
    }
}

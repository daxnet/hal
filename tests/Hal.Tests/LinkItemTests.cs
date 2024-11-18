using Newtonsoft.Json.Linq;
using Xunit;


namespace Hal.Tests
{
    public class LinkItemTests
    {
        [Fact]
        public void LinkItemToStringTest()
        {
            var linkItem = new LinkItem("/orders")
            {
                Name = "ea",
                Templated = true
            };
            
            linkItem.AddProperty("age", 10);

            var expected = JToken.Parse("""
                                        {
                                          "name" : "ea",
                                          "href" : "/orders",
                                          "templated" : true,
                                          "age" : 10
                                        }
                                        """);
            var actual = JToken.Parse(linkItem.ToString());
            Assert.True(JToken.DeepEquals(actual, expected));
        }
    }
}

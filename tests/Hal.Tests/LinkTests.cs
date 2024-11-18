using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Hal.Tests
{
    public class LinkTests
    {
        [Fact]
        public void SelfLinkToStringTest()
        {
            var selfLink = new Link("self")
            {
                Items = new LinkItemCollection
                {
                    new LinkItem ("/orders")
                }
            };

            var expected = JToken.Parse("""
                                        {
                                            "self": {
                                                "href": "/orders"
                                            }
                                        }
                                        """);
            var actual = JToken.Parse("{" + selfLink + "}");
            
            Assert.True(JToken.DeepEquals(actual, expected));
        }

        [Fact]
        public void CuriesLinkToStringTest()
        {
            var curiesLink = new Link("curies")
            {
                Items = new LinkItemCollection(true)
                {
                    new LinkItem("http://example.com/docs/rels/{rel}") { Name = "ea", Templated = true }
                }
            };

            var expected = JToken.Parse("""
                           {
                               "curies": [
                                 {
                                   "name": "ea",
                                   "href": "http://example.com/docs/rels/{rel}",
                                   "templated": true
                                 }
                               ]
                           }
                           """);
            var actual = JToken.Parse("{" + curiesLink + "}");
            Assert.True(JToken.DeepEquals(actual, expected));
        }
    }
}

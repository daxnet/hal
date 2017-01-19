using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Hal.Tests
{
    public class ResourceTests
    {
        [Fact]
        public void Example()
        {
            var links = new LinkCollection
            {
                new Link("self") { Items = new LinkItemCollection { new LinkItem("/orders") } },
                new Link("curies") { Items = new LinkItemCollection(true)
                    {
                        new LinkItem("http://example.com/docs/rels/{rel}") { Name = "ea", Templated = true }
                    }
                },
                new Link("next") { Items = new LinkItemCollection { new LinkItem("/orders?page=2") } },
                new Link("ea:find")
                {
                    Items = new LinkItemCollection { new LinkItem("/orders{?id}") { Templated = true } }
                }
            };

            var embedded = new EmbeddedResource
            {
                Name = "ea:order",
                Resources = new ResourceCollection
                {
                    new Resource(new { total = 30.00F, currency = "USD", status = "shipped" })
                    {
                        Links = new LinkCollection
                        {
                            new Link("self")
                            {
                                Items = new LinkItemCollection { new LinkItem("/orders/123") }
                            },
                            new Link("ea:basket")
                            {
                                Items = new LinkItemCollection { new LinkItem("/baskets/98712") }
                            },
                            new Link("ea:customer")
                            {
                                Items = new LinkItemCollection { new LinkItem("/customers/7809") }
                            },
                        }
                    },
                    new Resource(new { total = 20.00F, currency = "USD", status = "processing" })
                    {
                        Links = new LinkCollection
                        {
                            new Link("self")
                            {
                                Items = new LinkItemCollection { new LinkItem("/orders/124") }
                            },
                            new Link("ea:basket")
                            {
                                Items = new LinkItemCollection { new LinkItem("/baskets/97213") }
                            },
                            new Link("ea:customer")
                            {
                                Items = new LinkItemCollection { new LinkItem("/customers/12369") }
                            },
                        }
                    }
                }
            };

            var resource = new Resource(new { currentlyProcessing = 14, shippedToday = 20 })
            {
                Links = links,
                EmbeddedResources = new EmbeddedResourceCollection { embedded }
            };

            var hal = resource.ToString();
        }
    }
}

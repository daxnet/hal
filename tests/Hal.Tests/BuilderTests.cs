using System.Collections.Generic;
using System.Linq;
using Hal.Builders;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Hal.Tests
{
    public class BuilderTests
    {
        [Fact]
        public void BuilderTest()
        {
            var builder = new ResourceBuilder();
            var resource = builder
                .WithState(new { currentlyProcessing = 14, shippedToday = 20 })
                .AddSelfLink().WithLinkItem("/orders")
                .AddCuriesLink().WithLinkItem("http://example.com/docs/rels/{rel}", "ea", true)
                .AddLink("next").WithLinkItem("/orders?page=2")
                .AddLink("ea:find").WithLinkItem("/orders{?id}", templated: true)
                .AddEmbedded("ea:order", true)
                    .Resource(new ResourceBuilder()
                        .WithState(new { total = 30.00F, currency = "USD", status = "shipped" })
                        .AddSelfLink().WithLinkItem("/orders/123")
                        .AddLink("ea:basket").WithLinkItem("/baskets/98712")
                        .AddLink("ea:customer").WithLinkItem("/customers/7809"))
                    .Resource(new ResourceBuilder()
                        .WithState(new { total = 20.00F, currency = "USD", status = "processing" })
                        .AddSelfLink().WithLinkItem("/orders/124")
                        .AddLink("ea:basket").WithLinkItem("/baskets/97213")
                        .AddLink("ea:customer").WithLinkItem("/customers/12369"))
                .Build();

            var expected = JToken.Parse("""
                                        {
                                          "_links" : {
                                            "self" : {
                                              "href" : "/orders"
                                            },
                                            "curies" : [ {
                                              "name" : "ea",
                                              "href" : "http://example.com/docs/rels/{rel}",
                                              "templated" : true
                                            } ],
                                            "next" : {
                                              "href" : "/orders?page=2"
                                            },
                                            "ea:find" : {
                                              "href" : "/orders{?id}",
                                              "templated" : true
                                            }
                                          },
                                          "currentlyProcessing" : 14,
                                          "shippedToday" : 20,
                                          "_embedded" : {
                                            "ea:order" : [ {
                                              "_links" : {
                                                "self" : {
                                                  "href" : "/orders/123"
                                                },
                                                "ea:basket" : {
                                                  "href" : "/baskets/98712"
                                                },
                                                "ea:customer" : {
                                                  "href" : "/customers/7809"
                                                }
                                              },
                                              "total" : 30.0,
                                              "currency" : "USD",
                                              "status" : "shipped"
                                            }, {
                                              "_links" : {
                                                "self" : {
                                                  "href" : "/orders/124"
                                                },
                                                "ea:basket" : {
                                                  "href" : "/baskets/97213"
                                                },
                                                "ea:customer" : {
                                                  "href" : "/customers/12369"
                                                }
                                              },
                                              "total" : 20.0,
                                              "currency" : "USD",
                                              "status" : "processing"
                                            } ]
                                          }
                                        }
                                        """);
            

            var actual = JToken.Parse(resource.ToString());
            Assert.True(JToken.DeepEquals(expected, actual));
        }

        [Fact]
        public void BuilderTest2()
        {
            var state = new[] { "value1", "value2" };
            var builder = new ResourceBuilder()
                .WithState(new { records = 1 })
                .AddEmbedded("values")
                    .Resource(new ResourceBuilder().WithState(state));

            var expected = JToken.Parse("""
                                        {
                                          "records" : 1,
                                          "_embedded" : {
                                            "values" : [ "value1", "value2" ]
                                          }
                                        }
                                        """);
            var actual = JToken.Parse(builder.Build().ToString());
            Assert.True(JToken.DeepEquals(expected, actual));
        }

        [Fact]
        public void BuilderTest3()
        {
            var state = "abc";
            var builder = new ResourceBuilder()
                .WithState(state);
            var json = builder.Build().ToString();
            Assert.Equal("\"abc\"", json);
        }

        [Fact]
        public void BuilderTest4()
        {
            var firstList = Enumerable.Range(1, 5).ToArray();
            var secondList = Enumerable.Range(6, 5).ToHashSet();
            int[] empty = [];

            var builder = new ResourceBuilder();
            var resource = builder.WithState(new { valuesCount = 10, totalsCount = 5 })
                .AddCuriesLink().WithLinkItem("http://example.com/docs/rels/{rel}", "ea", true)
                .AddEmbedded("ea:values", true)
                .Resources(
                    firstList.Select(n => new ResourceBuilder()
                        .WithState(new { id = n })
                        .AddSelfLink().WithLinkItem($"/values/{n}")
                    )
                )
                .Resources(
                    secondList.Select(n => new ResourceBuilder()
                        .WithState(new { Id = n })
                        .AddSelfLink().WithLinkItem($"/values/{n}"))
                )
                .Resources(empty.Select(n => new ResourceBuilder()
                        .WithState(new { total = n })
                        .AddSelfLink().WithLinkItem($"/totals/{n}")
                    )
                ).Build();

            var expected = JToken.Parse("""
                                        {
                                          "_links" : {
                                            "curies" : [ {
                                              "name" : "ea",
                                              "href" : "http://example.com/docs/rels/{rel}",
                                              "templated" : true
                                            } ]
                                          },
                                          "valuesCount" : 10,
                                          "totalsCount" : 5,
                                          "_embedded" : {
                                            "ea:values" : [ {
                                              "_links" : {
                                                "self" : {
                                                  "href" : "/values/1"
                                                }
                                              },
                                              "id" : 1
                                            }, {
                                              "_links" : {
                                                "self" : {
                                                  "href" : "/values/2"
                                                }
                                              },
                                              "id" : 2
                                            }, {
                                              "_links" : {
                                                "self" : {
                                                  "href" : "/values/3"
                                                }
                                              },
                                              "id" : 3
                                            }, {
                                              "_links" : {
                                                "self" : {
                                                  "href" : "/values/4"
                                                }
                                              },
                                              "id" : 4
                                            }, {
                                              "_links" : {
                                                "self" : {
                                                  "href" : "/values/5"
                                                }
                                              },
                                              "id" : 5
                                            }, {
                                              "_links" : {
                                                "self" : {
                                                  "href" : "/values/6"
                                                }
                                              },
                                              "Id" : 6
                                            }, {
                                              "_links" : {
                                                "self" : {
                                                  "href" : "/values/7"
                                                }
                                              },
                                              "Id" : 7
                                            }, {
                                              "_links" : {
                                                "self" : {
                                                  "href" : "/values/8"
                                                }
                                              },
                                              "Id" : 8
                                            }, {
                                              "_links" : {
                                                "self" : {
                                                  "href" : "/values/9"
                                                }
                                              },
                                              "Id" : 9
                                            }, {
                                              "_links" : {
                                                "self" : {
                                                  "href" : "/values/10"
                                                }
                                              },
                                              "Id" : 10
                                            } ]
                                          }
                                        }
                                        """);

            var actual = JToken.Parse(resource.ToString());
            Assert.True(JToken.DeepEquals(expected, actual));
        }
        
        [Fact]
        public void BuilderTest5()
        {
            int[] empty = [];
            int[] single = [1];

            var builder = new ResourceBuilder();
            var resource = builder.WithState(new { valuesCount = 10, totalsCount = 5 })
                .AddCuriesLink().WithLinkItem("http://example.com/docs/rels/{rel}", "ea", true)
                .AddEmbedded("ea:values")
                .Resources(
                    empty.Select(n => new ResourceBuilder()
                        .WithState(new { id = n })
                        .AddSelfLink().WithLinkItem($"/values/{n}")
                    )
                )
                .AddEmbedded("ea:totals")
                .Resources(
                    single.Select(n => new ResourceBuilder()
                        .WithState(new { Id = n })
                        .AddSelfLink().WithLinkItem($"/totals/{n}"))
                ).Build();

            var expected = JToken.Parse("""
                                        {
                                          "_links" : {
                                            "curies" : [ {
                                              "name" : "ea",
                                              "href" : "http://example.com/docs/rels/{rel}",
                                              "templated" : true
                                            } ]
                                          },
                                          "valuesCount" : 10,
                                          "totalsCount" : 5,
                                          "_embedded" : {
                                            "ea:values" : [ ],
                                            "ea:totals" : {
                                              "_links" : {
                                                "self" : {
                                                  "href" : "/totals/1"
                                                }
                                              },
                                              "Id" : 1
                                            }
                                          }
                                        }
                                        """);
            
            var actual = JToken.Parse(resource.ToString());
            Assert.True(JToken.DeepEquals(expected, actual));
        }
        
        [Fact]
        public void BuilderTest6()
        {
            var builder = new ResourceBuilder();
            var resource = builder
                .WithState(new { currentlyProcessing = 14, shippedToday = 20 })
                .AddSelfLink().WithLinkItem("/orders")
                .AddCuriesLink().WithLinkItem("http://example.com/docs/rels/{rel}", "ea", true)
                .AddLink("next").WithLinkItem("/orders?page=2")
                .AddLink("ea:find").WithLinkItem("/orders{?id}", templated: true)
                .AddEmbedded("ea:order", true)
                .Resource(new ResourceBuilder()
                    .WithState(new { total = 30.00F, currency = "USD", status = "shipped" })
                    .AddSelfLink().WithLinkItem("/orders/123")
                    .AddLink("ea:basket").WithLinkItem("/baskets/98712")
                    .AddLink("ea:customer").WithLinkItem("/customers/7809"))
                .Build();

            var expected = JToken.Parse("""
                                        {
                                          "_links" : {
                                            "self" : {
                                              "href" : "/orders"
                                            },
                                            "curies" : [ {
                                              "name" : "ea",
                                              "href" : "http://example.com/docs/rels/{rel}",
                                              "templated" : true
                                            } ],
                                            "next" : {
                                              "href" : "/orders?page=2"
                                            },
                                            "ea:find" : {
                                              "href" : "/orders{?id}",
                                              "templated" : true
                                            }
                                          },
                                          "currentlyProcessing" : 14,
                                          "shippedToday" : 20,
                                          "_embedded" : {
                                            "ea:order" : [ {
                                              "_links" : {
                                                "self" : {
                                                  "href" : "/orders/123"
                                                },
                                                "ea:basket" : {
                                                  "href" : "/baskets/98712"
                                                },
                                                "ea:customer" : {
                                                  "href" : "/customers/7809"
                                                }
                                              },
                                              "total" : 30.0,
                                              "currency" : "USD",
                                              "status" : "shipped"
                                            } ]
                                          }
                                        }
                                        """);
            
            var actual = JToken.Parse(resource.ToString());
            Assert.True(JToken.DeepEquals(expected, actual));
        }
        
        [Fact]
        public void BuilderTest7()
        {
            var builder = new ResourceBuilder();
            var resource = builder
                .WithState(new { currentlyProcessing = 14, shippedToday = 20 })
                .AddSelfLink().WithLinkItem("/orders")
                .AddCuriesLink().WithLinkItem("http://example.com/docs/rels/{rel}", "ea", true)
                .AddLink("next").WithLinkItem("/orders?page=2")
                .AddLink("ea:find").WithLinkItem("/orders{?id}", templated: true)
                .AddEmbedded("ea:order")
                .Resource(new ResourceBuilder()
                    .WithState(new { total = 30.00F, currency = "USD", status = "shipped" })
                    .AddSelfLink().WithLinkItem("/orders/123")
                    .AddLink("ea:basket").WithLinkItem("/baskets/98712")
                    .AddLink("ea:customer").WithLinkItem("/customers/7809"))
                .Build();

            var expected = JToken.Parse("""
                                        {
                                          "_links" : {
                                            "self" : {
                                              "href" : "/orders"
                                            },
                                            "curies" : [ {
                                              "name" : "ea",
                                              "href" : "http://example.com/docs/rels/{rel}",
                                              "templated" : true
                                            } ],
                                            "next" : {
                                              "href" : "/orders?page=2"
                                            },
                                            "ea:find" : {
                                              "href" : "/orders{?id}",
                                              "templated" : true
                                            }
                                          },
                                          "currentlyProcessing" : 14,
                                          "shippedToday" : 20,
                                          "_embedded" : {
                                            "ea:order" : {
                                              "_links" : {
                                                "self" : {
                                                  "href" : "/orders/123"
                                                },
                                                "ea:basket" : {
                                                  "href" : "/baskets/98712"
                                                },
                                                "ea:customer" : {
                                                  "href" : "/customers/7809"
                                                }
                                              },
                                              "total" : 30.0,
                                              "currency" : "USD",
                                              "status" : "shipped"
                                            }
                                          }
                                        }
                                        """);
            
            var actual = JToken.Parse(resource.ToString());
            Assert.True(JToken.DeepEquals(expected, actual));
        }
    }
}


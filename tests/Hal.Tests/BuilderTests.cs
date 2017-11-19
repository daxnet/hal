using Hal.Builders;
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

            var json = resource.ToString();
        }

        [Fact]
        public void BuilderTest2()
        {
            var state = new string[] { "value1", "value2" };
            var builder = new ResourceBuilder()
                .WithState(new { records = 1 })
                .AddEmbedded("values")
                    .Resource(new ResourceBuilder().WithState(state));
            var json = builder.Build().ToString();
        }

        [Fact]
        public void BuilderTest3()
        {
            var state = "abc";
            var builder = new ResourceBuilder()
                .WithState(state);
            var json = builder.Build().ToString();
        }
    }
}

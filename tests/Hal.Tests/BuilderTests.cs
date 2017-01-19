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
                .AddEmbeddedResource("ea:order", new ResourceBuilder()
                    .WithState(new { total = 30.00F, currency = "USD", status = "shipped" })
                    .AddSelfLink().WithLinkItem("/orders/123")
                    .AddLink("ea:basket").WithLinkItem("/baskets/98712")
                    .AddLink("ea:customer").WithLinkItem("/customers/7809"))
                
                .Build();

            //var resource = builder.Build();
            var json = resource.ToString();
        }
    }
}

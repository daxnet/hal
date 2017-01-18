using Hal.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Hal.Tests
{
    public class BuilderTests
    {
        [Fact]
        public void BuilderTest()
        {
            var builder = new ResourceBuilder(new { currentlyProcessing = 14, shippedToday = 20 });
            builder.AddLink("self").WithLinkItem("/orders");
            var resource = builder.Build();
            var json = resource.ToString();
        }
    }
}

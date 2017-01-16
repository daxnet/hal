using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            var json = selfLink.ToString();
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

            var json = curiesLink.ToString();
        }
    }
}

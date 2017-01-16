using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Hal.Tests
{
    public class LinkItemCollectionTests
    {
        [Fact]
        public void LinkItemCollectionToStringTest()
        {
            var linkItem = new LinkItem("/orders");
            linkItem.Name = "ea";
            linkItem.Templated = true;
            linkItem.AddProperty("age", 10);

            var collection = new LinkItemCollection()
            {
                linkItem
            };

            var json = collection.ToString();
        }

        [Fact]
        public void LinkItemCollectionWithMultipleItemsToStringTest()
        {
            var linkItem1 = new LinkItem("/orders");
            linkItem1.Name = "ea";
            linkItem1.Templated = true;
            linkItem1.AddProperty("age", 10);

            var linkItem2 = new LinkItem("/customers");

            var collection = new LinkItemCollection()
            {
                linkItem1, linkItem2
            };

            var json = collection.ToString();
        }
    }
}

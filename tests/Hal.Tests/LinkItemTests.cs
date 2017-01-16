using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace Hal.Tests
{
    public class LinkItemTests
    {
        [Fact]
        public void LinkItemToStringTest()
        {
            var linkItem = new LinkItem("/orders");
            linkItem.Name = "ea";
            linkItem.Templated = true;
            linkItem.AddProperty("age", 10);
            var str = linkItem.ToString();
        }
    }
}

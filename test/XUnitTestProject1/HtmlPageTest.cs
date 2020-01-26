using Coroiu.Leet.Crawler.Net;
using FluentAssertions;
using System;
using Xunit;

namespace Coroiu.Leet.Crawlet.Net.Test
{
    public class HtmlPageTest
    {
        [Fact]
        public void Uris_EmptyContent_ReturnsNoUris()
        {
            var page = new HtmlPage(new Uri("fake://a"), "");

            var uris = page.Uris;

            uris.Should().BeEmpty();
        }
    }
}

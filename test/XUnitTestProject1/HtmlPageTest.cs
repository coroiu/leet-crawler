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

        [Fact]
        public void Uris_AnAnchor_ReturnsUri()
        {
            var page = new HtmlPage(new Uri("fake://a"), "<a href=\"http://test.com\">test link</a>");

            var uris = page.Uris;

            uris.Should().HaveCount(1)
                .And.Contain(new Uri("http://test.com"));
        }

        [Fact]
        public void Uris_MultipleAnchors_ReturnsUris()
        {
            var page = new HtmlPage(new Uri("fake://a"), "<html><body><a href=\"http://test.com\">test link</a>asdasdasd<a href=\"http://test2.com\">test link2</a></body><html>");

            var uris = page.Uris;

            uris.Should().HaveCount(2)
                .And.Contain(new[] { new Uri("http://test.com"), new Uri("http://test2.com") });
        }
    }
}

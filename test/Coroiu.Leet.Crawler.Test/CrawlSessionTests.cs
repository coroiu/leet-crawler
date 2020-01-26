using Coroiu.Leet.Crawler.Net;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Coroiu.Leet.Crawler.Test
{
    public class CrawlSessionTest
    {
        private MockBrowser browser;
        private ICrawlSession crawlSession;

        public CrawlSessionTest()
        {
        }

        [Fact]
        public async void Crawl_SinglePageSite_NavigatesStartPage()
        {
            var startUri = new MockUri("a");
            SetupSession(startUri, new MockPage());

            await crawlSession.Crawl();

            browser.NavigatedUris.Should().HaveCount(1)
                .And.Contain(startUri);
        }

        private void SetupSession(Uri startUri, IPage startPage) =>
            SetupSession(startUri, new Dictionary<Uri, IPage>()
            {
                { startUri, startPage }
            });

        private void SetupSession(Uri startUri, IDictionary<Uri, IPage> pageMap)
        {
            browser = new MockBrowser(pageMap);
            crawlSession = new CrawlSession(startUri, browser);
        }
    }
}

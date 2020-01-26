using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Coroiu.Leet.Crawler.Test
{
    public class CrawlSessionTest
    {
        private static readonly Uri StartPage = new MockUri("a");

        private IDictionary<Uri, IEnumerable<Uri>> pageMap;
        private MockBrowser browser;
        private ICrawlSession crawlSession;

        public CrawlSessionTest()
        {
            pageMap = new Dictionary<Uri, IEnumerable<Uri>>();
            browser = new MockBrowser(pageMap);
            crawlSession = new CrawlSession(StartPage, browser);
        }

        [Fact]
        public async void Crawl_SinglePageSite_CrawlsStartPage()
        {
            pageMap.Add(StartPage, Enumerable.Empty<Uri>());

            await crawlSession.Crawl();

            browser.NavigatedUris.Should().HaveCount(1)
                .And.Contain(StartPage);
        }
    }
}

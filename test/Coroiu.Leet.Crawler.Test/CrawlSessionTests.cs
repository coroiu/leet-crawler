using Coroiu.Leet.Crawler.Net;
using Coroiu.Leet.Crawler.Storage;
using Coroiu.Leet.Crawler.Storage.InMemory;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Coroiu.Leet.Crawler.Test
{
    public class CrawlSessionTest
    {
        private InMemoryStorage storage;
        private MockBrowser browser;
        private ICrawlSession crawlSession;

        public CrawlSessionTest()
        {
            storage = new InMemoryStorage();
        }

        [Fact]
        public async void Crawl_SinglePageSite_NavigatesStartPage()
        {
            var startUri = new MockUri("a");
            SetupSession(new MockPage(startUri));

            await crawlSession.Crawl();

            browser.NavigatedUris.Should().HaveCount(1)
                .And.Contain(startUri);
        }

        [Fact]
        public async void Crawl_SinglePageSite_SavesPageContent()
        {
            var startUri = new MockUri("a");
            const string content = "start page content";
            SetupSession(new MockPage(startUri, content));

            await crawlSession.Crawl();

            var savedContent = await storage.Read(storage.Entries.First());
            savedContent.Should().Be(content);
        }

        private void SetupSession(IPage startPage) =>
            SetupSession(startPage.Uri, new Dictionary<Uri, IPage>()
            {
                { startPage.Uri, startPage }
            });

        private void SetupSession(Uri startUri, IDictionary<Uri, IPage> pageMap)
        {
            browser = new MockBrowser(pageMap);
            crawlSession = new CrawlSession(startUri, browser, storage);
        }
    }
}

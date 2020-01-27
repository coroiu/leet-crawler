using Coroiu.Leet.Crawler.Net;
using Coroiu.Leet.Crawler.Storage.InMemory;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            storage.Entries.Should().Contain(new Uri("", UriKind.Relative));
            savedContent.Should().Be(content);
        }

        [Fact]
        public async void Crawl_SingleFileSite_SavesFileContent()
        {
            var startUri = new MockUri("a");
            byte[] content = { 0, 1, 2, 3, 4 };
            SetupSession(new MockFile(startUri, content));

            await crawlSession.Crawl();

            var savedContent = await storage.Read(storage.Entries.First());
            storage.Entries.Should().Contain(new Uri("", UriKind.Relative));
            savedContent.Should().Be(content);
        }

        [Fact]
        public async void Crawl_TwoPageSiteWithRelativeUri_NavigatesAllPages()
        {
            var startUri = new MockUri("");
            SetupSession(startUri, new[]
            {
                new MockPage(startUri, new[] { new Uri("/a", UriKind.Relative) }),
                new MockPage(new MockUri("a")),
            });

            await crawlSession.Crawl();

            browser.NavigatedUris.Should().HaveCount(2)
                .And.Contain(new[] {
                    new MockUri(""),
                    new MockUri("a"),
                });
        }

        [Fact]
        public async void Crawl_MultiPageSite_NavigatesAllPages()
        {
            var startUri = new MockUri("a");
            SetupSession(startUri, new[]
            {
                new MockPage(startUri, new[] { new MockUri("a/a"), new MockUri("a/b") }),
                new MockPage(new MockUri("a/a")),
                new MockPage(new MockUri("a/b"), new[] { new MockUri("a/b/a")}),
                new MockPage(new MockUri("a/b/a"))
            });

            await crawlSession.Crawl();

            browser.NavigatedUris.Should().HaveCount(4)
                .And.Contain(new[] { 
                    new MockUri("a"), 
                    new MockUri("a/a"), 
                    new MockUri("a/b"), 
                    new MockUri("a/b/a")
                });
        }

        [Fact]
        public async void Crawl_TwoPathsToSamePage_NavigatesAllPages()
        {
            var startUri = new MockUri("a");
            SetupSession(startUri, new[]
            {
                new MockPage(startUri, new[] { new MockUri("a/a"), new MockUri("a/b") }),
                new MockPage(new MockUri("a/a"), new[] { new MockUri("a/a/a")}),
                new MockPage(new MockUri("a/b"), new[] { new MockUri("a/a/a")}),
                new MockPage(new MockUri("a/a/a"))
            });

            await crawlSession.Crawl();

            browser.NavigatedUris.Should().HaveCount(4)
                .And.Contain(new[] {
                    new MockUri("a"),
                    new MockUri("a/a"),
                    new MockUri("a/b"),
                    new MockUri("a/a/a")
                });
        }

        [Fact]
        public async void Crawl_PageLoop_NavigatesAllPages()
        {
            var startUri = new MockUri("a");
            SetupSession(startUri, new[]
            {
                new MockPage(startUri, new[] { new MockUri("a/a") }),
                new MockPage(new MockUri("a/a"), new[] { startUri })
            });

            await crawlSession.Crawl();

            browser.NavigatedUris.Should().HaveCount(2)
                .And.Contain(new[] {
                    new MockUri("a"),
                    new MockUri("a/a"),
                });
        }

        [Fact]
        public async void Completed_MultiPageSite_GraduallyAddsCrawledSites()
        {
            // Arrange
            var startUri = new MockUri("a");
            SetupSession(startUri, new[]
            {
                new MockPage(startUri, new[] { new MockUri("a/a"), new MockUri("a/b") }),
                new MockPage(new MockUri("a/a")),
                new MockPage(new MockUri("a/b"), new[] { new MockUri("a/b/a")}),
                new MockPage(new MockUri("a/b/a"))
            }, true);

            // Step 1
            _ = crawlSession.Crawl();

            crawlSession.Completed.Should().BeEmpty();

            // Step 2
            browser.ReleaseDownloads();

            await Task.Delay(10);
            crawlSession.Completed.Should().HaveCount(1)
                .And.Contain(startUri);

            // Step 3
            browser.ReleaseDownloads();

            await Task.Delay(10);
            crawlSession.Completed.Should().HaveCount(3)
                .And.Contain(new[] { startUri, new MockUri("a/a"), new MockUri("a/b") });

            // Step 4
            browser.ReleaseDownloads();

            await Task.Delay(10);
            crawlSession.Completed.Should().HaveCount(4)
                .And.Contain(new[] { startUri, new MockUri("a/a"), new MockUri("a/b"), new MockUri("a/b/a") });
        }

        [Fact]
        public async void Downloading_MultiPageSite_UpdatesCurrentlyDownloadingPages()
        {
            // Arrange
            var startUri = new MockUri("a");
            SetupSession(startUri, new[]
            {
                new MockPage(startUri, new[] { new MockUri("a/a"), new MockUri("a/b") }),
                new MockPage(new MockUri("a/a")),
                new MockPage(new MockUri("a/b"), new[] { new MockUri("a/b/a")}),
                new MockPage(new MockUri("a/b/a"))
            }, true);

            // Step 1
            _ = crawlSession.Crawl();

            crawlSession.Downloading.Should().HaveCount(1)
                .And.Contain(startUri);

            // Step 2
            browser.ReleaseDownloads();

            await Task.Delay(10);
            crawlSession.Downloading.Should().HaveCount(2)
                .And.Contain(new[] { new MockUri("a/a"), new MockUri("a/b") });

            // Step 3
            browser.ReleaseDownloads();

            await Task.Delay(10);
            crawlSession.Downloading.Should().HaveCount(1)
                .And.Contain(new[] { new MockUri("a/b/a") });

            // Step 4
            browser.ReleaseDownloads();

            await Task.Delay(10);
            crawlSession.Downloading.Should().BeEmpty();
        }

        private void SetupSession(IResource startResource) =>
            SetupSession(startResource.Uri, new[] { startResource });

        private void SetupSession(Uri startUri, IEnumerable<IResource> resources, bool blockDownloads = false)
        {
            browser = new MockBrowser(resources, blockDownloads);
            crawlSession = new CrawlSession(startUri, browser, storage);
        }
    }
}

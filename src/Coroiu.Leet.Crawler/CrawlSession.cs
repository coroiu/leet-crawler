using Coroiu.Leet.Crawler.Net;
using Coroiu.Leet.Crawler.Storage;
using System;
using System.Threading.Tasks;

namespace Coroiu.Leet.Crawler
{
    internal class CrawlSession : ICrawlSession
    {
        private readonly Uri startUri;
        private readonly IBrowser browser;
        private readonly IStorage storage;

        public CrawlSession(Uri startUri, IBrowser browser, IStorage storage)
        {
            this.startUri = startUri;
            this.browser = browser;
            this.storage = storage;
        }

        public async Task Crawl()
        {
            var page = await browser.DownloadPage(startUri);
            await storage.Save(startUri, page.Content);
        }
    }
}

using Coroiu.Leet.Crawler.Net;
using Coroiu.Leet.Crawler.Storage;
using System;
using System.Linq;
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

        public Task Crawl()
        {
            return Crawl(startUri);
        }

        private async Task Crawl(Uri uri)
        {
            var page = await browser.DownloadPage(uri);
            var tasks = page.Uris
                .Select(uri => Crawl(uri))
                .Append(storage.Save(page.Uri, page.Content));

            await Task.WhenAll(tasks);
        }
    }
}

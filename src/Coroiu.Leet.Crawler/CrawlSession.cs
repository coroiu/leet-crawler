using Coroiu.Leet.Crawler.Net;
using Coroiu.Leet.Crawler.Storage;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Coroiu.Leet.Crawler
{
    internal class CrawlSession : ICrawlSession
    {
        private readonly Uri startUri;
        private readonly IBrowser browser;
        private readonly IStorage storage;
        private readonly ConcurrentBag<Uri> visited;

        public CrawlSession(Uri startUri, IBrowser browser, IStorage storage)
        {
            this.startUri = startUri;
            this.browser = browser;
            this.storage = storage;
            visited = new ConcurrentBag<Uri>();
        }

        public Task Crawl()
        {
            return Crawl(startUri);
        }

        private async Task Crawl(Uri uri)
        {
            visited.Add(uri);
            var page = await browser.DownloadPage(uri);
            var tasks = page.Uris
                .Except(visited)
                .Select(uri => Crawl(uri))
                .Append(storage.Save(page.Uri, page.Content));

            await Task.WhenAll(tasks);
        }
    }
}

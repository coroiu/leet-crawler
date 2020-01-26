using Coroiu.Leet.Crawler.Net;
using Coroiu.Leet.Crawler.Storage;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coroiu.Leet.Crawler
{
    internal class CrawlSession : ICrawlSession
    {
        public IEnumerable<Uri> Completed => completed;

        private readonly Uri startUri;
        private readonly IBrowser browser;
        private readonly IStorage storage;
        private readonly ConcurrentBag<Uri> visited;
        private readonly ConcurrentBag<Uri> completed;

        public CrawlSession(Uri startUri, IBrowser browser, IStorage storage)
        {
            this.startUri = startUri;
            this.browser = browser;
            this.storage = storage;
            visited = new ConcurrentBag<Uri>();
            completed = new ConcurrentBag<Uri>();
        }

        public Task Crawl()
        {
            return Crawl(startUri);
        }

        private async Task Crawl(Uri uri)
        {
            visited.Add(uri);
            var page = await browser.DownloadPage(uri);
            completed.Add(uri);
            var tasks = page.Uris
                .Except(visited)
                .Select(u => Crawl(u))
                .Append(storage.Save(page.Uri, page.Content));

            await Task.WhenAll(tasks);
        }
    }
}

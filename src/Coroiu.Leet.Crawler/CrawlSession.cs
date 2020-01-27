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
        public IEnumerable<Uri> Downloading => downloading;

        public IEnumerable<Uri> Completed => completed;

        private readonly Uri startUri;
        private readonly IBrowser browser;
        private readonly IStorage storage;
        private readonly object visitedAddLock;
        private readonly ConcurrentBag<Uri> visited;
        private readonly ConcurrentBag<Uri> completed;
        private readonly BlockingList<Uri> downloading;

        public CrawlSession(Uri startUri, IBrowser browser, IStorage storage)
        {
            this.startUri = startUri;
            this.browser = browser;
            this.storage = storage;
            visitedAddLock = new object();
            visited = new ConcurrentBag<Uri>();
            completed = new ConcurrentBag<Uri>();
            downloading = new BlockingList<Uri>();
        }

        public Task Crawl()
        {
            visited.Add(startUri);
            return Crawl(startUri);
        }

        private async Task Crawl(Uri uri)
        {
            downloading.Add(uri);
            var page = await browser.DownloadPage(uri);
            downloading.Remove(uri);
            completed.Add(uri);

            IEnumerable<Uri> newUris;
            lock (visitedAddLock)
            {
                newUris = page.Uris.Except(visited).Where(IsValidUri).ToList();
                foreach (var u in newUris)
                    visited.Add(u);
            }

            var tasks = newUris
                .Select(u => Crawl(u))
                .Append(storage.Save(page.Uri, page.Content));

            await Task.WhenAll(tasks);
        }

        private bool IsValidUri(Uri uri)
        {
            return uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps;
        }
    }
}

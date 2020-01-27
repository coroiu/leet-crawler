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
            var resource = await browser.DownloadPage(uri);
            downloading.Remove(uri);
            completed.Add(uri);
            
            if (resource is IPage page)
            {
                IEnumerable<Uri> newUris;
                lock (visitedAddLock)
                {
                    newUris = page.Uris
                        .Where(IsValidUri)
                        .Select(AbsoluteUri)
                        .Except(visited)
                        .Distinct()
                        .ToList();
                    foreach (var u in newUris)
                        visited.Add(u);
                }

                var tasks = newUris
                    .Select(u => Crawl(AbsoluteUri(u)))
                    .Append(storage.Save(startUri.MakeRelativeUri(page.Uri), page.Content));

                await Task.WhenAll(tasks);
            } 
            else if (resource is IFile file)
            {
                await storage.Save(startUri.MakeRelativeUri(file.Uri), file.Content);
            }
        }

        private bool IsValidUri(Uri uri)
        {
            return uri.IsAbsoluteUri && uri.Scheme == startUri.Scheme && uri.Host == startUri.Host ||
                !uri.IsAbsoluteUri;
        }

        private Uri AbsoluteUri(Uri uri)
        {
            if (uri.IsAbsoluteUri)
                return uri;

            return new Uri(startUri, uri);
        }
    }
}

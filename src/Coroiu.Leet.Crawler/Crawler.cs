using Coroiu.Leet.Crawler.Net;
using Coroiu.Leet.Crawler.Storage;
using System;

namespace Coroiu.Leet.Crawler
{
    public class Crawler
    {
        private readonly IBrowser browser;
        private readonly IStorage storage;

        public Crawler(IBrowser browser, IStorage storage)
        {
            this.browser = browser;
            this.storage = storage;
        }

        public ICrawlSession CreateSession(Uri startUri)
        {
            return new CrawlSession(startUri, browser, storage);
        }
    }
}

using Coroiu.Leet.Crawler.Net;
using Coroiu.Leet.Crawler.Storage;
using System;

namespace Coroiu.Leet.Crawler
{
    public class Crawler
    {
        public Crawler()
        {
        }

        public ICrawlSession CreateSession(Uri startUri, IBrowser browser, IStorage storage)
        {
            return new CrawlSession(startUri, browser, storage);
        }
    }
}

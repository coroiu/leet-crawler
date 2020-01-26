using Coroiu.Leet.Crawler.Net;
using System;
using System.Threading.Tasks;

namespace Coroiu.Leet.Crawler
{
    internal class CrawlSession : ICrawlSession
    {
        private readonly Uri startUri;
        private readonly IBrowser browser;

        public CrawlSession(Uri startUri, IBrowser browser)
        {
            this.startUri = startUri;
            this.browser = browser;
        }

        public async Task Crawl()
        {
            await browser.DownloadPage(startUri);
        }
    }
}

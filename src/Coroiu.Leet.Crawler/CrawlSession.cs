using Coroiu.Leet.Crawler.Net;
using System;
using System.Threading.Tasks;

namespace Coroiu.Leet.Crawler
{
    internal class CrawlSession : ICrawlSession
    {
        private readonly Uri startPage;
        private readonly IBrowser browser;

        public CrawlSession(Uri startPage, IBrowser browser)
        {
            this.startPage = startPage;
            this.browser = browser;
        }

        public async Task Crawl()
        {
            await browser.DownloadPage(startPage);
        }
    }
}

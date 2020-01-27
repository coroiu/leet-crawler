using Coroiu.Leet.Crawler.Net;
using Coroiu.Leet.Crawler.Storage.FileSystem;
using System;
using System.Threading.Tasks;

namespace Coroiu.Leet.Crawler.Gui
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var startUri = new Uri("https://tretton37.com");
            var browser = new HttpBrowser();
            var storage = new FileStorage(new Uri(Environment.CurrentDirectory + "/downloaded/"));
            var crawler = new Crawler(browser, storage);

            var session = crawler.CreateSession(startUri);

            await storage.Clear();
            await session.Crawl();
        }
    }
}

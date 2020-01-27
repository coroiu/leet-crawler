using Coroiu.Leet.Crawler.Net;
using Coroiu.Leet.Crawler.Storage.FileSystem;
using Coroiu.Leet.Crawler.Storage.InMemory;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Coroiu.Leet.Crawler.Gui
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var running = true;

            var startUri = new Uri("https://tretton37.com");
            var browser = new HttpBrowser();
            var storage = new FileStorage(new Uri(Environment.CurrentDirectory + "/downloaded/"));
            var crawler = new Crawler(browser, storage);

            var session = crawler.CreateSession(startUri);

            Console.WriteLine("Press any key to start crawling...");
            Console.ReadKey();

            Console.Clear();

            _ = Task.Run(() =>
            {
                while (running)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine($"Currently downloading: {session.Downloading.Count()}    ");
                    Console.WriteLine($"Completed: {session.Completed.Count()}    ");
                    Task.Delay(500);
                }
            });

            await storage.Clear();
            await session.Crawl();

            running = false;

            Console.WriteLine();
            Console.WriteLine("Crawl finished succesfully. Press any key to exit...");
            Console.ReadKey();
        }
    }
}

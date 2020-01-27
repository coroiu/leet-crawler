using Coroiu.Leet.Crawler.Net;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Coroiu.Leet.Crawler.Test
{
    internal class MockBrowser : IBrowser
    {
        public IEnumerable<Uri> NavigatedUris => navigatedUris;

        private readonly IDictionary<Uri, IPage> pageMap;
        private readonly IList<Uri> navigatedUris;
        private readonly bool blockDownloads;
        private readonly EventWaitHandle downloadBlockHandle;
        private readonly object blockedThreadsLock;

        private int blockedThreads = 0;

        public MockBrowser(IEnumerable<IPage> pages, bool blockDownloads = false)
        {
            pageMap = pages.ToDictionary(p => p.Uri);
            navigatedUris = new List<Uri>();
            this.blockDownloads = blockDownloads;
            downloadBlockHandle = new AutoResetEvent(false);
            blockedThreadsLock = new object();
        }

        public void ReleaseDownloads()
        {
            lock (blockedThreadsLock)
            {
                for (var i = blockedThreads; i != 0; --i)
                    downloadBlockHandle.Set();
                blockedThreads = 0;
            }
        }

        public Task<IResource> DownloadPage(Uri uri)
        {
            return Task.Run<IResource>(() =>
            {
                navigatedUris.Add(uri);

                if (!pageMap.TryGetValue(uri, out var page))
                {
                    throw new PageNotFoundException(uri);
                }

                Console.WriteLine($"Going to wait");

                if (blockDownloads)
                    BlockThread();

                Console.WriteLine($"Wait released");

                return page;
            });
        }
        
        private void BlockThread()
        {
            lock (blockedThreadsLock)
            {
                blockedThreads++;
            }

            downloadBlockHandle.WaitOne();
        }
    }
}

using Coroiu.Leet.Crawler.Net;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Coroiu.Leet.Crawler.Test
{
    internal class MockBrowser : IBrowser
    {
        public IEnumerable<Uri> NavigatedUris => navigatedUris;

        private readonly IDictionary<Uri, IPage> pageMap;
        private readonly IList<Uri> navigatedUris;

        public MockBrowser(IEnumerable<IPage> pages)
        {
            pageMap = pages.ToDictionary(p => p.Uri);
            navigatedUris = new List<Uri>();
        }

        public Task<IPage> DownloadPage(Uri uri)
        {
            navigatedUris.Add(uri);

            if (!pageMap.TryGetValue(uri, out var page))
            {
                throw new PageNotFoundException(uri);
            }

            return Task.FromResult(page);
        }
    }
}

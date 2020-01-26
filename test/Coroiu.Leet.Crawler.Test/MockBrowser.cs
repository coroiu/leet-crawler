using Coroiu.Leet.Crawler.Net;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Coroiu.Leet.Crawler.Test
{
    internal class MockBrowser : IBrowser
    {
        public IEnumerable<Uri> NavigatedUris => navigatedUris;

        private readonly IDictionary<Uri, IPage> pageMap;
        private readonly IList<Uri> navigatedUris;

        public MockBrowser(IDictionary<Uri, IPage> pageMap)
        {
            this.pageMap = pageMap;
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

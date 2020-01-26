using Coroiu.Leet.Crawler.Net;
using System;
using System.Collections.Generic;

namespace Coroiu.Leet.Crawler.Test
{
    internal class MockPage : IPage
    {
        public IEnumerable<Uri> Uris { get; }

        public MockPage(IEnumerable<Uri> uris)
        {
            Uris = uris;
        }
    }
}

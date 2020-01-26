using Coroiu.Leet.Crawler.Net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Coroiu.Leet.Crawler.Test
{
    internal class MockPage : IPage
    {
        public IEnumerable<Uri> Uris { get; }

        public string Content { get; }

        public MockPage() : this("")
        {
        }

        public MockPage(string content) : this(content, Enumerable.Empty<Uri>()) 
        {
        }

        public MockPage(string content, IEnumerable<Uri> uris)
        {
            Uris = uris;
            Content = content;
        }
    }
}

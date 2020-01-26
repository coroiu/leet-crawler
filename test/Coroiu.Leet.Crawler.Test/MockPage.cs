using Coroiu.Leet.Crawler.Net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Coroiu.Leet.Crawler.Test
{
    internal class MockPage : IPage
    {
        public Uri Uri { get; }

        public IEnumerable<Uri> Uris { get; }

        public string Content { get; }

        public MockPage(Uri uri) : this(uri, "")
        {
        }

        public MockPage(Uri uri, string content) : this(uri, content, Enumerable.Empty<Uri>()) 
        {
        }

        public MockPage(Uri uri, string content, IEnumerable<Uri> uris)
        {
            Uri = uri;
            Uris = uris;
            Content = content;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coroiu.Leet.Crawler.Net
{
    internal class HtmlPage : IPage
    {
        public Uri Uri { get; }

        public string Content { get; }

        public IEnumerable<Uri> Uris => Enumerable.Empty<Uri>();

        public HtmlPage(Uri uri, string content)
        {
            Uri = uri;
            Content = content;
        }
    }
}

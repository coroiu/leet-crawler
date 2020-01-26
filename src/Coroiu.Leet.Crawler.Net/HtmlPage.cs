using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Coroiu.Leet.Crawler.Net
{
    internal class HtmlPage : IPage
    {
        public Uri Uri { get; }

        public string Content { get; }

        public IEnumerable<Uri> Uris => ExtractAnchors(Content);

        public HtmlPage(Uri uri, string content)
        {
            Uri = uri;
            Content = content;
        }

        private static IEnumerable<Uri> ExtractAnchors(string content)
        {
            return Regex.Matches(content, "href\\s*=\\s*\"(?<url>.*?)\"")
                .Select(m => new Uri(m.Groups["url"].Value));
        }
    }
}

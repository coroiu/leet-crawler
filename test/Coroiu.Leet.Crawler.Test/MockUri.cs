using System;


namespace Coroiu.Leet.Crawler.Test
{
    internal class MockUri : Uri
    {
        private static readonly Uri FakeHost = new Uri("http://fake.com");

        public Uri RelativePath { get; }

        public MockUri(string path) : base(FakeHost, new Uri(path, UriKind.Relative))
        {
            RelativePath = new Uri(path, UriKind.Relative);
        }
    }
}

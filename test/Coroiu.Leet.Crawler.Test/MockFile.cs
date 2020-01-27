using Coroiu.Leet.Crawler.Net;
using System;

namespace Coroiu.Leet.Crawler.Test
{
    internal class MockFile : IFile
    {
        public Uri Uri { get; }

        public byte[] Content { get; }

        public MockFile(Uri uri) : this(uri, new byte[0])
        {
        }

        public MockFile(Uri uri, byte[] content)
        {
            Uri = uri;
            Content = content;
        }
    }
}

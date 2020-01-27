using System;

namespace Coroiu.Leet.Crawler.Net
{
    public class File : IFile
    {
        public Uri Uri { get; }

        public byte[] Content { get; }

        public File(Uri uri, byte[] content)
        {
            Uri = uri;
            Content = content;
        }
    }
}

using System;

namespace Coroiu.Leet.Crawler.Net
{
    public class PageNotFoundException : Exception
    {
        public PageNotFoundException(Uri uri) : this(uri, null)
        {
        }

        public PageNotFoundException(Uri uri, Exception innerException) : base($"Page '{uri}' not found.", innerException)
        {
        }
    }
}

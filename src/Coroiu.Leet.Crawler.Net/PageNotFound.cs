using System;

namespace Coroiu.Leet.Crawler.Net
{
    public class PageNotFound : Exception
    {
        public PageNotFound(Uri uri) : this(uri, null)
        {
        }

        public PageNotFound(Uri uri, Exception innerException) : base($"Page '{uri.AbsoluteUri}' not found.", innerException)
        {
        }
    }
}

using System;

namespace Coroiu.Leet.Crawler.Storage
{
    public class EntryAlreadyExistsException : Exception
    {
        public EntryAlreadyExistsException(Uri uri) : this(uri, null)
        {
        }

        public EntryAlreadyExistsException(Uri uri, Exception innerException) : base($"Entry '{uri.AbsoluteUri}' already exists.", innerException)
        {
        }
    }
}

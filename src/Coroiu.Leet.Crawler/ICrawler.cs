using System;

namespace Coroiu.Leet.Crawler
{
    public interface ICrawler
    {
        ICrawlSession CreateSession(Uri uri);
    }
}

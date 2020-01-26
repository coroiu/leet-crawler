using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coroiu.Leet.Crawler
{
    public interface ICrawlSession
    {
        IEnumerable<Uri> Downloading { get; }

        IEnumerable<Uri> Completed { get; }

        Task Crawl();
    }
}

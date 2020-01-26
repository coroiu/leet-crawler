using System;
using System.Collections.Generic;

namespace Coroiu.Leet.Crawler.Net
{
    public interface IPage
    {
        IEnumerable<Uri> Uris { get; }

        string Content { get; }
    }
}

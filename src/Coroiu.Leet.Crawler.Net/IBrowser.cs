using System;
using System.Threading.Tasks;

namespace Coroiu.Leet.Crawler.Net
{
    public interface IBrowser
    {
        Task<IPage> DownloadPage(Uri uri);
    }
}

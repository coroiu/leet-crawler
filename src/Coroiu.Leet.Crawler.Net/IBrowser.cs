using System;
using System.Threading.Tasks;

namespace Coroiu.Leet.Crawler.Net
{
    public interface IBrowser
    {
        Task<IResource> DownloadPage(Uri uri);
    }
}

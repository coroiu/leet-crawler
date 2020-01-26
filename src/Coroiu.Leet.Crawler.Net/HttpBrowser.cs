using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coroiu.Leet.Crawler.Net
{
    public class HttpBrowser : IBrowser
    {
        public Task<IPage> DownloadPage(Uri uri)
        {
            throw new NotImplementedException();
        }
    }
}

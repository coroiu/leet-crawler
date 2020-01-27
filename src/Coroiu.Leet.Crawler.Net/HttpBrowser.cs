using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Coroiu.Leet.Crawler.Net
{
    public class HttpBrowser : IBrowser
    {
        private readonly HttpClient httpClient;

        public HttpBrowser()
        {
            httpClient = new HttpClient();
        }

        public async Task<IResource> DownloadPage(Uri uri)
        {
            var response = await httpClient.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();

            return new HtmlPage(uri, content);
        }
    }
}

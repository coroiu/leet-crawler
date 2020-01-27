using System;
using System.Net.Http;
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

            if (response.Content.Headers.ContentType.MediaType.Contains("html"))
            {
                return new HtmlPage(uri, 
                    await response.Content.ReadAsStringAsync());
            }

            return new File(uri,
                await response.Content.ReadAsByteArrayAsync());
        }
    }
}

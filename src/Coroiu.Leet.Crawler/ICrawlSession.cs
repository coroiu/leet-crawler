using System.Threading.Tasks;

namespace Coroiu.Leet.Crawler
{
    public interface ICrawlSession
    {
        Task Crawl();
    }
}

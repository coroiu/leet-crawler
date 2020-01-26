using System;
using System.Threading.Tasks;

namespace Coroiu.Leet.Crawler.Storage
{
    public interface IStorage
    {
        Task Clear();

        Task Save(Uri uri, string content);
    }
}

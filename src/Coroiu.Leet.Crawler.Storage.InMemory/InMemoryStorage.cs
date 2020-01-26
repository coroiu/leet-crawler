using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coroiu.Leet.Crawler.Storage.InMemory
{
    public class InMemoryStorage : IStorage
    {
        public IEnumerable<Uri> Entries => entries.Keys;

        private IDictionary<Uri, string> entries { get; }

        public InMemoryStorage()
        {
            entries = new Dictionary<Uri, string>();
        }

        public string Read(Uri uri)
        {
            throw new NotImplementedException();
        }

        public Task Save(Uri uri, string content)
        {
            entries.Add(uri, content);

            return Task.CompletedTask;
        }
    }
}

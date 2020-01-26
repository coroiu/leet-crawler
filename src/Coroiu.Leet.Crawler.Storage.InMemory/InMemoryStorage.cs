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

        public Task<string> Read(Uri uri)
        {
            return Task.FromResult(entries[uri]);
        }

        public Task Clear()
        {
            entries.Clear();

            return Task.CompletedTask;
        }

        public Task Save(Uri uri, string content)
        {
            if (entries.ContainsKey(uri))
                throw new EntryAlreadyExistsException(uri);

            entries.Add(uri, content);

            return Task.CompletedTask;
        }
    }
}

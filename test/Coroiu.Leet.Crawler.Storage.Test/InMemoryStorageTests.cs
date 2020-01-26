using Coroiu.Leet.Crawler.Storage.InMemory;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Coroiu.Leet.Crawler.Storage.Test
{
    public class InMemoryStorageTests
    {
        private InMemoryStorage storage;

        public InMemoryStorageTests()
        {
            storage = new InMemoryStorage();
        }

        [Fact]
        public void Entries_NoEntriesSaved_ReturnsNoEntries()
        {
            storage.Entries.Should().BeEmpty();
        }

        [Fact]
        public void Save_TwoEntriesSaved_EntriesContainEntries()
        {
            var entries = new[] { new Uri("fake://a"), new Uri("fake://b") };

            foreach (var entry in entries)
            {
                storage.Save(entry, "");
            }

            storage.Entries.Should().ContainInOrder(entries);
        }

        [Fact]
        public void Save_EntryAlreadySaved_ThrowsEntryAlreadyExists()
        {
            var entry = new Uri("fake://a");
            storage.Save(entry, "");

            Func<Task> x = () => storage.Save(entry, "");

            x.Should().Throw<EntryAlreadyExistsException>();
        }

        [Fact]
        public async void Read_OneSavedEntry_ReturnsContent()
        {
            var entry = new Uri("fake://a");
            const string content = "some content";
            await storage.Save(entry, content);

            var result = await storage.Read(entry);

            result.Should().Be(content);
        }

        [Fact]
        public async void Clear_OneSavedEntryCleared_EntriesIsEmpty()
        {
            var entry = new Uri("fake://a");
            await storage.Save(entry, "");

            await storage.Clear();

            storage.Entries.Should().BeEmpty();
        }
    }
}

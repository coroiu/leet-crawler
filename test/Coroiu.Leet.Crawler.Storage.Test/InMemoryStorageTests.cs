using Coroiu.Leet.Crawler.Storage.InMemory;
using FluentAssertions;
using System;
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
        public void Entries_TwoEntriesSaved_ReturnsEntries()
        {
            var entries = new[] { new Uri("fake://a"), new Uri("fake://b") };

            foreach (var entry in entries)
            {
                storage.Save(entry, "");
            }

            storage.Entries.Should().ContainInOrder(entries);
        }
    }
}

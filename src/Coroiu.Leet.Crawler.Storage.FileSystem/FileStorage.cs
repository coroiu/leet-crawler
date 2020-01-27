﻿using System;
using System.IO;
using System.Threading.Tasks;

namespace Coroiu.Leet.Crawler.Storage.FileSystem
{
    public class FileStorage : IStorage
    {
        private readonly Uri root;

        public FileStorage(Uri root)
        {
            this.root = root;
        }
        
        public Task Clear()
        {
            Directory.Delete(root.ToString(), true);

            return Task.CompletedTask;
        }

        public async Task Save(Uri uri, string content)
        {
            // This conversion should be placed somewhere else
            var filepath = new Uri(root, uri.AbsolutePath);

            await File.WriteAllTextAsync(filepath.ToString(), content);
        }

        public async Task Save(Uri uri, byte[] content)
        {
            // This conversion should be placed somewhere else
            var filepath = new Uri(root, uri.AbsolutePath);

            await File.WriteAllBytesAsync(filepath.ToString(), content);
        }
    }
}

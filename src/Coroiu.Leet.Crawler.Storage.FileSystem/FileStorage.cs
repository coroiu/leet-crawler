using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Coroiu.Leet.Crawler.Storage.FileSystem
{
    public class FileStorage : IStorage
    {
        private static Uri EmptyUri = new Uri("", UriKind.Relative);
        private static Uri IndexUri = new Uri("index.html", UriKind.Relative);

        private readonly Uri root;

        public FileStorage(Uri root)
        {
            this.root = root;
        }

        public Task Clear()
        {
            if (Directory.Exists(root.LocalPath))
                Directory.Delete(root.LocalPath, true);

            return Task.CompletedTask;
        }

        public async Task Save(Uri uri, string content)
        {
            uri = uri == EmptyUri ? IndexUri : uri;
            var filepath = MakeValidHtmlFileName(new Uri(root, uri)).LocalPath;

            Directory.CreateDirectory(Path.GetDirectoryName(filepath));
            await File.WriteAllTextAsync(filepath, content);
        }

        public async Task Save(Uri uri, byte[] content)
        {
            var filepath = CleanUpFileName(new Uri(root, uri).LocalPath);

            Directory.CreateDirectory(Path.GetDirectoryName(filepath));
            await File.WriteAllBytesAsync(filepath, content);
        }

        private static Uri MakeValidHtmlFileName(Uri uri)
        {
            if (Path.GetExtension(uri.ToString()) == "")
            {
                return new Uri(uri.ToString() + "/index.html");
            }

            return uri;
        }

        private static string CleanUpFileName(string path)
        {
            var directory = Path.GetDirectoryName(path);
            var fileName = Path.GetFileName(path);
            var invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            var invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return Path.Combine(directory, System.Text.RegularExpressions.Regex.Replace(fileName, invalidRegStr, "_"));
        }
    }
}

using ParrotApp.Droid.Helper;
using ParrotApp.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]

namespace ParrotApp.Droid.Helper
{
    public class FileHelper : IFileHelper
    {
        public Task<bool> ExistsAsync(string filename)
        {
            string filepath = GetFilePath(filename);
            bool exists = File.Exists(filepath);
            return Task.FromResult(exists);
        }

        public async Task WriteTextAsync(string filename, string text)
        {
            string filepath = GetFilePath(filename);
            using (StreamWriter writer = File.CreateText(filepath))
            {
                await writer.WriteAsync(text);
            }
        }

        public async Task<string> ReadTextAsync(string filename)
        {
            string filepath = GetFilePath(filename);
            using (StreamReader reader = File.OpenText(filepath))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public void ManualCopyFile()
        {
            //Assets.Open(dbName)
        }

        public Task<IEnumerable<string>> GetFilesAsync()
        {
            // Sort the filenames.
            IEnumerable<string> filenames =
                from filepath in Directory.EnumerateFiles(GetDocsFolder())
                select Path.GetFileName(filepath);

            return Task.FromResult(filenames);
        }

        public Task DeleteAsync(string filename)
        {
            File.Delete(GetFilePath(filename));
            return Task.FromResult(true);
        }

        private string GetDocsFolder()
        {
            return System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private string GetFilePath(string filename)
        {
            return Path.Combine(GetDocsFolder(), filename);
        }

        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }

        public void WriteStreamToLocalFile(Stream stream, string filename)
        {
            byte[] data;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                data = memoryStream.ToArray();
            }

            File.WriteAllBytes(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename), data);
        }
    }
}
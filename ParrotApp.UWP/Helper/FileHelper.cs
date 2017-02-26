using ParrotApp.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(ParrotApp.UWP.Helper.FileHelper))]
namespace ParrotApp.UWP.Helper
{
    public class FileHelper : IFileHelper
    {
        public Task DeleteAsync(string filename)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(string filename)
        {
            return File.Exists(Path.Combine(ApplicationData.Current.LocalFolder.Path, filename));
        }

        public Task<IEnumerable<string>> GetFilesAsync()
        {
            throw new NotImplementedException();
        }

        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
        public void WriteStreamToLocalFile(Stream stream, string filename)
        {
            byte[] data;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                 data = memoryStream.ToArray();
            }

            File.WriteAllBytes(Path.Combine(ApplicationData.Current.LocalFolder.Path, filename), data);
        }
        public Task<string> ReadTextAsync(string filename)
        {
            throw new NotImplementedException();
        }

        public Task WriteTextAsync(string filename, string text)
        {
            throw new NotImplementedException();
        }
    }
}

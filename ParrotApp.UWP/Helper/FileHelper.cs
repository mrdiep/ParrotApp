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

        public Task<bool> ExistsAsync(string filename)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetFilesAsync()
        {
            throw new NotImplementedException();
        }

        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
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

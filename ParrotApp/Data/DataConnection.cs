using ParrotApp.Helper;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace ParrotApp.Data
{
    public class DataConnection
    {
        private const string FileName = "hopam1243.db3";
        private SQLiteConnection db;
        private IFileHelper _fileHelper;
        private bool isFirstSetupCompleted = false;
        private TaskCompletionSource<bool> firstSetupCompletionSource;

        public DataConnection(IFileHelper fileHelper)
        {
            firstSetupCompletionSource = new TaskCompletionSource<bool>();

            _fileHelper = fileHelper;

            Task.Factory.StartNew(async () =>
            {
                await FirstSetup();
            });
        }
        private void SetupConnection()
        {
            var path = _fileHelper.GetLocalFilePath(FileName);
            db = new SQLiteConnection(path);
        }
        public async Task FirstSetup()
        {
            if (await _fileHelper.ExistsAsync(FileName))
            {
                isFirstSetupCompleted = true;
                SetupConnection();
                firstSetupCompletionSource.SetResult(true);
                return;
            }

            
            var assembly = typeof(DataConnection).GetTypeInfo().Assembly;

            Stream stream = assembly.GetManifestResourceStream("ParrotApp.Resouces.hopam.db3");
            _fileHelper.WriteStreamToLocalFile(stream, FileName);
            isFirstSetupCompleted = true;
            SetupConnection();
            firstSetupCompletionSource.SetResult(true);
        }

        public IEnumerable<SongMetadata> QueryAllSongMetadata()
        {
            if (!isFirstSetupCompleted)
                throw new Exception();

            return db.Query<SongMetadata>("select `id` , `title`, `chord`, `author`, `singer` from song limit 10");
        }

        internal async Task WaitFristSetupCompleted()
        {
            if (isFirstSetupCompleted)
                return;

            await firstSetupCompletionSource.Task;
        }

        public string GetContent(int songId, int? songVersion = null)
        {
            return null;
        }
    }
}
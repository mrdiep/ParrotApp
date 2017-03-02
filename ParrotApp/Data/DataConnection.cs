using ParrotApp.Helper;
using ParrotApp.Models;
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
        private const string FileName = "hopam_test3.db3";
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

            using (var stream = ResourceFileHelper.GetStream("hopam.db3"))
            {
                _fileHelper.WriteStreamToLocalFile(stream, FileName);
            }

            isFirstSetupCompleted = true;
            SetupConnection();
            firstSetupCompletionSource.SetResult(true);
        }

        public IEnumerable<SongMetadata> QueryAllSongMetadata()
        {
            if (!isFirstSetupCompleted)
                throw new Exception();

            return db.Query<SongMetadata>("select `id` , `title`, `chord`, `author`, `singer` from songmetadata");
        }

        internal async Task WaitFristSetupCompleted()
        {
            if (isFirstSetupCompleted)
                return;

            await firstSetupCompletionSource.Task;
        }

        public SongVersion GetContent(int songId, int? songVersionId = null)
        {
            SongVersion version;
            string query;
            if (songVersionId == null)
            {
                query = " where songid=" + songId + " and `default`=1";
            }
            else
            {
                query = " where id=" + songVersionId;
            }
            version = db.FindWithQuery<SongVersion>("select * from versions" + query);

            return version;
        }
    }
}
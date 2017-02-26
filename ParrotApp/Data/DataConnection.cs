using ParrotApp.Helper;
using SQLite;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ParrotApp.Data
{
    public class DataConnection
    {
        private SQLiteConnection db;

        public DataConnection()
        {
            var path = DependencyService.Get<IFileHelper>().GetLocalFilePath("hopam.db3");
            db = new SQLiteConnection(path);
        }

        public IEnumerable<SongMetadata> QueryAllSongMetadata()
        {
            return db.Query<SongMetadata>("select `id` , `title`, `chord`, `author`, `singer` from song");
        }
    }
}
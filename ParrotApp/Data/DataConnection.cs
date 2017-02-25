using ParrotApp.Helper;
using SQLite;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ParrotApp.Data
{
    public class DataConnection
    {
        SQLiteConnection db;
        public DataConnection()
        {
            var path = DependencyService.Get<IFileHelper>().GetLocalFilePath("test.db3");
        }

        public IEnumerable<Song> QueryAllSongMetadata()
        {
            return db.Query<Song>("select 'id' , 'name' from SongMetadata");
        }
    }
}

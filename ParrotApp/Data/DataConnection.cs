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
            var path = DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3");
        }

        public IEnumerable<object> QueryAllMetadata()
        {
            return db.Query<object>("select 'Price' as 'Money', 'Time' as 'Date' from Valuation where StockId = ?", 1);
        }
    }
}


using SQLite;

namespace ParrotApp.Data
{
    public class Song
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

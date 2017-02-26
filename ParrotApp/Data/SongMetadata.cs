using SQLite;

namespace ParrotApp.Data
{
    public class SongMetadata
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("chord")]
        public string Chord { get; set; }

        [Column("rhythm")]
        public string Rhythm { get; set; }

        private string author;

        [Column("author")]
        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                author = TrimArtistName(value);
            }
        }

        private string singer;

        [Column("singer")]
        public string Singer
        {
            get
            {
                return singer;
            }
            set
            {
                singer = TrimArtistName(value);
            }
        }

        public override string ToString() => Title;

        public static string TrimArtistName(string name)
        {
            if (name == null)
            {
                return string.Empty;
            }
            return name.Trim(new char[] { ' ', ',', ';' });
        }
    }
}
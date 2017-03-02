using SQLite;

namespace ParrotApp.Models
{
    public class SongMetadata
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Chord { get; set; }

        public string Rhythm { get; set; }

        private string author;
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
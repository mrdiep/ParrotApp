namespace ParrotApp.Models
{
    public class SongVersion
    {
        public int Id { get; set; }
        public SongMetadata Metadata { get; set; }
        public string Chord { get; set; }
        public int VersionId { get; set; }
        public string Content { get; set; }

        private string _description;
        public string Description { get { return _description; } set { _description = FilterDescription(value); } }
        public string Star { get; set; }
        public string Votes { get; set; }
        public string Updated { get; set; }
        public string Default { get; set; }

        public static string FilterDescription(string text)
        {
            if (text == null) return string.Empty;
            return text.Replace('\n', ' ');
        }
    }
}
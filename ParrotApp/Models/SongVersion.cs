namespace ParrotApp.Models
{
    public class SongVersion
    {
        //"id" INTEGER PRIMARY KEY,
        //"songId" INTEGER,
        //"chord" TEXT,
        //"content" TEXT,
        //"description" TEXT,
        //"star" TEXT,
        //"votes" TEXT,
        //"updated" TEXT,
        //"default" INTEGER

        public int Id { get; set; }
        public SongMetadata Metadata { get; set; }
        public string Chord { get; set; }
        public int VersionId { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string Star { get; set; }
        public string Votes { get; set; }
        public string Updated { get; set; }
        public string Default { get; set; }
    }
}
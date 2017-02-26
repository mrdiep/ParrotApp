using ParrotApp.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ParrotApp.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<SongMetadata> SongMetadatas { get; }
        public HomeViewModel(DataConnection DataConnection)
        {
            SongMetadatas = DataConnection.QueryAllSongMetadata();
        }
    }
}
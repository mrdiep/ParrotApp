using ParrotApp.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ParrotApp.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<SongMetadata> SongMetadatas { get; }
        public ObservableCollection<SongMetadata> SearchResults { get; set; }
        public ICommand SearchSongCommand { get; }

        public HomeViewModel(DataConnection DataConnection)
        {
            SongMetadatas = DataConnection.QueryAllSongMetadata();

           // SearchSongCommand = new RelayCommand(() => { });
        }
    }
}
using ParrotApp.Data;
using ParrotApp.Libs;
using ParrotApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ParrotApp.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private IEnumerable<SongMetadata> allSong { get; }

        public ObservableCollection<SongMetadata> SearchResults { get; set; }

        public ICommand SearchSongCommand { get; }

        public SongFilter SongFilter { get; } = new SongFilter();

        public HomeViewModel(DataConnection DataConnection)
        {
            allSong = DataConnection.QueryAllSongMetadata();
            SearchResults = new ObservableCollection<SongMetadata>(allSong);

            SearchSongCommand = new RelayCommand(() =>
            {
                var songs = allSong.Where(SongFilter.Filter);
                SearchResults = new ObservableCollection<SongMetadata>(songs);
                RaisePropertyChanged("SearchResults");
            });
        }
    }
}
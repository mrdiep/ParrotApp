using ParrotApp.Data;
using ParrotApp.Helper;
using ParrotApp.Libs;
using ParrotApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ParrotApp.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private DataConnection dataConnection;

        private IEnumerable<SongMetadata> allSong { get; set; }

        private IEnumerable<SongMetadata> _searchResults;

        public IEnumerable<SongMetadata> SearchResults
        {
            get
            {
                return _searchResults;
            }
            set
            {
                _searchResults = value;
                RaisePropertyChanged();
            }
        }

        public ICommand SearchSongCommand { get; private set; }

        public SongFilter SongFilter { get; } = new SongFilter();

        public HomeViewModel(DataConnection dataConnection, IFileHelper fileHelper)
        {
            this.dataConnection = dataConnection;

            CommandRegister();

            Task.Factory.StartNew(async () =>
            {
                await LoadData();
            });
        }

        public void CommandRegister()
        {
            SearchSongCommand = new RelayCommand(() =>
            {
                SearchResults = allSong.Where(SongFilter.Filter);
                
            });
        }

        public async Task LoadData()
        {
            await dataConnection.WaitFristSetupCompleted();

            allSong = dataConnection.QueryAllSongMetadata();
            SearchResults = allSong;
        }
    }
}
﻿using Ninject;
using ParrotApp.Data;
using ParrotApp.Helper;
using ParrotApp.Libs;
using ParrotApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ParrotApp.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private bool _isSearchVisible;
        private DataConnection dataConnection;
        private NavigationService navigationService;

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

        private SongMetadata _selectedSong;

        public SongMetadata SelectedSong
        {
            get { return _selectedSong; }
            set
            {
                _selectedSong = value;
                RaisePropertyChanged();
                if (_selectedSong != null)
                {
                    MessagingCenter.Send(this, "ViewDetail", _selectedSong);
                    navigationService.GotoPage(NavigationService.View.DetailPage);
                }
            }
        }

        [Inject]
        public DetailViewModel DetailViewModel { get; set; }

        public ICommand SearchSongCommand { get; private set; }

        ICommand _toggleSearchVisibilityCommand;
        public ICommand ToggleSearchVisibilityCommand => _toggleSearchVisibilityCommand ?? (_toggleSearchVisibilityCommand = new RelayCommand(() => IsSearchVisible = !IsSearchVisible));

        public SongFilter SongFilter { get; } = new SongFilter();

        public HomeViewModel(DataConnection dataConnection, IFileHelper fileHelper, NavigationService navigationService)
        {
            this.dataConnection = dataConnection;
            this.navigationService = navigationService;

            CommandRegister();

            Task.Factory.StartNew(async () =>
            {
                await LoadData();
            });

        }

        public bool IsSearchVisible
        {
            get
            {
                return _isSearchVisible;
            }
            set
            {
                _isSearchVisible = value;
                RaisePropertyChanged();
            }
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
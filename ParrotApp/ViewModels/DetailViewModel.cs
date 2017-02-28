using ParrotApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ParrotApp.ViewModels
{
    public class DetailViewModel
    {
        private DataConnection dataConnection;
        public SongMetadata SongMetadata { get; set; }

        public DetailViewModel(DataConnection dataConnection)
        {
            this.dataConnection = dataConnection;
            MessagingCenter.Subscribe<HomeViewModel, SongMetadata>(this, "ViewDetail", (sender, song) => {
                SongMetadata = song;
                UpdateDetailView();
            });
        }

        private void UpdateDetailView()
        {
            var htmlContent = dataConnection.GetContent(SongMetadata.Id);
        }
    }
}

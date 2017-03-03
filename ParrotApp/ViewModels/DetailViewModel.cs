using ParrotApp.Data;
using ParrotApp.Helper;
using ParrotApp.Models;
using Xamarin.Forms;

namespace ParrotApp.ViewModels
{
    public class DetailViewModel : ViewModelBase
    {
        private DataConnection dataConnection;

        private SongMetadata _songMetadata;

        public SongMetadata SongMetadata
        {
            get
            {
                return _songMetadata;
            }
            set
            {
                _songMetadata = value;
                RaisePropertyChanged();
            }
        }

        private HtmlWebViewSource _htmlSource;

        public HtmlWebViewSource HtmlSource
        {
            get
            {
                return _htmlSource;
            }
            set
            {
                _htmlSource = value;
                RaisePropertyChanged();
            }
        }

        private string htmlContent;
        public string HtmlTemplateContent => htmlContent;

        public DetailViewModel(DataConnection dataConnection)
        {
            this.dataConnection = dataConnection;
            htmlContent = ResourceFileHelper.GetString("template.html");
            MessagingCenter.Subscribe<HomeViewModel, SongMetadata>(this, "ViewDetail", (sender, song) =>
            {
                SongMetadata = song;
                UpdateDetailView(SongMetadata.Id);
            });
        }

        public void UpdateDetailView(int songId, int?versionId=null)
        {
            var song = dataConnection.GetContent(songId);
            var contents = HtmlTemplateContent;
            var htmlSource = new HtmlWebViewSource();

            contents = contents.Replace("____FONTSIZE___", "20");
            contents = contents.Replace("____TITLE___", SongMetadata.Title);
            contents = contents.Replace("____CONTENT___", TextParser.GetHtmlMarkup(song.Content));
            htmlSource.Html = contents;

            HtmlSource = htmlSource;

            MessagingCenter.Send(this, "UpdateHtmlSource", HtmlSource);
        }
    }
}
using Xamarin.Forms;

namespace ParrotApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            listView.ItemAppearing += ListView_ItemAppearing;
        }

        private void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
        }
    }
}
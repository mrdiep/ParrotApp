using SlideOverKit;
using Xamarin.Forms;

namespace ParrotApp.Views
{
    public partial class DetailPage : MenuContainerPage
    {
        public DetailPage()
        {
            InitializeComponent();
            this.SlideMenu = new DetailPageSlideMenu();

            menuButton.Clicked += MenuButton_Clicked;
        }

        private void MenuButton_Clicked(object sender, System.EventArgs e)
        {
            ShowMenuAction?.Invoke();
        }
    }
}
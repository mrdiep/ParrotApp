using SlideOverKit;
using Xamarin.Forms;

namespace ParrotApp.Views
{
    public partial class HomePage : MenuContainerPage
    {
        public HomePage()
        {
            InitializeComponent();
            this.SlideMenu = new HomePageSlideMenu();

            menuButton.Clicked += MenuButton_Clicked;
            this.ToolbarItems.Add(new ToolbarItem
            {
                Command = new Command(() => {
                    if (this.SlideMenu.IsShown)
                    {
                        HideMenuAction?.Invoke();
                    }
                    else
                    {
                        ShowMenuAction?.Invoke();
                    }
                }),

                Text = "Settings",
                Priority = 0
            });

        }

        private void MenuButton_Clicked(object sender, System.EventArgs e)
        {
            ShowMenu();
        }
    }
}
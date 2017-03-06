using SlideOverKit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParrotApp.Views
{
    public partial class HomePageSlideMenu : SlideMenuView
    {
        public HomePageSlideMenu()
        {
            InitializeComponent();
            this.IsFullScreen = true;
            this.WidthRequest = 350;
            this.MenuOrientations = MenuOrientation.LeftToRight;
            AnimationDurationMillisecond = 250;
            this.BackgroundColor = Color.White;
            this.BackgroundViewColor = Color.FromRgba(0,0,0,111);
        }
    }
}
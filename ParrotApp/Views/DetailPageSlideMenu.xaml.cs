using SlideOverKit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParrotApp.Views
{
    public partial class DetailPageSlideMenu : SlideMenuView
    {
        public DetailPageSlideMenu()
        {
            InitializeComponent();
            this.IsFullScreen = true;
            // You must set WidthRequest in this case
            this.WidthRequest = 350;
            this.MenuOrientations = MenuOrientation.LeftToRight;
            AnimationDurationMillisecond = 250;
            this.BackgroundColor = Color.White;
            

            // This is shadow view color, you can set a transparent color
            this.BackgroundViewColor = Color.FromRgba(0,0,0,111);
        }
    }
}
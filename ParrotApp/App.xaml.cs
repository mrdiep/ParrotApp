using ParrotApp.Helper;
using ParrotApp.Helper.Covnerters;
using ParrotApp.ViewModels;
using ParrotApp.Views;
using Xamarin.Forms;

namespace ParrotApp
{
    public partial class App : Application
    {
        public static ServiceLocator Locator { get; internal set; }

        public App()
        {
            InitializeComponent();
            foreach (var converter in ConverterHelper.Converters)
            {
                Resources[converter.Key] = converter.Value;
            }

            Locator = new ServiceLocator();

            MainPage = new NavigationPage(new HomePage());

            Locator.Get<NavigationService>().Navigation = MainPage.Navigation;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
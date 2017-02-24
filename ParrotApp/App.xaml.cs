using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            App.Locator = new ServiceLocator();
            MainPage = new MainPage();
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

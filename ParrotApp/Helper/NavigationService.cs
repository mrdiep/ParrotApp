﻿using ParrotApp.Views;
using Xamarin.Forms;

namespace ParrotApp.Helper
{
    public class NavigationService
    {
        public INavigation Navigation { get; internal set; }

        public enum View
        {
            HomePage,
            DetailPage,
        }

        private View currentView;

        internal void GotoPage(View view)
        {
            switch (view)
            {
                case View.DetailPage:
                    Navigation.PushAsync(new DetailPage());
                    currentView = View.DetailPage;

                    break;

                case View.HomePage:
                    Navigation.PushAsync(new DetailPage());
                    currentView = View.HomePage;

                    break;
            }
        }
    }
}
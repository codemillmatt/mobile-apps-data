using System;
using FitApp.Core.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FitApp.Core;
using MonkeyCache.SQLite;


namespace FitApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            DependencyService.Register<IWebDataService, WebDataService>();
            DependencyService.Register<ILocalDataService, LocalDataService>();

            MainPage = new NavigationPage(new WorkoutHistoryPage());                

            Barrel.ApplicationId = "fitapp";
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

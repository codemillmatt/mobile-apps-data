using System;
using FitApp.Core.Pages;
using Xamarin.Forms;
using FitApp.Core;


namespace FitApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            DependencyService.Register<IWebDataService, WebDataService>();
            
            MainPage = new NavigationPage(new WorkoutHistoryPage());                                       
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

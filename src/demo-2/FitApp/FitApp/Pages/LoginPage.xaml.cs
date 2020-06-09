using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FitApp.Core
{
    public partial class LoginPage : ContentPage
    {
        LoginPageViewModel vm;

        public LoginPage()
        {
            InitializeComponent();

            vm = new LoginPageViewModel();

            BindingContext = vm;
        }
    }
}

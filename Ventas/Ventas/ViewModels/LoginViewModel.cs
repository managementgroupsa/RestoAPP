using RestoPLUS.Services.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using RestoPLUS.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Splat;

namespace RestoPLUS.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        //private IRoutingService _navigationService;

        //public LoginViewModel(IRoutingService navigationService = null)
        //{
        //    _navigationService = navigationService ?? Locator.Current.GetService<IRoutingService>();
        //    ExecuteLogin = new Command(() => Login());
        //    ExecuteRegistration = new Command(() => Register());
        //}

        //public string Username { get; set; }
        //public string Password { get; set; }
        //public ICommand ExecuteLogin { get; set; }
        //public ICommand ExecuteRegistration { get; set; }

        //private void Login()
        //{
        //    // This is where you would probably check the login and only if valid do the navigation...
        //    _navigationService.NavigateTo(nameof(LoginPage));
        //}

        //private void Register()
        //{
        //    Shell.Current.GoToAsync("//login/registration");
        //}
    }
}

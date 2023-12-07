using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Ventas.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Acerca de..";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("http://www.mgsa.com.pe"));
        }

        public ICommand OpenWebCommand { get; }
    }
}
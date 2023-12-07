using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ventas.ViewModels
{
    internal class VentasDetalleViewModel : BaseViewModel
    {
        public ICommand GoBackcommand => new Command(async () => await Shell.Current.DisplayAlert("", "Si desea terminar la operacion haga clic en el boton cancelar", "OK"));


        public VentasDetalleViewModel()
        {
            Title = "Ventas Detalle";
        }

    }
}
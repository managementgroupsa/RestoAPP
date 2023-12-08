using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RestoAPP.ViewModels
{
    internal class NotaCreditoDetalleViewModel : BaseViewModel
    {
        public ICommand GoBackcommand => new Command(async () => await Shell.Current.DisplayAlert("", "Si desea terminar la operacion haga clic en el boton cancelar", "OK"));


        public NotaCreditoDetalleViewModel()
        {
            Title = "Nota de Credito Detalle";
        }

    }
}
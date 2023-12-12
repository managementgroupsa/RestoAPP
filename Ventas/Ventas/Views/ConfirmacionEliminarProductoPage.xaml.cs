using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestoPLUS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmacionEliminarProductoPage : ContentPage
    {
        public ConfirmacionEliminarProductoPage()
        {
            InitializeComponent();
        }

        private async void OnSiClicked(object sender, EventArgs e)
        {
            // Indica que el usuario ha aceptado eliminar el producto
            MessagingCenter.Send(this, "EliminarProducto", true);
            await Navigation.PopModalAsync();
        }

        private async void OnNoClicked(object sender, EventArgs e)
        {
            // Indica que el usuario ha cancelado la eliminación del producto
            MessagingCenter.Send(this, "EliminarProducto", false);
            await Navigation.PopModalAsync();
        }
    }
}
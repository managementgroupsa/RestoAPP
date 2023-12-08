using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoAPP.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestoAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VentaDetalleCuotaPage : ContentPage
    {

        public VentaDetalleCuotaPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

        private void dtpFechas_OkButtonClicked(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        {
            DateTime date = General.FE(e.NewValue.ToString());

            txtFecha.Text = date.ToString("dd/MM/yyyy");
        }

        private void btnFecha_Clicked(object sender, EventArgs e)
        {
            dtpFechas.IsOpen = true;
        }

        private void btnBuscar_Clicked(object sender, EventArgs e)
        {

        }

        private async Task<bool> Validacion()
        {
            bool bValidar = true;

            string nTotalVenta = Application.Current.Properties["TOTAL"] as string;
            string nTotalCuotas = Application.Current.Properties["TOTALCUOTAS"] as string;
            string cFechaCuota = Application.Current.Properties["FECHACUOTA"] as string;


            if (string.IsNullOrEmpty(txtFecha.Text))
            {
                await DisplayAlert("Validación - Cuotas", "Seleccione una fecha", "OK");
                return false;
            }

            if (Convert.ToDecimal(txtCuota.Value) <= 0)
            {
                await DisplayAlert("Validación - Cuotas", "Ingrese una cuota", "OK");
                return false;
            }

            if (Convert.ToDecimal(txtCuota.Value) + Convert.ToDecimal(nTotalCuotas) > Convert.ToDecimal(nTotalVenta))
            {
                await DisplayAlert("Validación - Cuotas", "La cuota ingresada sobrepasa a la venta total", "OK");
                return false;
            }

            //if ( General.NE(General.FechaISO(txtFecha.Text))<= General.NE(General.FechaISO(cFechaCuota)))
            //{
            //    await DisplayAlert("Validación - Cuotas", "La fecha ingresada no debe ser menor a la ultima cuota registrada", "OK");
            //    return false;
            //}

            return bValidar;
        }

        private async void btnAceptar_Clicked(object sender, EventArgs e)
        {



            if (await Validacion() == true)
            {
                Application.Current.Properties["RepuestaCuota"] = RepuestaVentanaFlotante.Aceptado;
                Application.Current.Properties["Ccu_nMontoCuota"] = txtCuota.Value;
                Application.Current.Properties["Ccu_dFechaVenc"] = txtFecha.Text;

                await Shell.Current.Navigation.PopAsync();


            }
        }

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["RepuestaBusqueda"] = RepuestaVentanaFlotante.Cancelado;

            await Shell.Current.Navigation.PopAsync();
        }

    }
}
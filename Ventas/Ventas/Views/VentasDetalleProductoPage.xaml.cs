using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestoAPP.Extensions;
using RestoAPP.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestoAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VentasDetalleProductoPage : ContentPage
    {
        string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }
        public VentasDetalleProductoPage()
        {
            InitializeComponent();
        }

        private void RedimencionarCombos()
        {
            Application.Current.Properties["AltoVentana"] = (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density) - 80;
            tdbcProductos.PickerHeight = General.NE(Application.Current.Properties["AltoVentana"]);
        }

        private void tdbcProductos_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            txtProductos.Text = e.NewValue.ToString();
            Application.Current.Properties["Cab_cCatalogo"] = General.PrimerValor(e.NewValue.ToString());


            //------------------------------
            LGM_CATALOGO_BS_Entity oEntidad = new LGM_CATALOGO_BS_Entity();

            oEntidad.Accion = "BUSCAR_REGAPP";
            oEntidad.Emp_cCodigo = cEmpresa;
            oEntidad.Cab_cCatalogo = General.PrimerValor(txtProductos.Text);

            string result = ProcedimientosAPI.GetPostBuscarCatalogo(oEntidad);

            result = General.ValidaJSON(result);

            List<LGM_CATALOGO_BS_Entity> response = JsonConvert.DeserializeObject<List<LGM_CATALOGO_BS_Entity>>(result);

            foreach (var message in response)
            {
                txtUnidad.Text = message.Cab_cUnidad;
            }

            if (!string.IsNullOrEmpty(txtUnidad.Text))
            {
                txtUnidadDescrip.Text = General.SegundoValor(txtUnidad.Text);
            }

            //------------------------------
            VTD_CATALOGO_PRECIO_Entity oEntidadPU = new VTD_CATALOGO_PRECIO_Entity();

            oEntidadPU.Accion = "ULTIMOPRECIO";
            oEntidadPU.Emp_cCodigo = cEmpresa;
            oEntidadPU.Cab_cCatalogo = General.PrimerValor(txtProductos.Text);
            oEntidadPU.Tpr_cCodigo = "01";

            string result_PU = ProcedimientosAPI.GetPostBuscarPrecio(oEntidadPU);

            result_PU = General.ValidaJSON( result_PU );

            List<VTD_CATALOGO_PRECIO_Entity> response_PU = JsonConvert.DeserializeObject<List<VTD_CATALOGO_PRECIO_Entity>>(result_PU);

            foreach (var message_pu in response_PU)
            {
                txtPrecioUnit.Value =General.NE( message_pu.Cpr_nPrecio);
            }

            txtCantidad.Value = 1;


        }

        private void btProductos_Clicked(object sender, EventArgs e)
        {
            RedimencionarCombos();
            tdbcProductos.ItemsSource = LlenaProductos();
            tdbcProductos.IsOpen = true;
        }

        private List<string> LlenaProductos()
        {
            List<string> oLista = new List<string>();

            LGM_CATALOGO_BS_Entity oEntidad = new LGM_CATALOGO_BS_Entity();

            oEntidad.Accion = "BUSCARTODOS";
            oEntidad.Emp_cCodigo = cEmpresa;


            string result = ProcedimientosAPI.GetPostBuscarCatalogos(oEntidad);

            List<LGM_CATALOGO_BS_Entity> response = JsonConvert.DeserializeObject<List<LGM_CATALOGO_BS_Entity>>(result);

            foreach (var message in response)
            {
                oLista.Add(message.Cab_cCatalogo + " - " + message.Cab_cDescripLarga);
            }

            return oLista;
        }

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["RepuestaBusqueda"] = RepuestaVentanaFlotante.Cancelado;
            await Navigation.PopAsync();
        }

        private async Task<bool> Validacion()
        {
            bool bValidar = true;

            if (string.IsNullOrEmpty(txtProductos.Text))
            {
                await DisplayAlert("Validación - Productos", "Seleccione un tipo de documento", "OK");
                return false;
            }

            if (Convert.ToDecimal(txtCantidad.Value) <= 0)
            {
                await DisplayAlert("Validación - Productos", "Ingrese la cantidad", "OK");
                return false;
            }

            if (Convert.ToDecimal(txtPrecioUnit.Value) <= 0)
            {
                await DisplayAlert("Validación - Productos", "Ingrese el precio unitario", "OK");
                return false;
            }

            return bValidar;
        }

        private async void btnAceptar_Clicked(object sender, EventArgs e)
        {
            if (await Validacion() == true)
            {
                Application.Current.Properties["RepuestaBusqueda"] = RepuestaVentanaFlotante.Aceptado;

                Application.Current.Properties["Cab_cCatalogo"] = General.PrimerValor(txtProductos.Text);
                Application.Current.Properties["Dvd_nCantidad"] = txtCantidad.Value;
                Application.Current.Properties["PU"] = txtPrecioUnit.Value;

                txtTotal.Value = RetornaProducto();

                Application.Current.Properties["TOTAL"] = txtTotal.Value;
                Application.Current.Properties["Cab_cDescripLarga"] = General.SegundoValor(txtProductos.Text);
                Application.Current.Properties["Cab_cUnidad"] = General.PrimerValor(txtUnidad.Text);
                Application.Current.Properties["Unidad"] = txtUnidadDescrip.Text;

                await Shell.Current.Navigation.PopAsync();


            }
        }

        private decimal RetornaProducto()
        {
            return Math.Round(Convert.ToDecimal(General.NE(txtCantidad.Value)) * Convert.ToDecimal(General.NE(txtPrecioUnit.Value)), 2);
        }

        private void txtPrecioUnit_ValueChanged(object sender, Syncfusion.SfNumericTextBox.XForms.ValueEventArgs e)
        {
            txtTotal.Value = RetornaProducto();
        }

        private void txtCantidad_ValueChanged(object sender, Syncfusion.SfNumericUpDown.XForms.ValueEventArgs e)
        {
            txtTotal.Value = RetornaProducto();
        }
    }
}
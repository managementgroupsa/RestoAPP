using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Extensions;
using Ventas.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Android;
using Splat;
using Ventas.ViewModels;

namespace Ventas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmpresasPage : ContentPage
    {
        string cSoftware = Application.Current.Properties["Soft_cCodSoft"] as string;



        public EmpresasPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
            }
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

        private void RedimencionarCombos()
        { 
            Application.Current.Properties["AltoVentana"] = (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density) - 80;
            tdbcEmpresas.PickerHeight = General.NE(Application.Current.Properties["AltoVentana"]);
            tdbcAnios.PickerHeight = General.NE(Application.Current.Properties["AltoVentana"]);
            tdbcPuntosVenta.PickerHeight = General.NE(Application.Current.Properties["AltoVentana"]);
        }

        private async void btnPrincipal_Clicked(object sender, EventArgs e)
        {
            try
            {
                var selectedItemEmpresa = tdbcEmpresas.SelectedItem as string;
                var selectedItemAnio = tdbcAnios.SelectedItem as string;
                var selectedItemPuntoVenta = tdbcPuntosVenta.SelectedItem as string;


                if (string.IsNullOrEmpty(selectedItemEmpresa))
                {
                    await DisplayAlert("Validación", "Seleccione una empresa", "Aceptar");
                }
                else
                {
                    if (string.IsNullOrEmpty(selectedItemAnio))
                    {
                        await DisplayAlert("Validación", "Seleccione un año", "Aceptar");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(selectedItemPuntoVenta))
                        {
                            await DisplayAlert("Validación", "Seleccione un Punto de Venta", "Aceptar");
                        }

                    }
                }

                if (!string.IsNullOrEmpty(selectedItemEmpresa) && !string.IsNullOrEmpty(selectedItemAnio) && !string.IsNullOrEmpty(selectedItemPuntoVenta))
                {
                    Application.Current.Properties["Emp_cCodigo"] = General.PrimerValor(selectedItemEmpresa);
                    Application.Current.Properties["Emp_cNombreLargo"] = General.SegundoValor(selectedItemEmpresa);
                    Application.Current.Properties["Pan_cAnio"] = selectedItemAnio;
                    Application.Current.Properties["buscaUbicacion"] = "0";
                    Application.Current.Properties["Pvt_cCodigo"] = General.PrimerValor(selectedItemPuntoVenta);


                    await Shell.Current.GoToAsync( $"//DefaultPage");
                    await Navigation.PopAsync();

                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Validación", ex.Message, "Aceptar");
            }
        }

        private List<string> LlenaEmpresas()
        {
            List<string> oLista = new List<string>();

            string cUsuario = Application.Current.Properties["Usu_cCodUsuario"] as string;

            Login_Entity oEntidad = new Login_Entity();

            oEntidad.Accion= "EMPRESA";
            oEntidad.Usu_cCodUsuario = cUsuario;
            oEntidad.Usu_cClave = "";
            oEntidad.Emp_cCodigo = "";
            oEntidad.Soft_cCodSoft = cSoftware;

            string result = ProcedimientosAPI.GetPostBuscarEmpresas(oEntidad);

            List<Empresa_Entity> response = JsonConvert.DeserializeObject<List<Empresa_Entity>>(result);

            foreach (var message in response)
            {
                oLista.Add(message.Emp_cCodigo + " - " + message.Emp_cNombreLargo.Left(30));
            }

            return oLista;
        }

        private List<string> LlenaAnios()
        {
            List<string> oLista = new List<string>();

            string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;

            Login_Entity oEntidad = new Login_Entity();

            oEntidad.Accion= "ANIOS";
            oEntidad.Usu_cCodUsuario = "";
            oEntidad.Usu_cClave = "";
            oEntidad.Emp_cCodigo = cEmpresa;
            oEntidad.Soft_cCodSoft = cSoftware;

            string result = ProcedimientosAPI.GetPostBuscarAnios(oEntidad);

            List<Anio_Entity> response = JsonConvert.DeserializeObject<List<Anio_Entity>>(result);

            foreach (var message in response)
            {
                oLista.Add(message.Pan_cAnio);
            }

            return oLista;
        }

        private List<string> LlenaPuntosVenta()
        {
            List<string> oLista = new List<string>();

            string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;
            string cUsuario = Application.Current.Properties["Usu_cCodUsuario"] as string;

            Login_Entity oEntidad = new Login_Entity();

            oEntidad.Accion = "PUNTOVTA";
            oEntidad.Usu_cCodUsuario = cUsuario;
            oEntidad.Usu_cClave = "";
            oEntidad.Emp_cCodigo = cEmpresa;
            oEntidad.Soft_cCodSoft = cSoftware;

            string result = ProcedimientosAPI.GetPostBuscarPuntosVenta(oEntidad);

            List<PuntoVenta_Entity> response = JsonConvert.DeserializeObject<List<PuntoVenta_Entity>>(result);

            foreach (var message in response)
            {
                oLista.Add(message.Pvt_cCodigo + " - " + message.Pvt_cDescripcion);
            }

            return oLista;
        }

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private void tdbcEmpresas_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            txtEmpresas.Text = e.NewValue.ToString();
            Application.Current.Properties["Emp_cCodigo"] = General.PrimerValor(e.NewValue.ToString());
        }

        private void cmdEmpresas_Clicked(object sender, EventArgs e)
        {
            RedimencionarCombos();
            tdbcEmpresas.ItemsSource = LlenaEmpresas();
            tdbcEmpresas.IsOpen = true;
        }

        private void tdbcAnios_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            txtAnios.Text = e.NewValue.ToString();
            Application.Current.Properties["Pan_cAnio"] = General.PrimerValor(e.NewValue.ToString());
        }

        private void cmdAnios_Clicked(object sender, EventArgs e)
        {
            RedimencionarCombos();
            tdbcAnios.ItemsSource = LlenaAnios();
            tdbcAnios.IsOpen = true;
        }

        private void cmdPuntoVenta_Clicked(object sender, EventArgs e)
        {
            RedimencionarCombos();
            tdbcPuntosVenta.ItemsSource = LlenaPuntosVenta();
            tdbcPuntosVenta.IsOpen = true;
        }

        private void tdbcPuntosVenta_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            txtPuntoVenta.Text = e.NewValue.ToString();
            Application.Current.Properties["Pvt_cCodigo"] = General.PrimerValor(e.NewValue.ToString());
            Application.Current.Properties["Pvt_cDescripcion"] = General.SegundoValor(e.NewValue.ToString());
            
        }

    }
}
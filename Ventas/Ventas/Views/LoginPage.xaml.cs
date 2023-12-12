using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using RestoPLUS.Extensions;
using RestoPLUS.Models;
using RestoPLUS.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Splat;
using RestoPLUS.Services.Routing;
using System.Linq;
using System.IO;
using System.Reflection;

namespace RestoPLUS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        string cSoftware = Application.Current.Properties["Soft_cCodSoft"] as string;

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        public LoginPage()
        {
            InitializeComponent();

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(LoginPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("RestoPLUS.appsettings.json");
            var steamreader = new StreamReader(stream).ReadToEnd();

            List<AppSettings> list =JsonConvert.DeserializeObject<List<AppSettings>>(steamreader);

            string url = "";

            foreach (var info in list)
            {
                url  =info.ApiUrl;
            }

            Application.Current.Properties["URL"] = url;

            this.BindingContext = new LoginViewModel();



            txtUsuario.Text = "DEMO";
            txtContraseña.Text = "DEMO";
        }

        private async void btnPrincipal_Clicked(object sender, EventArgs e)
        {


            try
            {
                char[] charsToTrim = { ' ' };
                txtUsuario.Text = txtUsuario.Text.ToUpper().Trim(charsToTrim);

                string cUsuario = txtUsuario.Text;
                string cContraseña = txtContraseña.Text;



                if (cUsuario != "" && cContraseña != "")
                {
                    string cSoftware = Application.Current.Properties["Soft_cCodSoft"] as string;

                    Login_Entity oUser = new Login_Entity();
                    oUser.Usu_cCodUsuario = cUsuario;
                    oUser.Usu_cClave = cContraseña;

                    string token =  ProcedimientosAPI.GetPostBuscarToken(oUser);

                    if (!string.IsNullOrEmpty(token))
                    {
                        Application.Current.Properties["Token"] = token;

                        Login_Entity oEntidad = new Login_Entity();

                        oEntidad.Accion = "LOGIN";
                        oEntidad.Usu_cCodUsuario = cUsuario;
                        oEntidad.Usu_cClave = cContraseña;
                        oEntidad.Emp_cCodigo = "";
                        oEntidad.Soft_cCodSoft = cSoftware;


                        string result = ProcedimientosAPI.GetPostBuscarCredenciales(oEntidad);

                        Respuesta_Entity deserialized = JsonConvert.DeserializeObject<Respuesta_Entity>(result);


                        if (deserialized.Respuesta == Resultado.Encontrado)
                        {
                            Application.Current.Properties["Usu_cCodUsuario"] = cUsuario;
                            Application.Current.Properties["Usu_cClave"] = cContraseña;

                            LayaoutLogeo.IsVisible = false;
                            LayaoutEmpresas.IsVisible = true;

                            
                        }
                        else
                        {
                            await DisplayAlert("Validación", "Usuario no tiene acceso", "Aceptar");
                        }

                    }
                    else
                        await DisplayAlert("Validación", "Usuario y Contraseña incorrecta", "Aceptar");


                }
                else
                {
                    await DisplayAlert("Validación", "Usuario y Contraseña incorrecta", "Aceptar");
                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Validación", ex.Message, "Aceptar");
            }

        }


        private void RedimencionarCombos()
        {
            Application.Current.Properties["AltoVentana"] = (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density) - 80;
            tdbcEmpresas.PickerHeight = General.NE(Application.Current.Properties["AltoVentana"]);
            tdbcAnios.PickerHeight = General.NE(Application.Current.Properties["AltoVentana"]);
            tdbcPuntosVenta.PickerHeight = General.NE(Application.Current.Properties["AltoVentana"]);
        }

        private List<string> LlenaEmpresas()
        {
            List<string> oLista = new List<string>();

            string cUsuario = Application.Current.Properties["Usu_cCodUsuario"] as string;

            Login_Entity oEntidad = new Login_Entity();

            oEntidad.Accion = "EMPRESA";
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

            oEntidad.Accion = "ANIOS";
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

        private void tdbcPuntosVenta_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            txtPuntoVenta.Text = e.NewValue.ToString();
            Application.Current.Properties["Pvt_cCodigo"] = General.PrimerValor(e.NewValue.ToString());
            Application.Current.Properties["Pvt_cDescripcion"] = General.SegundoValor(e.NewValue.ToString());
        }

        private void cmdPuntoVenta_Clicked(object sender, EventArgs e)
        {
            RedimencionarCombos();
            tdbcPuntosVenta.ItemsSource = LlenaPuntosVenta();
            tdbcPuntosVenta.IsOpen = true;
        }

        private async void btnPrincipal_Empresas_Clicked(object sender, EventArgs e)
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


                    LayaoutLogeo.IsVisible = false;
                    LayaoutEmpresas.IsVisible = true;

                    // quita la pagina del logeo y se ejecuta el loading page para determinar a donde se direcciona
                    // al login o al default
                    // pero como ya se logueo y el token es diferente de vacio entoinces va al default.
                    await Shell.Current.Navigation.PopAsync();
                    
                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Validación", ex.Message, "Aceptar");
            }
        }

        private void btnCancelar_Empresas_Clicked(object sender, EventArgs e)
        {
            LayaoutLogeo.IsVisible = true;
            LayaoutEmpresas.IsVisible = false;
        }
    }

}
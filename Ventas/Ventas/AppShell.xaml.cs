using System;
using System.Collections.Generic;
using RestoAPP.ViewModels;
using RestoAPP.Views;
using Xamarin.Forms;
using System.Windows.Input;
using Xamarin.Essentials;

namespace RestoAPP
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        private void IniciaVariablesGlobales()
        {
            Application.Current.Properties["AltoVentana"] = (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density) - 80;

            Application.Current.Properties["Emp_cCodigo"] = "";
            Application.Current.Properties["Emp_cNombreLargo"] = "";
            Application.Current.Properties["Pan_cAnio"] = "";
            Application.Current.Properties["Per_cPeriodo"] = "";
            
            Application.Current.Properties["Usu_cCodUsuario"] = "";
            Application.Current.Properties["Usu_cClave"] = "";

            Application.Current.Properties["Pvt_cCodigo"] = "";
            Application.Current.Properties["Pvt_cDescripcion"] = "";

            Application.Current.Properties["Mes_cCodigo"] = "";
            Application.Current.Properties["Mes_cDescripcion"] = "";
            Application.Current.Properties["Ent_cCodEntidad"] = "";
            Application.Current.Properties["Ent_cPersona"] = "";


            Application.Current.Properties["Opcion"] = "";
            Application.Current.Properties["Ped_cNummov"] ="";
            Application.Current.Properties["Res_cNummov"] = "";

            Application.Current.Properties["RepuestaBusqueda"] = "";
            Application.Current.Properties["Cab_cCatalogo"] = "";
            Application.Current.Properties["Dvd_nCantidad"] = "";
            Application.Current.Properties["PU"] = "";
            Application.Current.Properties["Cab_cDescripLarga"] = "";
            Application.Current.Properties["Cab_cUnidad"] = "";
            Application.Current.Properties["Unidad"] = "";

            Application.Current.Properties["Soft_cCodSoft"] = "005";
            Application.Current.Properties["Tca_dFecha"] = "";

            Application.Current.Properties["Dvd_nItem"] = "";

            Application.Current.Properties["TOTAL"] = "";

            Application.Current.Properties["Token"] = "";
            Application.Current.Properties["TiempoInactividad"] = DateTime.Now; ;

            Application.Current.Properties["URL"] = "";
        }
        public AppShell()
        {
            IniciaVariablesGlobales();

            InitializeComponent();


            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            

            // Ruteo de primer nivel
            Routing.RegisterRoute(nameof(PedidosPage), typeof(PedidosPage));
            
            // Ruteo de segundo nivel
            Routing.RegisterRoute(nameof(PedidosDetallePage ), typeof(PedidosDetallePage));
            

            // Ruteo de tercer nivel
            //Routing.RegisterRoute(nameof(VentasDetalleProductoPage), typeof(VentasDetalleProductoPage));
            
            BindingContext = this;
        }

        public ICommand ExecuteLogout => new Command(async () => await GoToAsync(nameof(LoginPage)));

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

        protected override void OnNavigating
                               (ShellNavigatingEventArgs args)
        {
            // implement your logic
            Application.Current.Properties["TiempoInactividad"] = DateTime.Now;


            base.OnNavigating(args);
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}

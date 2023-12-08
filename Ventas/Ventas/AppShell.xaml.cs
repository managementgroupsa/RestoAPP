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
            Application.Current.Properties["Opcion"] = "";
            Application.Current.Properties["Ent_cCodEntidad"] = "";
            Application.Current.Properties["Dvm_cNumMov"] = "";
            Application.Current.Properties["Aca_cCategoria"] = "";
            Application.Current.Properties["Gru_cGrupo"] = "";
            Application.Current.Properties["Cla_cClase"] = "";
            Application.Current.Properties["Fam_cFamilia"] = "";

            Application.Current.Properties["RepuestaBusqueda"] = "";
            Application.Current.Properties["Cab_cCatalogo"] = "";
            Application.Current.Properties["Dvd_nCantidad"] = "";
            Application.Current.Properties["PU"] = "";
            Application.Current.Properties["Cab_cDescripLarga"] = "";
            Application.Current.Properties["Cab_cUnidad"] = "";
            Application.Current.Properties["Unidad"] = "";

            Application.Current.Properties["Soft_cCodSoft"] = "005";
            Application.Current.Properties["Tca_dFecha"] = "";

            Application.Current.Properties["Dvm_cTipoDoc"] = "";
            Application.Current.Properties["Dvm_cSerieDoc"] = "";

            Application.Current.Properties["Cab_cTipoBS"] = "";
            Application.Current.Properties["Dvm_cCondicion"] = "";

            Application.Current.Properties["Dvm_cNacExt"] = "";
            Application.Current.Properties["FormaPago"] = "";
            Application.Current.Properties["Modalidad"] = "";

            Application.Current.Properties["Dvd_nItem"] = "";
            Application.Current.Properties["Ccu_nCorrel"] = "";

            Application.Current.Properties["RepuestaCuota"] = "";
            Application.Current.Properties["Ccu_nMontoCuota"] = "";
            Application.Current.Properties["Ccu_dFechaVenc"] = "";

            Application.Current.Properties["En_nMail"] = "";



            Application.Current.Properties["TOTAL"] = "";
            Application.Current.Properties["TOTALCUOTAS"] = "";
            Application.Current.Properties["FECHACUOTA"] = "";
            Application.Current.Properties["ValidaSunat"] = "";
            Application.Current.Properties["Tpr_cCodigo"] = "";
            Application.Current.Properties["Cpr_cCorrelativo"] = "";

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
            Routing.RegisterRoute(nameof(CategoriaPage), typeof(CategoriaPage));
            Routing.RegisterRoute(nameof(GrupoPage), typeof(GrupoPage));
            Routing.RegisterRoute(nameof(ClasePage), typeof(ClasePage));
            Routing.RegisterRoute(nameof(FamiliaPage), typeof(FamiliaPage));

            // Ruteo de segundo nivel
            Routing.RegisterRoute(nameof(ClientesDetallePage), typeof(ClientesDetallePage));
            Routing.RegisterRoute(nameof(CatalogoDetallePage), typeof(CatalogoDetallePage));

            Routing.RegisterRoute(nameof(TipoPrecioDetallePage), typeof(TipoPrecioDetallePage));
            Routing.RegisterRoute(nameof(CatalogoPrecioDetallePage), typeof(CatalogoPrecioDetallePage));

            Routing.RegisterRoute(nameof(TipoCambioDetallePage), typeof(TipoCambioDetallePage));
            
            Routing.RegisterRoute(nameof(VentasDetallePage), typeof(VentasDetallePage));
            
            Routing.RegisterRoute(nameof(NotaCreditoDetallePage), typeof(NotaCreditoDetallePage));

            Routing.RegisterRoute(nameof(CategoriadetallePage), typeof(CategoriadetallePage));
            Routing.RegisterRoute(nameof(GrupodetallePage), typeof(GrupodetallePage));
            Routing.RegisterRoute(nameof(ClasedetallePage), typeof(ClasedetallePage));
            Routing.RegisterRoute(nameof(FamiliadetallePage), typeof(FamiliadetallePage));
            

            // Ruteo de tercer nivel
            Routing.RegisterRoute(nameof(VentasDetalleProductoPage), typeof(VentasDetalleProductoPage));
            Routing.RegisterRoute(nameof(VentaDetalleCuotaPage), typeof(VentaDetalleCuotaPage));

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

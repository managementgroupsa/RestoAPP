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
    public partial class DefaultPage : ContentPage
    {
        public DefaultPage()
        {
            InitializeComponent();

      //      Shell.Current.GoToAsync("//LoginPage");


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            lblNombreEmpresa.Text = " EMPRESA: " + Application.Current.Properties["Emp_cNombreLargo"] as string;
            lblEmpresa.Text = " CODIGO: " + Application.Current.Properties["Emp_cCodigo"] as string;
            lblPuntoVenta.Text = " PUNTO DE VENTA: " + Application.Current.Properties["Pvt_cDescripcion"] as string;
            lblUsuario.Text = " USUARIO: " + Application.Current.Properties["Usu_cCodUsuario"] as string;
            lblAnio.Text = " AÑO: " + Application.Current.Properties["Pan_cAnio"] as string;


        }

    }
}
using Newtonsoft.Json;
using RestoAPP.Extensions;
using RestoAPP.Models;
using RestoAPP.ViewModels;
using Syncfusion.GridCommon;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestoAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PedidosPage : ContentPage
    {

        string Pan_cAnio = Application.Current.Properties["Pan_cAnio"] as string;
        string Emp_cCodigo = Application.Current.Properties["Emp_cCodigo"] as string;
        string Pvt_cCodigo = Application.Current.Properties["Pvt_cCodigo"] as string;
        string Usu_cCodUsuario = Application.Current.Properties["Usu_cCodUsuario"] as string;

        DataView view;

        protected override void OnAppearing()
        {
            base.OnAppearing();
          //  LlenarMesas();
         

        }

        //private void LlenarMesas()
        //{
        //    var oEntidad = new PedidosViewModel();
        //    view.Table = oEntidad.GetData();

        //}

        public PedidosPage()
        {
            InitializeComponent();

            try 
            {
               // LlenarMesas();
                //listView.ItemsSource  = view; 
            } 
            catch (Exception ex) 
            { 
            }
        }
    }
}
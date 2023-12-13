using Newtonsoft.Json;
using RestoPLUS.Extensions;
using RestoPLUS.Models;
using RestoPLUS.ViewModels;
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

namespace RestoPLUS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PedidosPage : ContentPage
    {
        private PedidosViewModel viewModel;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Ejecutar el comando al aparecer la página
            viewModel.RefreshCommand.Execute(null);
        }


        public PedidosPage()
        {
            InitializeComponent();

            viewModel = new PedidosViewModel();
            BindingContext = viewModel;
        }
    }
}
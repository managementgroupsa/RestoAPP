using Newtonsoft.Json;
using Syncfusion.SfBusyIndicator.XForms;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Extensions;
using Ventas.Models;
using Ventas.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ventas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogoPrecioPage : ContentPage
    {

        DataView view;

        public CatalogoPrecioPage()
        {
            InitializeComponent();

            try
            {
                dataGrid.CanUseViewFilter = true;
                view = new DataView();

                LlenarTabla();

                dataGrid.ItemsSource = view;
            }
            catch (Exception)
            {
            }
        }

        private async void btnNuevo_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["Opcion"] = Opciones.Nuevo;

            await Shell.Current.GoToAsync($"{nameof(CatalogoPrecioDetallePage)}");
        }

        private async void btnConsultar_Clicked(object sender, EventArgs e)
        {
            string cCodigo = Application.Current.Properties["Cab_cCatalogo"] as string;

            if (General.CE(cCodigo) != "")
            {
                Application.Current.Properties["Opcion"] = Opciones.Consulta;

                await Shell.Current.GoToAsync($"{nameof(CatalogoPrecioDetallePage)}");
            }
        }

        private async void btnModificar_Clicked(object sender, EventArgs e)
        {
            string cCodigo = Application.Current.Properties["Cab_cCatalogo"] as string;

            if (General.CE(cCodigo) != "")
            {
                Application.Current.Properties["Opcion"] = Opciones.Editar;

                await Shell.Current.GoToAsync($"{nameof(CatalogoPrecioDetallePage)}");
            }
        }

        private async void Eliminar()
        {
            try
            {
                string cCatalogo = Application.Current.Properties["Cab_cCatalogo"] as string;
                string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;
                string cOpcion = Application.Current.Properties["Opcion"] as string;
                string cCodigo = Application.Current.Properties["Tpr_cCodigo"] as string;
                string cCorrel = Application.Current.Properties["Cpr_cCorrelativo"] as string;


                VTD_CATALOGO_PRECIO_Entity oEntidad = new VTD_CATALOGO_PRECIO_Entity();

                oEntidad.Accion = "ELIMINAR";
                oEntidad.Emp_cCodigo = cEmpresa;
                oEntidad.Cab_cCatalogo = cCatalogo;
                oEntidad.Tpr_cCodigo = cCodigo;
                oEntidad.Cpr_cCorrelativo = cCorrel;


                string resultPost = ProcedimientosAPI.GetPostEliminarPrecio(oEntidad);
                MENSAJE_Entity oResult = JsonConvert.DeserializeObject<MENSAJE_Entity>(resultPost);

                if (oResult.Resultado == "OK")
                {
                    if (oResult.FilasAfectadas > 0)
                        await DisplayAlert(Title, "Se eliminó correctamente el registro", "OK");
                    else
                        await DisplayAlert(Title, "No se eliminó correctamente el registro", "OK");
                }
                else
                {
                    await DisplayAlert(Title, oResult.Mensaje, "OK");
                }


            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool answer = await DisplayAlert("Eliminar", "Deseas eliminar el registro seleccionado", "SI", "NO");

                if (answer == true)
                {
                    Eliminar();
                    LlenarTabla();
                }

            }
            catch (Exception ex)
            {

                await DisplayAlert("Mensaje", ex.Message, "Aceptar");
            }
        }

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//DefaultPage");
        }

        private void filterText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != null)
            {
                view.RowFilter = "Cab_cCatalogo like '%" + e.NewTextValue + "%' or Producto like '%" + e.NewTextValue + "%'  or TipoPrecio like '%" + e.NewTextValue + "%' ";

            }
        }

        private void dataGrid_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            try
            {
                Application.Current.Properties["Cab_cCatalogo"] = dataGrid.GetCellValue(e.AddedItems[0], "Cab_cCatalogo");
                Application.Current.Properties["Tpr_cCodigo"] = dataGrid.GetCellValue(e.AddedItems[0], "Tpr_cCodigo");
                Application.Current.Properties["Cpr_cCorrelativo"] = dataGrid.GetCellValue(e.AddedItems[0], "Cpr_cCorrelativo");
            }
            catch (Exception)
            {
            }

        }

        private async void LlenarTabla()
        {
            try
            {
                var oEntidad = new CatalogoPrecioViewModel();
                view.Table = oEntidad.GetData();

            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", ex.Message, "OK");
                return;
            }
        }

        private void btnRefrescar_Clicked(object sender, EventArgs e)
        {
            LlenarTabla();
        }

        private void cboTipoPrecio_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {

        }

        private void cboTipoPrecio_FilterCollectionChanged(object sender, Syncfusion.XForms.ComboBox.FilterCollectionChangedEventArgs e)
        {

        }
    }
}
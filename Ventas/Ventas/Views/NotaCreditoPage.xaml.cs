using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ventas.Extensions;
using Ventas.Models;
using Ventas.ViewModels;
using Syncfusion.SfDataGrid.XForms;
using Newtonsoft.Json;


namespace Ventas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotaCreditoPage : ContentPage
    {
        string cAnio = Application.Current.Properties["Pan_cAnio"] as string;
        string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;

        DataView view;
        public NotaCreditoPage()
        {
            InitializeComponent();
            try
            {
                dataGrid.CanUseViewFilter = true;
                view = new DataView();

                string cPeriodo = Application.Current.Properties["Per_cPeriodo"] as string;

                if (string.IsNullOrEmpty(cPeriodo))
                {
                    cPeriodo = General.Right("00" + DateTime.Now.Month.ToString(), 2);
                    cboPeriodo.SelectedIndex = Convert.ToInt16(General.NE(cPeriodo) - 1);
                }
                else
                {
                    LlenarTabla();
                }





                dataGrid.ItemsSource = view;


            }
            catch (Exception)
            {
            }
        }

        private async void btnNuevo_Clicked(object sender, EventArgs e)
        {
            //Application.Current.Properties["Opcion"] = Opciones.Nuevo;
            await Shell.Current.GoToAsync($"{nameof(NotaCreditoDetallePage)}");
        }

        private async void btnConsultar_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(NotaCreditoDetallePage)}");
        }

        private async void btnModificar_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(NotaCreditoDetallePage)}");
        }
        private void LlenarTabla()
        {
            var oEntidad = new NotaCreditoViewModel();
            view.Table = oEntidad.GetData();

        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {

                bool answer = await DisplayAlert("Eliminar", "Deseas eliminar el registro seleccionado", "SI", "NO");

                if (answer == true)
                {
                    await DisplayAlert("Eliminar", "Se eliminó el registro seleccionado", "Aceptar");

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

        private void cboPeriodo_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            Meses selectedValue = e.Value as Meses;

            Application.Current.Properties["Per_cPeriodo"] = selectedValue.ID;

            LlenarTabla();

        }

        private void cboPeriodo_FilterCollectionChanged(object sender, Syncfusion.XForms.ComboBox.FilterCollectionChangedEventArgs e)
        {

        }

        private void btnRefrescar_Clicked(object sender, EventArgs e)
        {
            LlenarTabla();
        }

        private void filterText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != null)
            {
                view.RowFilter = "Ncm_cNumMov like '%" + e.NewTextValue + "%' or tipoDoc like '%" + e.NewTextValue + "%'  or Ncm_cSerieDoc like '%" + e.NewTextValue + "%'  or Ncm_cNumDoc like '%" + e.NewTextValue + "%'  or cliente like '%" + e.NewTextValue + "%'   or validaSunat like '%" + e.NewTextValue + "%' ";
            }
        }

        private void dataGrid_SelectionChanged(object sender, Syncfusion.SfDataGrid.XForms.GridSelectionChangedEventArgs e)
        {
            try
            {
                Application.Current.Properties["Ncm_cNumMov"] = dataGrid.GetCellValue(e.AddedItems[0], "Ncm_cNumMov");
            }
            catch (Exception)
            {
            }
        }

        private void dataGrid_QueryRowHeight(object sender, Syncfusion.SfDataGrid.XForms.QueryRowHeightEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                e.Height = SfDataGridHelpers.GetRowHeight(dataGrid, e.RowIndex);
                e.Handled = true;
            }
        }

        private void btnImprimir_Clicked(object sender, EventArgs e)
        {

        }

        private void btnEnviar_Clicked(object sender, EventArgs e)
        {

        }
        
    }
}
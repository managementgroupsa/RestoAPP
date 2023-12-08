using Newtonsoft.Json;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoAPP.Models;
using RestoAPP.Extensions;
using RestoAPP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestoAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TipoCambioPage : ContentPage
    {
        DataView view;
        public TipoCambioPage()
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
            Application.Current.Properties["Opcion"] = Opciones.Nuevo;

            await Shell.Current.GoToAsync($"{nameof(TipoCambioDetallePage)}");
        }

        private async void btnConsultar_Clicked(object sender, EventArgs e)
        {
            string cFecha = Application.Current.Properties["Tca_dFecha"] as string;

            if (General.CE(cFecha) != "")
            {
                Application.Current.Properties["Opcion"] = Opciones.Consulta;

                await Shell.Current.GoToAsync($"{nameof(TipoCambioDetallePage)}");
            }
        }

        private async void btnModificar_Clicked(object sender, EventArgs e)
        {
            string cFecha = Application.Current.Properties["Tca_dFecha"] as string;

            if (General.CE(cFecha) != "")
            {
                Application.Current.Properties["Opcion"] = Opciones.Editar;

                await Shell.Current.GoToAsync($"{nameof(TipoCambioDetallePage)}");
            }
        }

        private async void Eliminar()
        {
            try
            {
                string cFecha = Application.Current.Properties["Tca_dFecha"] as string;
                string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;
                string cOpcion = Application.Current.Properties["Opcion"] as string;


                CNT_TIPO_CAMBIO_Entity oEntidad = new CNT_TIPO_CAMBIO_Entity();

                oEntidad.Accion = "ELIMINAR";
                oEntidad.Emp_cCodigo = cEmpresa;
                oEntidad.Tca_dFecha = General.FechaISO(cFecha);// General.FE(General.Left(cFecha,10)).ToString("yyyy-MM-dd");
                oEntidad.Tca_cCodigoOrigen = "038";
                oEntidad.Tca_cCodigoDestino  = "040";


                string resultPost = ProcedimientosAPI.GetPostEliminarTipoCambio(oEntidad);
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
                view.RowFilter = "Tca_dFecha like '%" + e.NewTextValue + "%' ";
            }
        }

        private void dataGrid_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            try
            {
                Application.Current.Properties["Tca_dFecha"] = dataGrid.GetCellValue(e.AddedItems[0], "Tca_dFecha");
            }
            catch (Exception)
            {
            }

        }

        private void LlenarTabla()
        {
            var oEntidad = new TipoCambioViewModel();
            view.Table = oEntidad.GetData();

        }

        private void btnRefrescar_Clicked(object sender, EventArgs e)
        {
            LlenarTabla();
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
    }
}
using Newtonsoft.Json;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Data;
using RestoAPP.Extensions;
using RestoAPP.Models;
using RestoAPP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace RestoAPP.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientesPage : ContentPage
    {
        DataView view;

        public ClientesPage()
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

            await Shell.Current.GoToAsync($"{nameof(ClientesDetallePage)}");
        }

        private async void btnConsultar_Clicked(object sender, EventArgs e)
        {
            string cCodigo = Application.Current.Properties["Ent_cCodEntidad"] as string;

            if (General.CE(cCodigo) != "")
            {
                Application.Current.Properties["Opcion"] = Opciones.Consulta;

                await Shell.Current.GoToAsync($"{nameof(ClientesDetallePage)}");
            }
        }

        private async void btnModificar_Clicked(object sender, EventArgs e)
        {
            string cCodigo = Application.Current.Properties["Ent_cCodEntidad"] as string;

            if (General.CE(cCodigo) != "")
            {
                Application.Current.Properties["Opcion"] = Opciones.Editar;

                await Shell.Current.GoToAsync($"{nameof(ClientesDetallePage)}");
            }
        }

        private async void Eliminar()
        {
            try
            {
                string cCodigoEntidad = Application.Current.Properties["Ent_cCodEntidad"] as string;
                string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;
                string cOpcion = Application.Current.Properties["Opcion"] as string;


                CNM_ENTIDAD_Entity oEntidad = new CNM_ENTIDAD_Entity();

                oEntidad.Accion = "ELIMINAR";
                oEntidad.Emp_cCodigo = cEmpresa;
                oEntidad.Ten_cTipoEntidad = "C";
                oEntidad.Ent_cCodEntidad = cCodigoEntidad;


                string resultPost = ProcedimientosAPI.GetPostEliminarEntidad(oEntidad);
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
                view.RowFilter = "Ent_cPersona like '%" + e.NewTextValue + "%' or Ent_nRuc like '%" + e.NewTextValue + "%'  or Ent_cCodEntidad like '%" + e.NewTextValue + "%' ";
            }
        }

        private void dataGrid_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            try
            {
                Application.Current.Properties["Ent_cCodEntidad"] = dataGrid.GetCellValue(e.AddedItems[0], "Ent_cCodEntidad");
            }
            catch (Exception)
            {
            }

        }

        private async void LlenarTabla()
        {

            try
            {
                var oEntidad = new ClientesViewModel();
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
    }


}

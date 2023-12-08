using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RestoAPP.Extensions;
using RestoAPP.Models;
using RestoAPP.ViewModels;
using Syncfusion.SfDataGrid.XForms;
using Newtonsoft.Json;

using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.IO;
using Xamarin.Essentials;

namespace RestoAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VentasPage : ContentPage
    {
        string cAnio = Application.Current.Properties["Pan_cAnio"] as string;
        string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;

        DataView view;

        public VentasPage()
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LlenarTabla();
            dataGrid.GridColumnSizer.Refresh(true);
        }


        private DataTable LlenaMeses()
        {
            List<string> oLista = new List<string>();

            CNT_PERIODO_Entity oEntidad = new CNT_PERIODO_Entity();

            oEntidad.Accion = "BUSCARTODOS";
            oEntidad.Emp_cCodigo = cEmpresa;
            oEntidad.Pan_cAnio = cAnio;


            string result = ProcedimientosAPI.GetPostBuscarMeses(oEntidad);

            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
            dt.TableName = "Meses";
            return dt;
        }

        private async void btnNuevo_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["Opcion"] = Opciones.Nuevo;

            await Shell.Current.GoToAsync($"{nameof(VentasDetallePage)}");
        }

        private async void btnConsultar_Clicked(object sender, EventArgs e)
        {
            string cNummov = Application.Current.Properties["Dvm_cNumMov"] as string;

            if (General.CE(cNummov) != "")
            {
                Application.Current.Properties["Opcion"] = Opciones.Consulta;

                await Shell.Current.GoToAsync($"{nameof(VentasDetallePage)}");
                //await Shell.Current.Navigation.PushModalAsync(new VentasDetallePage());
            }
        }

        private async void btnModificar_Clicked(object sender, EventArgs e)
        {
            string cNummov = Application.Current.Properties["Dvm_cNumMov"] as string;
            string cValidaSunat = Application.Current.Properties["ValidaSunat"] as string;


            if (string.IsNullOrEmpty(cValidaSunat))
            {
                if (General.CE(cNummov) != "")
                {
                    Application.Current.Properties["Opcion"] = Opciones.Editar;

                    await Shell.Current.GoToAsync($"{nameof(VentasDetallePage)}");

                    //await Shell.Current.Navigation.PushModalAsync(new VentasDetallePage());


                }
            }
            else
            {
                await DisplayAlert("Validación - Comprobante", "El comprobante no puede eliminarse por ser informado por la SUNAT", "OK");
            }
        }

        private async void Eliminar()
        {
            try
            {
                string cCodigoEntidad = Application.Current.Properties["Ent_cCodEntidad"] as string;
                string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;
                string cAnio = Application.Current.Properties["Pan_cAnio"] as string;
                string cNummov = Application.Current.Properties["Dvm_cNumMov"] as string;

                VTM_DOC_VENTA_Entity oEntidad = new VTM_DOC_VENTA_Entity();

                oEntidad.Accion = "BORRAR";
                oEntidad.Emp_cCodigo = cEmpresa;
                oEntidad.Pan_cAnio = cAnio;
                oEntidad.Dvm_cNumMov = cNummov;


                string resultPost = ProcedimientosAPI.GetPostEliminarVenta_CAB(oEntidad);
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
            string cNummov = Application.Current.Properties["Dvm_cNumMov"] as string;
            string cValidaSunat = Application.Current.Properties["ValidaSunat"] as string;


            if (string.IsNullOrEmpty(cValidaSunat))
            {
                if (General.CE(cNummov) != "")
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
            }
            else
            {
                await DisplayAlert("Validación - Comprobante", "El comprobante no puede eliminarse por ser informado por la SUNAT", "OK");
            }
        }

        private async void Anular()
        {
            try
            {
                string cCodigoEntidad = Application.Current.Properties["Ent_cCodEntidad"] as string;
                string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;
                string cAnio = Application.Current.Properties["Pan_cAnio"] as string;
                string cNummov = Application.Current.Properties["Dvm_cNumMov"] as string;

                VTM_DOC_VENTA_Entity oEntidad = new VTM_DOC_VENTA_Entity();

                oEntidad.Accion = "ANULAR";
                oEntidad.Emp_cCodigo = cEmpresa;
                oEntidad.Pan_cAnio = cAnio;
                oEntidad.Dvm_cNumMov = cNummov;


                string resultPost = ProcedimientosAPI.GetPostAnularVenta_CAB(oEntidad);
                MENSAJE_Entity oResult = JsonConvert.DeserializeObject<MENSAJE_Entity>(resultPost);

                if (oResult.Resultado == "OK")
                {
                    if (oResult.FilasAfectadas > 0)
                        await DisplayAlert(Title, "Se Anuló correctamente el registro", "OK");
                    else
                        await DisplayAlert(Title, "No se Anuló correctamente el registro", "OK");
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

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//DefaultPage");
        }

        private void filterText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != null)
            {
                view.RowFilter = "dvm_cNumMov like '%" + e.NewTextValue + "%' or tipoDoc like '%" + e.NewTextValue + "%'  or dvm_cSerieDoc like '%" + e.NewTextValue + "%'  or dvm_cNumDoc like '%" + e.NewTextValue + "%'  or cliente like '%" + e.NewTextValue + "%'   or validaSunat like '%" + e.NewTextValue + "%' ";
            }
        }

        private void dataGrid_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            try
            {
                Application.Current.Properties["Dvm_cNumMov"] = dataGrid.GetCellValue(e.AddedItems[0], "dvm_cNumMov");
                Application.Current.Properties["ValidaSunat"] = dataGrid.GetCellValue(e.AddedItems[0], "validaSunat");
            }
            catch (Exception)
            {
            }

        }

        private void LlenarTabla()
        {
            var oEntidad = new VentasViewModel();
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

        private void dataGrid_QueryRowHeight(object sender, QueryRowHeightEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                e.Height = SfDataGridHelpers.GetRowHeight(dataGrid, e.RowIndex);
                e.Handled = true;
            }
        }

        public async Task<int> ReadCountAsync()
        {
            var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "count.txt");

            if (backingFile == null || !File.Exists(backingFile))
            {
                return 0;
            }

            var count = 0;
            using (var reader = new StreamReader(backingFile, true))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (int.TryParse(line, out var newcount))
                    {
                        count = newcount;
                    }
                }
            }

            return count;
        }

        private async void btnImprimir_Clicked(object sender, EventArgs e)
        {
            try
            {
                string cNummov = Application.Current.Properties["Dvm_cNumMov"] as string;
                string cValidaSunat = Application.Current.Properties["ValidaSunat"] as string;

                if (cValidaSunat == Estado_Sunat.Aceptado)
                {
                    Nubefact oPreliminar = new Nubefact();
                    string urlPDF = oPreliminar.ConsultarComprobanteVenta(cEmpresa, cAnio, cNummov);
                    await Browser.OpenAsync(urlPDF, BrowserLaunchMode.SystemPreferred);
                }
                else
                {
                    await DisplayAlert("Validación - Comprobante", "El comprobante debe de ACEPTADO por la SUNAT", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Mensaje", ex.Message, "Aceptar");

            }
        }

        private async void btnEnviar_Clicked(object sender, EventArgs e)
        {
            try
            {
                string cNummov = Application.Current.Properties["Dvm_cNumMov"] as string;
                string cValidaSunat = Application.Current.Properties["ValidaSunat"] as string;

                if (cValidaSunat == Estado_Sunat.Aceptado)
                {
                    await DisplayAlert("Validación - Comprobante", "El comprobante ya fue enviado a la SUNAT", "OK");
                }
                else
                {
                    Nubefact oPreliminar = new Nubefact();
                    string cRespuesta = oPreliminar.GenerarComprobanteVenta(cEmpresa, cAnio, cNummov, "");

                    if (!string.IsNullOrEmpty(cRespuesta))

                        if (cRespuesta.Left(5) == "Error")
                            await DisplayAlert("Validación - Comprobante", cRespuesta, "OK");
                        else
                            await Browser.OpenAsync(cRespuesta, BrowserLaunchMode.SystemPreferred);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Mensaje", ex.Message, "Aceptar");

            }
        }

        private async void btnAnular_Clicked(object sender, EventArgs e)
        {
            string cNummov = Application.Current.Properties["Dvm_cNumMov"] as string;
            string cValidaSunat = Application.Current.Properties["ValidaSunat"] as string;


            if (string.IsNullOrEmpty(cValidaSunat))
            {
                if (General.CE(cNummov) != "")
                {
                    try
                    {
                        bool answer = await DisplayAlert("Anular", "Deseas Anular el registro seleccionado", "SI", "NO");

                        if (answer == true)
                        {
                            Anular();
                            LlenarTabla();
                        }

                    }
                    catch (Exception ex)
                    {

                        await DisplayAlert("Mensaje", ex.Message, "Aceptar");
                    }
                }
            }
            else
            {
                await DisplayAlert("Validación - Comprobante", "El comprobante no puede Anularse por ser informado por la SUNAT", "OK");
            }
        }
    }
}
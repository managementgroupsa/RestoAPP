using Newtonsoft.Json;
using RestoAPP.Extensions;
using RestoAPP.Models;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json.Converters;
using RestoAPP.ViewModels;
using Syncfusion.ListView.XForms;
using Syncfusion.SfRangeSlider.XForms;

namespace RestoAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PedidosDetallePage : ContentPage
    {
        private ObservableCollection<VTD_RESTO_APERTURA_PEDIDO_Entity> pedidos;

        DataTable dtDetalle = new DataTable();
        DataTable dtPedidos= new DataTable();
        
        string Ped_cNummov = Application.Current.Properties["Ped_cNummov"] as string;
        string Res_cNummov = Application.Current.Properties["Res_cNummov"] as string;
        string Pan_cAnio = Application.Current.Properties["Pan_cAnio"] as string;
        string Cab_cCatalogo = Application.Current.Properties["Cab_cCatalogo"] as string;
        string Emp_cCodigo = Application.Current.Properties["Emp_cCodigo"] as string;
        string cOpcion = Application.Current.Properties["Opcion"] as string;
        string Usu_cCodUsuario = Application.Current.Properties["Usu_cCodUsuario"] as string;
        string Pvt_cCodigo = Application.Current.Properties["Pvt_cCodigo"] as string;
        string Pvt_cDescripcion = Application.Current.Properties["Pvt_cDescripcion"] as string;
        string Mes_cCodigo = Application.Current.Properties["Mes_cCodigo"] as string;

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

        async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        public PedidosDetallePage()
        {
            cOpcion = Opciones.Editar;

            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);

            

            //''dtDetalle = Propiedades.CreateEntityToDataTable(typeof(VTD_RESTO_APERTURA_Entity), Title);
            //''dtPedidos = Propiedades.CreateEntityToDataTable(typeof(VTD_RESTO_APERTURA_PEDIDO_Entity), Title);


            

            

            if (cOpcion == Opciones.Nuevo)
            {
                LimpiaDatos();

                LlenaGrillaPedido();

                pedidos = GetPedidosFromApi(dtDetalle);
            }

            if (cOpcion == Opciones.Editar)
            {
                LlenaGrillaPedido();

                pedidos = GetPedidosFromApi(dtDetalle);
            }

            listView.ItemsSource = pedidos;
        }


        private ObservableCollection<VTD_RESTO_APERTURA_PEDIDO_Entity> GetPedidosFromApi(DataTable dt)
        {
            var collection = new ObservableCollection<VTD_RESTO_APERTURA_PEDIDO_Entity>();

            foreach (System.Data.DataRow row in dt.Rows)
            {
                var persona = new VTD_RESTO_APERTURA_PEDIDO_Entity
                {
                    Ped_nItem = Convert.ToInt32(row["Ped_nItem"]),
                    Ped_nCantidad = Convert.ToInt32(row["Ped_nCantidad"]),
                    Cab_cDescripLarga = row["Cab_cDescripLarga"].ToString(),
                    Ped_cComentario = row["Ped_cComentario"].ToString(),
                    Cab_cCatalogo = row["Cab_cCatalogo"].ToString()
                };

                collection.Add(persona);
            }

            return collection;
        }


        private void OnIncrementClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var item = button?.CommandParameter as VTD_RESTO_APERTURA_PEDIDO_Entity;

            if (item != null)
            {
                item.Ped_nCantidad ++;
                UpdateListViewItem(item);
            }
        }

        private void OnDecrementClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var item = button?.CommandParameter as VTD_RESTO_APERTURA_PEDIDO_Entity;

            if (item != null && item.Ped_nCantidad > 0)
            {
                item.Ped_nCantidad--;
                UpdateListViewItem(item);
            }
        }

        private async void OnCommentClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var item = button?.CommandParameter as VTD_RESTO_APERTURA_PEDIDO_Entity;

            if (item != null)
            {
                // Muestra un cuadro de diálogo de entrada de texto
                var comentario = await DisplayPromptAsync("Comentario", "Ingrese un comentario:", initialValue: item.Ped_cComentario);

                // Verifica si el usuario ingresó un comentario
                if (!string.IsNullOrEmpty(comentario))
                {
                    // Actualiza el comentario en el modelo
                    item.Ped_cComentario  = comentario;
                    UpdateListViewItem(item);
                }
            }
        }

        private void UpdateListViewItem(VTD_RESTO_APERTURA_PEDIDO_Entity item)
        {
            // Notifica el cambio en la propiedad Cantidad para que la interfaz de usuario se actualice
            var index = pedidos.IndexOf(item);
            listView.LayoutManager.ScrollToRowIndex(index, Syncfusion.ListView.XForms.ScrollToPosition.Center, true);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LlenaGrillaPedido();
          //  grdDetalle.GridColumnSizer.Refresh(true);
        }

        private void LlenaGrillaPedido()
        {
            try
            {
                VTD_RESTO_APERTURA_PEDIDO_Entity oEntidad = new VTD_RESTO_APERTURA_PEDIDO_Entity();

                oEntidad.Accion = "BUSCARTODOS";
                oEntidad.Emp_cCodigo = Emp_cCodigo;
                oEntidad.Pan_cAnio = Pan_cAnio;
                oEntidad.Res_cNummov = Res_cNummov;
                oEntidad.Pvt_cCodigo = Pvt_cCodigo;

                if (cOpcion == Opciones.Nuevo)
                    oEntidad.Ped_cNummov = "";
                else
                    oEntidad.Ped_cNummov = Ped_cNummov;

                oEntidad.Ped_cUser = Usu_cCodUsuario;

                string result = ProcedimientosAPI.BuscarPedidosPorUsuario(oEntidad);

                dtDetalle = JsonConvert.DeserializeObject<DataTable>(result);
                dtDetalle.TableName = Title;
               // grdDetalle.ItemsSource = dtDetalle;
            }
            catch (Exception ex)
            {

            }
        }

        private void LimpiaDatos()
        {
        }
        private async Task<bool> GrabarPedido(string cPed_cNummov)
        {
            bool bEstado = false;

            try
            {
                PEDIDO_Entity oEntidad = new PEDIDO_Entity ();

                oEntidad.Cabecera = await LlenaObjetoCabecera(cPed_cNummov);
                oEntidad.Detalle  = await LlenaObjetoDetalle(cPed_cNummov);
                oEntidad.Pedidos  = await LlenaObjetoPedidos(cPed_cNummov);

                string resultPost = ProcedimientosAPI.GetPostGrabarPedido(oEntidad);

                MENSAJE_Entity oResult = JsonConvert.DeserializeObject<MENSAJE_Entity>(resultPost);

                if (oResult.Resultado == "OK")
                {
                    if (oResult.FilasAfectadas > 0)
                    {
                        await DisplayAlert(Title, "Se grabo correctamente el registro", "OK");
                    }
                    else
                    {
                        await DisplayAlert(Title, "No se grabo correctamente el registro", "OK");
                        return false;
                    }
                }
                else
                {
                    await DisplayAlert(Title, oResult.Mensaje, "OK");
                    return false;
                }

                bEstado = true;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }

            return bEstado;
        }
        private async Task<List<VTD_RESTO_APERTURA_PEDIDO_Entity>> LlenaObjetoPedidos(string cPed_cNummov)
        {
            List<VTD_RESTO_APERTURA_PEDIDO_Entity> oEntidades = new List<VTD_RESTO_APERTURA_PEDIDO_Entity>();

            try
            {
                foreach (System.Data.DataRow oRow in dtPedidos.Rows)
                {
                    

                    VTD_RESTO_APERTURA_PEDIDO_Entity oEntidad = new VTD_RESTO_APERTURA_PEDIDO_Entity();

                    
                    oEntidad.Accion = "INSERTAR";
                    oEntidad.Emp_cCodigo = Emp_cCodigo;
                    oEntidad.Pan_cAnio = Pan_cAnio;
                    oEntidad.Res_cNummov = Res_cNummov;
                    oEntidad.Pvt_cCodigo = Pvt_cCodigo;
                    oEntidad.Ped_cNummov = cPed_cNummov;
                    oEntidad.Ped_nItem = Convert.ToInt16(oRow["Ped_nItem"]);
                    oEntidad.Cab_cCatalogo = General.CE(oRow["Cab_cCatalogo"]);
                    oEntidad.Ped_nCantidad = Convert.ToInt16(oRow["Ped_nCantidad"]);
                    oEntidad.Ped_cComentario = General.CE(oRow["Ped_cComentario"]);
                    oEntidad.Ped_cEstado = "A";
                    oEntidad.Ped_cUser = Usu_cCodUsuario ;

                    oEntidades.Add(oEntidad);
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }

            return oEntidades;


        }
        private async Task<VTD_RESTO_APERTURA_Entity> LlenaObjetoDetalle(string cPed_cNummov)
        {
            VTD_RESTO_APERTURA_Entity oEntidad = new VTD_RESTO_APERTURA_Entity();

            try
            {
                oEntidad.Accion = "INSERTAR";
                oEntidad.Emp_cCodigo = Emp_cCodigo;
                oEntidad.Pan_cAnio = Pan_cAnio;
                oEntidad.Res_cNummov = Res_cNummov;
                oEntidad.Pvt_cCodigo = Pvt_cCodigo;
                oEntidad.Ped_cNummov = cPed_cNummov;
                oEntidad.Ten_cTipoEntidad  = "";
                oEntidad.Ent_cCodEntidad = "";
                oEntidad.Ape_cEstado  = "A";
                oEntidad.Ape_cUser = Usu_cCodUsuario;

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }

            return oEntidad;

        }
        private async Task<VTM_RESTO_APERTURA_Entity> LlenaObjetoCabecera(string cPed_cNummov)
        {
            VTM_RESTO_APERTURA_Entity oEntidad = new VTM_RESTO_APERTURA_Entity();

            try
            {
                oEntidad.Accion = "INSERTAR";
                oEntidad.Emp_cCodigo = Emp_cCodigo;
                oEntidad.Pan_cAnio = Pan_cAnio;
                oEntidad.Res_cNummov = Res_cNummov;
                oEntidad.Pvt_cCodigo = Pvt_cCodigo;
                oEntidad.Per_cPeriodo = General.Right(General.Left(General.FechaISO(DateTime.Today.ToString()), 6), 2);
                oEntidad.Ape_dFecha = DateTime.Today;
                oEntidad.Ape_cEstado = "A";
                oEntidad.Ape_cComentario = "";
                oEntidad.Ape_cUser  = Usu_cCodUsuario ;
                
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }

            return oEntidad;
        }

        //private void grdDetalle_QueryRowHeight(object sender, Syncfusion.SfDataGrid.XForms.QueryRowHeightEventArgs e)
        //{
        //    if (e.RowIndex > 0)
        //    {
        //        e.Height = SfDataGridHelpers.GetRowHeight(grdDetalle, e.RowIndex);
        //        e.Handled = true;
        //    }
        //}

        //private void grdDetalle_SelectionChanged(object sender, Syncfusion.SfDataGrid.XForms.GridSelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        Application.Current.Properties["Ped_nItem"] = grdDetalle.GetCellValue(e.AddedItems[0], "Ped_nItem");
        //        Application.Current.Properties["Cab_cCatalogo"] = grdDetalle.GetCellValue(e.AddedItems[0], "Cab_cCatalogo");
                
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
    }
}
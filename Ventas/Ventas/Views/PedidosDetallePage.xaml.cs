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
        private ObservableCollection<VTD_RESTO_APERTURA_PEDIDO_Entity> ItemsCatalogo;
        private ObservableCollection<VTD_RESTO_APERTURA_PEDIDO_Entity> pedidos;
        

        DataTable dtDetalle = new DataTable();
        DataTable dtCatalogo= new DataTable();
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
            listView.IsVisible = true;
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

                LlenaCatalogo();

                ItemsCatalogo = GetProductosFromApi(dtCatalogo);
                
            }

            if (cOpcion == Opciones.Editar)
            {
                LlenaGrillaPedido();

                pedidos = GetPedidosFromApi(dtDetalle);

                LlenaCatalogo();

                ItemsCatalogo = GetProductosFromApi(dtCatalogo);
            }

            listView.ItemsSource = pedidos;

            //-------------------------------------------

            // Inicializa tu lista de productos y asígnala al origen de datos del SfListView
            
            

            productosListView.ItemDoubleTapped  += OnItemDoubleTappedEnProductosListView;
            
            //-------------------------------------------

        }

        private void LlenaCatalogo()
        {
            try
            {
                VTD_RESTO_APERTURA_PEDIDO_Entity oEntidad = new VTD_RESTO_APERTURA_PEDIDO_Entity();

                oEntidad.Accion = "BUSCARCATALOGO";
                oEntidad.Emp_cCodigo = Emp_cCodigo;

                string result = ProcedimientosAPI.BuscarCatalogo(oEntidad);

                dtCatalogo = JsonConvert.DeserializeObject<DataTable>(result);
                dtCatalogo.TableName = "Catalogo";
                // grdDetalle.ItemsSource = dtDetalle;
            }
            catch (Exception ex)
            {

            }
        }

        private ObservableCollection<VTD_RESTO_APERTURA_PEDIDO_Entity> GetProductosFromApi(DataTable dt)
        {
            var collection = new ObservableCollection<VTD_RESTO_APERTURA_PEDIDO_Entity>();

            foreach (System.Data.DataRow row in dt.Rows)
            {
                var producto = new VTD_RESTO_APERTURA_PEDIDO_Entity
                {
                    Cab_cCatalogo = row["Cab_cCatalogo"].ToString(),
                    Cab_cDescripLarga = row["Cab_cDescripLarga"].ToString(),
                    Ped_nCantidad = Convert.ToInt16(row["Ped_nCantidad"])
                };

                collection.Add(producto);
            }

            return collection;
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

        private async void OnDecrementClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var item = button?.CommandParameter as VTD_RESTO_APERTURA_PEDIDO_Entity;

            if (item != null)
            {
                // Disminuye la cantidad en uno
                item.Ped_nCantidad--;

                // Si la cantidad llega a cero, muestra un cuadro de diálogo para confirmar la eliminación
                if (item.Ped_nCantidad == 0)
                {
                    var confirmacionPage = new ConfirmacionEliminarProductoPage();

                    // Suscríbete al evento de confirmación
                    MessagingCenter.Subscribe<ConfirmacionEliminarProductoPage, bool>(this, "EliminarProducto", (senderPage, confirmacion) =>
                    {
                        // Si el usuario acepta, elimina el producto de la lista
                        if (confirmacion)
                        {
                            pedidos.Remove(item);
                        }
                        else
                        {
                            // Si el usuario no acepta, restaura la cantidad a su valor original
                            item.Ped_nCantidad=1;
                        }
                    });

                    await Navigation.PushModalAsync(confirmacionPage);

                    UpdateListViewItem(item);
                }
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

        private  void OnAgregarClicked(object sender, EventArgs e)
        {
            productosListView.ItemsSource = ItemsCatalogo; // Establece la lista de productos en el segundo SfListView
            filtroEntry.Text = string.Empty; // Limpia el filtro

            ScrollViewBusqueda.IsVisible = true; 
            ScrollViewPedido.IsVisible = false;

            BotonesCancelarProductos.IsVisible = true;
            BotonesGrabar.IsVisible = false;
            

        }

        private void OnFiltroTextChanged(object sender, TextChangedEventArgs e)
        {
            // Aplica el filtro a la lista de productos en el segundo SfListView
            var filtro = e.NewTextValue.ToLower();
            var productosFiltrados = ItemsCatalogo.Where(p => p.Cab_cCatalogo.ToLower().Contains(filtro) || p.Cab_cDescripLarga.ToLower().Contains(filtro)).ToList();
            productosListView.ItemsSource = productosFiltrados;
        }

        private void OnItemDoubleTappedEnProductosListView(object sender, Syncfusion.ListView.XForms.ItemDoubleTappedEventArgs  e)
        {
            // Maneja el evento de doble toque en el segundo SfListView
            if (e.ItemData is VTD_RESTO_APERTURA_PEDIDO_Entity productoSeleccionado)
            {
                // Verifica si el producto ya está presente en la lista inicial
                var productoExistente = pedidos.FirstOrDefault(p => p.Cab_cCatalogo == productoSeleccionado.Cab_cCatalogo);

                if (productoExistente != null)
                {
                    // Si el producto ya está en la lista, suma uno a la cantidad
                    productoExistente.Ped_nCantidad++;
                }
                else
                {
                    // Si el producto no está en la lista, agrégalo con cantidad 1
                    productoSeleccionado.Ped_nCantidad = 1;
                    pedidos.Add(productoSeleccionado);
                }

                // Oculta el segundo SfListView
                ScrollViewBusqueda.IsVisible = false;
                ScrollViewPedido.IsVisible = true;

                BotonesCancelarProductos.IsVisible = false;
                BotonesGrabar.IsVisible = true;
                


                // Oculta el teclado desenfocando el Entry
                filtroEntry.Unfocus();
            }



        }

        private void OnCancelarProductosClicked(object sender, EventArgs e)
        {
            ScrollViewBusqueda.IsVisible = false;
            ScrollViewPedido.IsVisible = true;

            BotonesCancelarProductos.IsVisible = false;
            BotonesGrabar.IsVisible = true;
            
        }

        private void OnGrabarClicked(object sender, EventArgs e)
        {

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
                dtDetalle.TableName = "Pedido";
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


    }
}
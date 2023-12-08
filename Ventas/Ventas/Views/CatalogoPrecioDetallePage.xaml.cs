using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestoAPP.Extensions;
using RestoAPP.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace RestoAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogoPrecioDetallePage : ContentPage
    {
        string cCatalogo = Application.Current.Properties["Cab_cCatalogo"] as string;
        string cCodigo = Application.Current.Properties["Tpr_cCodigo"] as string;
        string cCorrel = Application.Current.Properties["Cpr_cCorrelativo"] as string;
        string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;
        string cOpcion = Application.Current.Properties["Opcion"] as string;
        string cUsuario = Application.Current.Properties["Usu_cCodUsuario"] as string;

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }
        private void RedimencionarCombos()
        {
            Application.Current.Properties["AltoVentana"] = (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density) - 80;
            tdbcCatalogo.PickerHeight = General.NE(Application.Current.Properties["AltoVentana"]);
        }

        public CatalogoPrecioDetallePage()
        {
            InitializeComponent();

            if (cOpcion == Opciones.Nuevo)
            {
                LimpiaDatos();
                BloquearDatos(false);
            }

            if (cOpcion == Opciones.Consulta)
            {
                LlenaDatos();
                BloquearDatos(true);
            }

            if (cOpcion == Opciones.Editar)
            {
                LlenaDatos();
                BloquearDatos(false);
            }
        }

        private void BloquearDatos(bool bEstado)
        {

            Propiedades.SoloLecturaEntry(txtTipoPrecio, bEstado);
            Propiedades.SoloLecturaEntry(txtCatalogo, bEstado);


            btnGrabar.IsVisible = !bEstado;
            btnCatalogo.IsVisible = !bEstado;
            btnTipoPrecio.IsVisible = !bEstado;
            btnMoneda.IsVisible = !bEstado;
            btnFechaVigenteDesde.IsVisible = !bEstado;
            btnFechaVigenteHasta.IsVisible = !bEstado;
        }

        private void LimpiaDatos()
        {
            
        }

        private void LlenaDatos()
        {
            try
            {
                VTD_CATALOGO_PRECIO_Entity oEntidad = new VTD_CATALOGO_PRECIO_Entity();

                oEntidad.Accion = "BUSCARREGISTRO";
                oEntidad.Emp_cCodigo = cEmpresa;
                oEntidad.Cab_cCatalogo = cCatalogo ;
                oEntidad.Tpr_cCodigo = cCodigo;
                oEntidad.Cpr_cCorrelativo = cCorrel;                



                string result = ProcedimientosAPI.GetPostBuscarPrecio(oEntidad);

                result = "[" + result + "]";

                List<VTD_CATALOGO_PRECIO_Entity> response = JsonConvert.DeserializeObject<List<VTD_CATALOGO_PRECIO_Entity>>(result);

                foreach (var message in response)
                {
                    
                    txtCatalogo.Text = message.Cab_cCatalogo + " - " + message.Producto;
                    txtTipoPrecio.Text = message.Tpr_cCodigo +" - " + message.TipoPrecio;
                    txtCorrelativo.Text = message.Cpr_cCorrelativo;
                    if (message.Mon_cCodigo == "038")
                        txtMoneda.Text = "038 - SOLES";
                    else
                        txtMoneda.Text = "040 - DOLARES";
                    txtPrecio.Value = message.Cpr_nPrecio;
                    txtFechaVigenteDesde.Text = message.Cpr_dFechaVigIni.ToString("dd/MM/yyyy");
                    txtFechaVigenteHasta.Text = message.Cpr_dFechaVigFin.ToString("dd/MM/yyyy");

                    if (message.Cpr_cFlgIncImpto == "S")
                        chkIncluyeImp.IsChecked = true;
                    else
                        chkIncluyeImp.IsChecked = false;                               

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private async Task<bool> Valida()
        {
            bool bValidar = true;

            if (string.IsNullOrEmpty(txtTipoPrecio.Text))
            {
                await DisplayAlert("Tipo de Precio", "Se debe ingresar el cod. de Categoria", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(txtCatalogo.Text))
            {
                await DisplayAlert("Descripcion", "Se debe ingresar la descripción ", "OK");
                return false;
            }

            return bValidar;
        }

        private async void btnGrabar_Clicked(object sender, EventArgs e)
        {
            if (await Valida() == true)
            {

                try
                {
                    VTD_CATALOGO_PRECIO_Entity oEntidad = new VTD_CATALOGO_PRECIO_Entity();

                    oEntidad.Accion = "INSERTAR";
                    oEntidad.Emp_cCodigo = cEmpresa;
                    oEntidad.Cab_cCatalogo = General.PrimerValor(txtCatalogo.Text);
                    oEntidad.Tpr_cCodigo = General.PrimerValor(txtTipoPrecio.Text); 
                    oEntidad.Cpr_cCorrelativo = "";
                    oEntidad.Mon_cCodigo = General.PrimerValor(txtMoneda.Text);
                    oEntidad.Cpr_nPrecio = Convert.ToDecimal(txtPrecio.Value);
                    oEntidad.Cpr_nPrecioMin = 0;
                    oEntidad.Cpr_nPrecioMax = 0;
                    oEntidad.Cpr_dFechaVigIni= General.ISO_To_DateTime(General.FechaISO(txtFechaVigenteDesde.Text));
                    oEntidad.Cpr_dFechaVigFin = General.ISO_To_DateTime(General.FechaISO(txtFechaVigenteHasta.Text));
                    if (chkIncluyeImp.IsChecked == true)
                        oEntidad.Cpr_cFlgIncImpto = "S";
                    else
                        oEntidad.Cpr_cFlgIncImpto = "N";
                    oEntidad.Cpr_cEstado = "A";
                    oEntidad.Cpr_cUser = cUsuario;
                    oEntidad.nTipoCambio = 0;

                    string resultPost;

                    if (cOpcion == Opciones.Editar)
                    {
                        oEntidad.Accion = "EDITAR";
                        oEntidad.Cab_cCatalogo = cCatalogo;
                        oEntidad.Cpr_cCorrelativo = cCorrel;


                        resultPost = ProcedimientosAPI.GetPostEditarPrecio(oEntidad);
                    }
                    else
                    {
                        resultPost = ProcedimientosAPI.GetPostInsertarPrecio(oEntidad);
                    }

                    MENSAJE_Entity oResult = JsonConvert.DeserializeObject<MENSAJE_Entity>(resultPost);

                    if (oResult.Resultado == "OK")
                    {
                        if (oResult.FilasAfectadas > 0)
                        {
                            await DisplayAlert(Title, "Se grabo correctamente el registro", "OK");

                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert(Title, "No se grabo correctamente el registro", "OK");
                        }
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

        }

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void tdbcCatalogo_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            txtCatalogo.Text = e.NewValue.ToString();
            Application.Current.Properties["Cab_cCatalogo"] = General.PrimerValor(e.NewValue.ToString());
            //txtCorrelativo.Text = LlenaCorrel();

            //------------------------------
            LGM_CATALOGO_BS_Entity oEntidad = new LGM_CATALOGO_BS_Entity();

            oEntidad.Accion = "BUSCAR_REGAPP";
            oEntidad.Emp_cCodigo = cEmpresa;
            oEntidad.Cab_cCatalogo = General.PrimerValor(txtCatalogo.Text);

            string result = ProcedimientosAPI.GetPostBuscarCatalogo(oEntidad);

            result = General.ValidaJSON(result);

           
        }
        //private string LlenaCorrel()
        //{
        //    string cNumeroDoc = "";

        //    try
        //    {
        //        VTD_CATALOGO_PRECIO_Entity oEntidad = new VTD_CATALOGO_PRECIO_Entity();

        //        oEntidad.Accion = "SIGUIENTECODIGO_APP";
        //        oEntidad.Emp_cCodigo = cEmpresa;
        //        oEntidad.Cab_cCatalogo = General.PrimerValor(txtCatalogo.Text);
        //        oEntidad.Tpr_cCodigo = General.PrimerValor(txtTipoPrecio.Text);

        //        string result = ProcedimientosAPI.SiguienteNumCorrel(oEntidad);

        //        List<VTD_CATALOGO_PRECIO_Entity> response = JsonConvert.DeserializeObject<List<VTD_CATALOGO_PRECIO_Entity>>(result);

        //        foreach (var message in response)
        //        {
        //            cNumeroDoc = message.Cpr_cCorrelativo;
        //        }



        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return cNumeroDoc;
        //}

        private void btnCatalogo_Clicked(object sender, EventArgs e)
        {
            RedimencionarCombos();
            tdbcCatalogo.ItemsSource = LlenaProductos();
            tdbcCatalogo.IsOpen = true;
        }

        private List<string> LlenaProductos()
        {
            List<string> oLista = new List<string>();

            LGM_CATALOGO_BS_Entity oEntidad = new LGM_CATALOGO_BS_Entity();

            oEntidad.Accion = "BUSCARTODOS";
            oEntidad.Emp_cCodigo = cEmpresa;


            string result = ProcedimientosAPI.GetPostBuscarCatalogos(oEntidad);

            List<LGM_CATALOGO_BS_Entity> response = JsonConvert.DeserializeObject<List<LGM_CATALOGO_BS_Entity>>(result);

            foreach (var message in response)
            {
                oLista.Add(message.Cab_cCatalogo + " - " + message.Cab_cDescripLarga);
            }

            return oLista;
        }

        private void tdbcTipoPrecio_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            txtTipoPrecio.Text = e.NewValue.ToString();
            Application.Current.Properties["Tpr_cCodigo"] = General.PrimerValor(e.NewValue.ToString());
            
        }

        private void btnTipoPrecio_Clicked(object sender, EventArgs e)
        {
            RedimencionarCombos();
            tdbcTipoPrecio.ItemsSource = LlenaTipoPrecio();
            tdbcTipoPrecio.IsOpen = true;
        }

        private List<string> LlenaTipoPrecio()
        {
            List<string> oLista = new List<string>();

            VTT_TIPO_PRECIO_Entity oEntidad = new VTT_TIPO_PRECIO_Entity();

            oEntidad.Accion = "BUSCARTODOS";
            oEntidad.Emp_cCodigo = cEmpresa;

            string result = ProcedimientosAPI.GetPostBuscarTipoPrecios(oEntidad);

            List<VTT_TIPO_PRECIO_Entity> response = JsonConvert.DeserializeObject<List<VTT_TIPO_PRECIO_Entity>>(result);

            foreach (var message in response)
            {
                oLista.Add(message.Tpr_cCodigo + " - " + message.Tpr_cDescripcion);
            }

            return oLista;
        }

        private void tdbcMoneda_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            txtMoneda.Text = e.NewValue.ToString();
            Application.Current.Properties["Mon_cCodigo"] = General.PrimerValor(e.NewValue.ToString());
        }

        private void btnMoneda_Clicked(object sender, EventArgs e)
        {
            RedimencionarCombos();
            tdbcMoneda.ItemsSource = LlenaMoneda();
            tdbcMoneda.IsOpen = true;
        }
        private List<string> LlenaMoneda()
        {
            List<string> oLista = new List<string>();

            CNT_TIPO_MONEDA_Entity oEntidad = new CNT_TIPO_MONEDA_Entity();

            oEntidad.Accion = "SEL_ALL";
            oEntidad.Emp_cCodigo = cEmpresa;

            string result = ProcedimientosAPI.GetPostBuscarTipoMoneda(oEntidad);

            List<CNT_TIPO_MONEDA_Entity> response = JsonConvert.DeserializeObject<List<CNT_TIPO_MONEDA_Entity>>(result);

            foreach (var message in response)
            {
                oLista.Add(message.Mon_cCodigo + " - " + message.Mon_cNombreLargo);
            }

            return oLista;
        }                        
        

        private void dtpFechaVigenteDesde_OkButtonClicked(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        {
            DateTime date = General.FE(e.NewValue.ToString());
            txtFechaVigenteDesde.Text = date.ToString("dd/MM/yyyy");
        }

        private void btnFechaVigenteDesde_Clicked(object sender, EventArgs e)
        {
            dtpFechaVigenteDesde.IsOpen = true;
        }

        private void dtpFechaVigenteHasta_OkButtonClicked(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        {
            DateTime date = General.FE(e.NewValue.ToString());
            txtFechaVigenteHasta.Text = date.ToString("dd/MM/yyyy");

        }

        private void btnFechaVigenteHasta_Clicked(object sender, EventArgs e)
        {
            dtpFechaVigenteHasta.IsOpen = true;
        }
        
    }
}
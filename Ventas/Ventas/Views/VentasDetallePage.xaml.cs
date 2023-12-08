using Newtonsoft.Json;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Input;
using RestoAPP.Extensions;
using RestoAPP.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestoAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VentasDetallePage : ContentPage
    {
        DataTable dtDetalle = new DataTable();
        DataTable dtCuotas = new DataTable();

        double nIGV = 0.18;
        string cPuntoVenta = Application.Current.Properties["Pvt_cCodigo"] as string;
        string cNummov = Application.Current.Properties["Dvm_cNumMov"] as string;
        string cAnio = Application.Current.Properties["Pan_cAnio"] as string;
        string cCodigo = Application.Current.Properties["Cab_cCatalogo"] as string;
        string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;
        string cOpcion = Application.Current.Properties["Opcion"] as string;
        string cUsuario = Application.Current.Properties["Usu_cCodUsuario"] as string;

        

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

        public void LimpiaClientePorDefecto()
        {
            txtCodigo.Text = "";
         //   txtNumeroDocId.Text = "";
            txtRazon.Text = "";
            txtDireccion.Text = "";
            txtUbigeo.Text = "";
            txtTipoDocumento.Text = "";
            
        }

        public void ClientePorDefectoBoleta()
        {
            txtCodigo.Text = "00001";
            txtNumeroDocId.Text = "000000000000000";
            txtRazon.Text = "CLIENTES DIVERSOS ";
            txtDireccion.Text = "S/D";
            txtUbigeo.Text = "150101";
            txtTipoDocumento.Text = "07";
            txtCorreo.Text = "";
        }

        public VentasDetallePage()
        {
            NavigationPage.SetHasBackButton(this, false);

            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);


            dtDetalle = Propiedades.CreateEntityToDataTable(typeof(VTD_DOC_VENTA_Entity), Title);
            dtCuotas = Propiedades.CreateEntityToDataTable(typeof(VTM_DOC_VENTA_CUOTAS_Entity), Title);

            if (cOpcion == Opciones.Nuevo)
            {

                LimpiaDatos();
                BloquearDatos(false);

                DateTime date = DateTime.Now;
                txtTipoDoc.Text = "03 - BOLETA DE VENTA";
                txtSeriedoc.Text = "B001";

                Application.Current.Properties["Dvm_cTipoDoc"] = General.PrimerValor(txtTipoDoc.Text);
                Application.Current.Properties["Dvm_cSerieDoc"] = General.PrimerValor(txtSeriedoc.Text);


                txtNumeroDoc.Text = LlenaNumero();

                txtFechaEmision.Text = date.ToString("dd/MM/yyyy");
                txtDias.Text = "0";
                txtFechaVencimiento.Text = date.ToString("dd/MM/yyyy");
                txtTipoMoneda.Text = "038 - SOLES";
                txtTipoVenta.Text = "N - NACIONAL";
                txtModalidad.Text = "B - CATALOGADO";
                txtTipoCambio.Text = LlenaTC();

                ClientePorDefectoBoleta();


                txtEstado.Text = "A";
                txtCondicion.Text = "A";
                txtMedio.Text = "008 - EFECTIVO";

                CalculaDetalle();
                CalculaCuota();
            }

            if (cOpcion == Opciones.Consulta)
            {
                LlenaDatosCAB();
                LlenaDatosDET();
                LlenaDatosCUOTAS();
                CalculaDetalle();
                CalculaCuota();
                CalculaItems();
                BloquearDatos(true);
            }

            if (cOpcion == Opciones.Editar)
            {
                LlenaDatosCAB();
                LlenaDatosDET();
                LlenaDatosCUOTAS();
                CalculaDetalle();
                CalculaCuota();
                CalculaItems();
                BloquearDatos(false);
            }
        }

        async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        protected override void OnAppearing()
        {

           // Shell.Current.Navigating += Current_Navigating;
           
            base.OnAppearing();

            string bRespuesta = Application.Current.Properties["RepuestaBusqueda"] as string;


            if (bRespuesta == RepuestaVentanaFlotante.Aceptado)
            {
                tabVenta.SelectedIndex = 1;

                LlenaFilaDetalleVenta();

                grdDetalle.Refresh();

                Application.Current.Properties["RepuestaBusqueda"] = RepuestaVentanaFlotante.Cancelado;
            }

            string bRespuestaCuota = Application.Current.Properties["RepuestaCuota"] as string;

            if (bRespuestaCuota == RepuestaVentanaFlotante.Aceptado)
            {
                tabVenta.SelectedIndex = 2;

                LlenaFilaCuotaVenta();

                grdCuotas.Refresh();

                Application.Current.Properties["RepuestaCuota"] = RepuestaVentanaFlotante.Cancelado;
            }

            Application.Current.Properties["AltoVentana"] = (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density) - 80;

        }

        private void RedimencionarCombos()
        {
            Application.Current.Properties["AltoVentana"] = (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density) - 80;
            tdbcTipoDoc.PickerHeight = General.NE(Application.Current.Properties["AltoVentana"]);
            tdbcSerie.PickerHeight = General.NE(Application.Current.Properties["AltoVentana"]);
            tdbcVenta.PickerHeight = General.NE(Application.Current.Properties["AltoVentana"]);
            tdbcModalidad.PickerHeight = General.NE(Application.Current.Properties["AltoVentana"]);
            tdbcMoneda.PickerHeight = General.NE(Application.Current.Properties["AltoVentana"]);
            tdbcMedio.PickerHeight = General.NE(Application.Current.Properties["AltoVentana"]);
        }

        private List<string> LlenaMedioPago()
        {
            List<string> oLista = new List<string>();

            TABLA_Entity oEntidad = new TABLA_Entity();

            oEntidad.Accion = "BUSCAR_ALL";
            oEntidad.Emp_cCodigo = cEmpresa;
            oEntidad.Tab_cTabla = "074";


            string result = ProcedimientosAPI.GetPostBuscarTablas(oEntidad);

            List<TABLA_Entity> response = JsonConvert.DeserializeObject<List<TABLA_Entity>>(result);

            foreach (var message in response)
            {
                oLista.Add(message.Tab_cCodigo + " - " + message.Tab_cDescripCampo);
            }

            return oLista;
        }

        private List<string> LlenaModalidad()
        {
            List<string> oLista = new List<string>();

            TABLA_Entity oEntidad = new TABLA_Entity();

            oEntidad.Accion = "BUSCAR_ALL";
            oEntidad.Emp_cCodigo = cEmpresa;
            oEntidad.Tab_cTabla = "150";


            string result = ProcedimientosAPI.GetPostBuscarTablas(oEntidad);

            List<TABLA_Entity> response = JsonConvert.DeserializeObject<List<TABLA_Entity>>(result);

            foreach (var message in response)
            {
                oLista.Add(message.Tab_cCodigo + " - " + message.Tab_cDescripCampo);
            }

            return oLista;
        }

        private List<string> LlenaForma()
        {
            List<string> oLista = new List<string>();

            TABLA_Entity oEntidad = new TABLA_Entity();

            oEntidad.Accion = "BUSCAR_ALL";
            oEntidad.Emp_cCodigo = cEmpresa;
            oEntidad.Tab_cTabla = "152";


            string result = ProcedimientosAPI.GetPostBuscarTablas(oEntidad);

            List<TABLA_Entity> response = JsonConvert.DeserializeObject<List<TABLA_Entity>>(result);

            foreach (var message in response)
            {
                oLista.Add(message.Tab_cCodigo + " - " + message.Tab_cDescripCampo);
            }

            return oLista;
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

        private List<string> LlenaVenta()
        {
            List<string> oLista = new List<string>();

            TABLA_Entity oEntidad = new TABLA_Entity();

            oEntidad.Accion = "BUSCAR_ALL";
            oEntidad.Emp_cCodigo = cEmpresa;
            oEntidad.Tab_cTabla = "151";


            string result = ProcedimientosAPI.GetPostBuscarTablas(oEntidad);

            List<TABLA_Entity> response = JsonConvert.DeserializeObject<List<TABLA_Entity>>(result);

            foreach (var message in response)
            {
                oLista.Add(message.Tab_cCodigo + " - " + message.Tab_cDescripCampo);
            }

            return oLista;
        }

        private List<string> LlenaTipoDoc()
        {
            List<string> oLista = new List<string>();

            CNT_TIPODOC_Entity oEntidad = new CNT_TIPODOC_Entity();

            oEntidad.Accion = "SEL_VENTA_APP";
            oEntidad.Emp_cCodigo = cEmpresa;


            string result = ProcedimientosAPI.GetPostBuscarTipoDocumento(oEntidad);

            List<CNT_TIPODOC_Entity> response = JsonConvert.DeserializeObject<List<CNT_TIPODOC_Entity>>(result);

            foreach (var message in response)
            {
                oLista.Add(message.Tdo_cCodigo + " - " + message.Tdo_cNombreLargo);
            }

            return oLista;
        }

        private List<string> LlenaSerie()
        {
            List<string> oLista = new List<string>();

            LGT_SERIEDOC_Entity oEntidad = new LGT_SERIEDOC_Entity();

            oEntidad.Accion = "BUSCARVENTA";
            oEntidad.Emp_cCodigo = cEmpresa;
            oEntidad.Doc_cTipoDoc = Application.Current.Properties["Dvm_cTipoDoc"] as string;


            string result = ProcedimientosAPI.GetPostBuscarSerieDocumento(oEntidad);

            List<LGT_SERIEDOC_Entity> response = JsonConvert.DeserializeObject<List<LGT_SERIEDOC_Entity>>(result);

            foreach (var message in response)
            {
                oLista.Add(message.Doc_cSerie);
            }

            return oLista;
        }

        private string LlenaNumero()
        {
            string cNumeroDoc = "";

            try
            {
                LGT_SERIEDOC_Entity oEntidad = new LGT_SERIEDOC_Entity();

                oEntidad.Accion = "SIGUIENTENUM";
                oEntidad.Emp_cCodigo = cEmpresa;
                oEntidad.Doc_cTipoDoc = Application.Current.Properties["Dvm_cTipoDoc"] as string;
                oEntidad.Doc_cSerie = Application.Current.Properties["Dvm_cSerieDoc"] as string;


                string result = ProcedimientosAPI.GetPostBuscarSerieDocumento(oEntidad);

                List<LGT_SERIEDOC_Entity> response = JsonConvert.DeserializeObject<List<LGT_SERIEDOC_Entity>>(result);

                foreach (var message in response)
                {
                    cNumeroDoc = message.Doc_cNumInicio;
                }



            }
            catch (Exception ex)
            {
            }
            return cNumeroDoc;
        }

        private string LlenaTC()
        {
            string resultado = "";
            try
            {
                CNT_TIPO_CAMBIO_Entity oEntidad = new CNT_TIPO_CAMBIO_Entity();

                oEntidad.Accion = "SEL_TC";
                oEntidad.Emp_cCodigo = cEmpresa;
                oEntidad.Tca_dFecha = General.FechaISO(txtFechaEmision.Text);
                oEntidad.Tca_cCodigoOrigen = "038";
                oEntidad.Tca_cCodigoDestino = "040";

                string result = ProcedimientosAPI.GetPostBuscarTipoCambio(oEntidad);

                result = "[" + result + "]";

                List<CNT_TIPO_CAMBIO_Entity> response = JsonConvert.DeserializeObject<List<CNT_TIPO_CAMBIO_Entity>>(result);

                foreach (var message in response)
                {
                    resultado = General.NE(message.Tca_nVenta).ToString();
                }
            }
            catch (Exception)
            {
            }
            return resultado;
        }

        private void BloquearDatos(bool bEstado)
        {
            Propiedades.SoloLecturaEntry(txtTipoDoc, true);
            Propiedades.SoloLecturaEntry(txtSeriedoc, true);
            Propiedades.SoloLecturaEntry(txtNumeroDoc, true);
            Propiedades.SoloLecturaEntry(txtTipoVenta, true);
            Propiedades.SoloLecturaEntry(txtTipoMoneda, true);
            Propiedades.SoloLecturaEntry(txtModalidad, true);
            Propiedades.SoloLecturaEntry(txtTipoCambio, true);

            Propiedades.SoloLecturaEntry(txtNumeroDocId, bEstado);
            Propiedades.SoloLecturaEntry(txtRazon, bEstado);
            Propiedades.SoloLecturaEntry(txtDireccion, bEstado);
            Propiedades.SoloLecturaEntry(txtCorreo, bEstado);


            Propiedades.SoloLecturaEntry(txtFechaEmision, true);
            Propiedades.SoloLecturaEntry(txtFechaVencimiento, true);

            btnGrabar.IsVisible = !bEstado;

            btnAgregarFilaDet.IsVisible = !bEstado;
            btnEliminarFilaDet.IsVisible = !bEstado;
            btnAgregarFilaCuo.IsVisible = !bEstado;
            btnEliminarFilaCuo.IsVisible = !bEstado;

            btnTipoDoc.IsVisible = !bEstado;
            btnSeriedoc.IsVisible = !bEstado;
            btnFechaEmision.IsVisible = !bEstado;

            btnTipoVenta.IsVisible = !bEstado;
            btnTipoMoneda.IsVisible = !bEstado;

            btnFechaVencimiento.IsVisible = !bEstado;
            btnBuscar.IsVisible = !bEstado;
            btnMedio.IsVisible = !bEstado;
            btnModalidad.IsVisible = !bEstado;
        }

        private void LimpiaDatos()
        {
            txtTipoDoc.Text = "";
            txtSeriedoc.Text = "";
            txtTipoVenta.Text = "";
            txtTipoMoneda.Text = "";
            txtDireccion.Text = "";
            txtModalidad.Text = "";
            txtTipoCambio.Text = "";
            txtNumeroDocId.Text = "";
            txtNumeroDoc.Text = "";
            txtRazon.Text = "";

        }

        private async void LlenaDatosCAB()
        {
            try
            {
                VTM_DOC_VENTA_Entity oEntidad = new VTM_DOC_VENTA_Entity();

                oEntidad.Accion = "BUSCARREGISTRO";
                oEntidad.Emp_cCodigo = cEmpresa;
                oEntidad.Pan_cAnio = cAnio;
                oEntidad.Dvm_cNumMov = cNummov;

                string result = ProcedimientosAPI.GetPostBuscarVenta_CAB(oEntidad);

                result = "[" + result + "]";

                List<VTM_DOC_VENTA_Entity> response = JsonConvert.DeserializeObject<List<VTM_DOC_VENTA_Entity>>(result);

                foreach (var message in response)
                {
                    if (message.Dvm_cTipoDoc == "01")
                        txtTipoDoc.Text = "01 - FACTURA";
                    else
                        txtTipoDoc.Text = "03 - BOLETA DE VENTA";

                    txtSeriedoc.Text = message.Dvm_cSerieDoc;
                    txtNumeroDoc.Text = message.Dvm_cNumDoc;
                    txtFechaEmision.Text = message.Dvm_dFechaEmision.ToString("dd/MM/yyyy");
                    txtTipoCambio.Text = message.Dvm_nTipoCambio.ToString();
                    txtDias.Text = General.NE(message.Dvm_nDiasCredito).ToString();
                    txtCodigo.Text = message.Cli_cCodigo;



                    CNM_ENTIDAD_Entity oCliente = new CNM_ENTIDAD_Entity();
                    oCliente.Accion = "SEL_CODENT";
                    oCliente.Emp_cCodigo = cEmpresa;
                    oCliente.Ten_cTipoEntidad = "C";
                    oCliente.Ent_cCodEntidad = txtCodigo.Text;

                    string resultCliente = ProcedimientosAPI.GetPostBuscarEntidad(oCliente);

                    resultCliente = "[" + resultCliente + "]";

                    List<CNM_ENTIDAD_Entity> responseCliente = JsonConvert.DeserializeObject<List<CNM_ENTIDAD_Entity>>(resultCliente);

                    foreach (var messageCliente in responseCliente)
                    {
                        txtNumeroDocId.Text = messageCliente.Ent_nRuc.ToString();
                        txtRazon.Text = messageCliente.Ent_cPersona;
                        txtDireccion.Text = messageCliente.Ent_cDireccion;
                        txtUbigeo.Text = messageCliente.Dir_cUbigeo;
                        txtCorreo.Text = messageCliente.En_nMail;
                    }

                    txtMedio.Text = message.Dvm_cCondicion + " - " + message.MedioPago;

                    if (message.Dvm_cNacExt == "N")
                        txtTipoVenta.Text = "N - NACIONAL";
                    else
                        txtTipoVenta.Text = "E - EXTRANJERA";

                    if (message.Mon_cCodigo == "038")
                        txtTipoMoneda.Text = "038 - SOLES";
                    else
                        txtTipoMoneda.Text = "040 - DOLARES";

                    if (message.Dvm_cBienServ == "B")
                        txtModalidad.Text = "B - CATALOGADO";
                    else
                        txtModalidad.Text = "N - NO CATALOGADO";

                    if (message.Dvm_IncluyeImpto == "1")
                        chkIGV.IsChecked = true;
                    else
                        chkIGV.IsChecked = false;

                    if (message.Dvm_cDetraccion == "S")
                        chkDetraccion.IsChecked = true;
                    else
                        chkDetraccion.IsChecked = false;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Validación", ex.Message, "Aceptar");
            }

        }

        private void LlenaDatosDET()
        {
            try
            {
                if (General.PrimerValor(txtTipoMoneda.Text) == "038")
                {
                    PU_ME.IsHidden = true;
                    PT_ME.IsHidden = true;
                }
                else
                {
                    PU_MN.IsHidden = true;
                    PT_MN.IsHidden = true;
                }

                VTD_DOC_VENTA_Entity oEntidad = new VTD_DOC_VENTA_Entity();

                oEntidad.Accion = "BUSCARTODOS";
                oEntidad.Emp_cCodigo = cEmpresa;
                oEntidad.Pan_cAnio = cAnio;

                if (cOpcion == Opciones.Nuevo)
                    oEntidad.Dvm_cNumMov = "";
                else
                    oEntidad.Dvm_cNumMov = cNummov;

                string result = ProcedimientosAPI.GetPostBuscarVentas_DET(oEntidad);

                dtDetalle = JsonConvert.DeserializeObject<DataTable>(result);
                dtDetalle.TableName = Title;
                grdDetalle.ItemsSource = dtDetalle;
            }
            catch (Exception ex)
            {
                
            }
        }

        private void LlenaDatosCUOTAS()
        {
            try
            {
                VTM_DOC_VENTA_CUOTAS_Entity oEntidad = new VTM_DOC_VENTA_CUOTAS_Entity();

                oEntidad.Accion = "BUSCARTODOS";
                oEntidad.Emp_cCodigo = cEmpresa;
                oEntidad.Pan_cAnioVT = cAnio;

                if (cOpcion == Opciones.Nuevo)
                    oEntidad.Dvm_cNumMov = "";
                else
                    oEntidad.Dvm_cNumMov = cNummov;

                string result = ProcedimientosAPI.GetPostBuscarVentas_CUOTAS(oEntidad);

                dtCuotas = JsonConvert.DeserializeObject<DataTable>(result);
                dtCuotas.TableName = Title;
                grdCuotas.ItemsSource = dtCuotas;
            }
            catch (Exception ex)
            {
            }
        }

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            //await Shell.Current.Navigation.PopModalAsync();
        }

        private void tdbcTipoDoc_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            txtTipoDoc.Text = e.NewValue.ToString();

            if (General.PrimerValor( txtTipoDoc.Text )== "01")
            {
                txtSeriedoc.Text = "F001";
                LimpiaClientePorDefecto();
            }
            else
            {
                txtSeriedoc.Text = "B001";
                ClientePorDefectoBoleta();
            }

            Application.Current.Properties["Dvm_cTipoDoc"] = General.PrimerValor(txtTipoDoc.Text);
            Application.Current.Properties["Dvm_cSerieDoc"] = General.PrimerValor(txtSeriedoc.Text);

            txtNumeroDoc.Text = LlenaNumero();
        }

        private void tdbcSerie_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            txtSeriedoc.Text = e.NewValue.ToString();
            Application.Current.Properties["Dvm_cSerieDoc"] = General.PrimerValor(e.NewValue.ToString());

            txtNumeroDoc.Text = LlenaNumero();

        }

        private void dtpFechaEmision_OkButtonClicked(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        {
            DateTime date = General.FE(e.NewValue.ToString());
            txtFechaEmision.Text = date.ToString("dd/MM/yyyy");
            txtDias.Text = "0";
            txtFechaVencimiento.Text = date.ToString("dd/MM/yyyy");

            txtTipoCambio.Text = LlenaTC();
        }

        private void tdbcVenta_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            txtTipoVenta.Text = e.NewValue.ToString();
            Application.Current.Properties["Dvm_cNacExt"] = General.PrimerValor(e.NewValue.ToString());
        }

        private void tdbcMoneda_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            txtTipoMoneda.Text = e.NewValue.ToString();
            Application.Current.Properties["Mon_cCodigo"] = General.PrimerValor(e.NewValue.ToString());
        }

        private void tdbcModalidad_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            txtModalidad.Text = e.NewValue.ToString();
            Application.Current.Properties["Modalidad"] = General.PrimerValor(e.NewValue.ToString());

        }

        private void txtDias_TextChanged(object sender, TextChangedEventArgs e)
        {
            DateTime dateEmision = General.ISO_To_DateTime(General.FechaISO(txtFechaEmision.Text));
            DateTime dateVencimiento = dateEmision.AddDays(General.NE(txtDias.Text));
            txtFechaVencimiento.Text = dateVencimiento.ToString("dd/MM/yyyy");
        }

        private void dtpFechaVencimiento_OkButtonClicked(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        {
            DateTime dateEmision = General.ISO_To_DateTime(General.FechaISO(txtFechaEmision.Text));
            DateTime dateVencimiento = General.FE(e.NewValue.ToString());
            txtFechaVencimiento.Text = dateVencimiento.ToString("dd/MM/yyyy");

            txtDias.Text = (dateVencimiento - dateEmision).Days.ToString();

        }

        private async void btnBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNumeroDocId.Text))
                {
                    BuscarClienteLocal();

                    if (string.IsNullOrEmpty(txtRazon.Text))
                    {
                        BuscarClienteSunat();
                    }

                }
                else
                {
                    await DisplayAlert("Validación - Cliente", "Ingrese correctamente el número de Documento de Identidad", "OK");
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void BuscarClienteSunat()
        {
            try
            {
                string result = "";

                SUNAT_Entity oEntidad = new SUNAT_Entity();

                oEntidad.ruc = txtNumeroDocId.Text;


                if (txtNumeroDocId.Text.Length == 8)
                    result = ProcedimientosAPI.GetPostBuscarDNI(oEntidad);
                else
                    result = ProcedimientosAPI.GetPostBuscarRUC(oEntidad);

                result = "[" + result + "]";

                List<SUNAT_Entity> response = JsonConvert.DeserializeObject<List<SUNAT_Entity>>(result);

                foreach (var message in response)
                {
                    txtCodigo.Text = "";
                    txtNumeroDocId.Text = message.ruc;
                    txtRazon.Text = message.nombres;
                    txtDireccion.Text = message.direccion + " " + message.departamento + " - " + message.provincia + " - " + message.distrito;
                    txtUbigeo.Text = message.ubigeo;
                    txtTipoDocumento.Text = message.tipoDocumento;
                    txtEstado.Text = message.estado;
                    txtCondicion.Text = message.condiContribuyente;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void BuscarClienteLocal()
        {
            try
            {
                string result = "";
                string cNombreEncotrado = "";
                string cDireccionEncotrada = "";
                string cCodigoEntidad = "";
                string cUbigeo = "";
                string cCorreo = "";

                CNM_ENTIDAD_Entity oEntidadCli = new CNM_ENTIDAD_Entity();

                oEntidadCli.Accion = "SEL_DOCID";
                oEntidadCli.Emp_cCodigo = cEmpresa;
                oEntidadCli.Ten_cTipoEntidad = "C";
                oEntidadCli.Ent_nRuc = txtNumeroDocId.Text;

                result = ProcedimientosAPI.GetPostConsultaDocumento(oEntidadCli);

                result = "[" + result + "]";

                List<CNM_ENTIDAD_Entity> responseCli = JsonConvert.DeserializeObject<List<CNM_ENTIDAD_Entity>>(result);

                foreach (var message in responseCli)
                {
                    cNombreEncotrado = message.Ent_cPersona;
                    cDireccionEncotrada = message.Ent_cDireccion;
                    cCodigoEntidad = message.Ent_cCodEntidad;
                    cUbigeo = message.Dir_cUbigeo;
                    cCorreo = message.En_nMail;

                }

                if (!string.IsNullOrEmpty(cNombreEncotrado))
                {
                    txtRazon.Text = cNombreEncotrado;
                    txtDireccion.Text = cDireccionEncotrada;
                    txtCodigo.Text = cCodigoEntidad;
                    txtUbigeo.Text = cUbigeo;
                    txtCorreo.Text = cCorreo;
                }
                else
                {
                    LimpiaClientePorDefecto();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async Task<bool> GrabarCliente()
        {
            bool result = false;

            try
            {
                CNM_ENTIDAD_Entity oEntidad = new CNM_ENTIDAD_Entity();

                oEntidad.Accion = "INSERTAR_APP";
                oEntidad.Emp_cCodigo = cEmpresa;
                oEntidad.Ten_cTipoEntidad = "C";
                oEntidad.Ent_cCodEntidad = "";
                oEntidad.Ent_nRuc = txtNumeroDocId.Text;
                oEntidad.Ent_cPersona = txtRazon.Text;
                oEntidad.Ent_cDireccion = txtDireccion.Text;
                oEntidad.Dir_cUbigeo = txtUbigeo.Text;
                oEntidad.Ent_cEstado = General.Left(txtEstado.Text, 1);

                if (txtTipoDocumento.Text == "1")
                    oEntidad.Ent_cFlagPersona = "N";// natural
                else
                    oEntidad.Ent_cFlagPersona = "J";// juridico

                if (txtTipoDocumento.Text == "6")
                    oEntidad.Ent_cTipoDoc = "04";// ruc
                else
                    oEntidad.Ent_cTipoDoc = "01";// dni


                if (txtCondicion.Text == "HABIDO")
                {
                    oEntidad.Ent_cEstadoEntidad = "S";
                }

                if (txtCondicion.Text == "NO HABIDO")
                {
                    oEntidad.Ent_cEstadoEntidad = "N";
                }

                if (txtCondicion.Text == "NO HALLADO")
                {
                    oEntidad.Ent_cEstadoEntidad = "X";
                }

                oEntidad.Ent_dFechaApe = DateTime.Now;
                oEntidad.Ent_dFechaNac = DateTime.Now;
                oEntidad.Ent_dFecInicio = DateTime.Now;

                oEntidad.En_nMail = txtCorreo.Text;

                oEntidad.GrabaDireccion = "S";
                oEntidad.Ent_cUserCrea = cUsuario;

                string resultPost;

                resultPost = ProcedimientosAPI.GetPostInsertarEntidad(oEntidad);


                //if (cOpcion == Opciones.Editar)
                //{
                //    oEntidad.Accion = "EDITAR";
                //    oEntidad.Ent_cCodEntidad = txtCodigo.Text;

                //    resultPost = ProcedimientosAPI.GetPostEditarEntidad(oEntidad);
                //}
                //else
                //{
                //    resultPost = ProcedimientosAPI.GetPostInsertarEntidad(oEntidad);
                //}

                MENSAJE_Entity oResult = JsonConvert.DeserializeObject<MENSAJE_Entity>(resultPost);

                if (oResult.Resultado == "OK")
                {
                    if (oResult.FilasAfectadas > 0)
                    {
                        result = true;
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

            return result;
        }

        private void btnTipoDoc_Clicked(object sender, EventArgs e)
        {
            RedimencionarCombos();
            tdbcTipoDoc.ItemsSource = LlenaTipoDoc();
            tdbcTipoDoc.IsOpen = true;

            txtSeriedoc.Text = "";
            txtNumeroDoc.Text = "";
        }

        private void btnSeriedoc_Clicked(object sender, EventArgs e)
        {
            RedimencionarCombos();
            tdbcSerie.ItemsSource = LlenaSerie();
            tdbcSerie.IsOpen = true;
        }

        private void btnFechaEmision_Clicked(object sender, EventArgs e)
        {
            dtpFechaEmision.IsOpen = true;
        }

        private void btnTipoVenta_Clicked(object sender, EventArgs e)
        {
            RedimencionarCombos();
            tdbcVenta.ItemsSource = LlenaVenta();
            tdbcVenta.IsOpen = true;
        }

        private void btnTipoMoneda_Clicked(object sender, EventArgs e)
        {
            RedimencionarCombos();
            tdbcMoneda.ItemsSource = LlenaMoneda();
            tdbcMoneda.IsOpen = true;
        }

        private void btnModalidad_Clicked(object sender, EventArgs e)
        {
            RedimencionarCombos();
            tdbcModalidad.ItemsSource = LlenaModalidad();
            tdbcModalidad.IsOpen = true;
        }

        private void btnFechaVencimiento_Clicked(object sender, EventArgs e)
        {
            dtpFechaVencimiento.IsOpen = true;
        }

        private async void tabVenta_SelectionChanging(object sender, Syncfusion.XForms.TabView.SelectionChangingEventArgs e)
        {
            if (e.Index == 1)
            {
                e.Cancel = true;

                if (await ValidaCabecera() == true)
                {
                    e.Cancel = false;
                }
                else
                {
                    grdDetalle.ItemsSource = dtDetalle;
                }

            }


            if (e.Index == 2)
            {
                if (General.NE(txtDias.Text) == 0)
                {
                    e.Cancel = true;
                    await DisplayAlert("Cuotas", "Para adicionar una Couta el número de días debe ser mayor a cero", "OK");
                }
                else
                {
                    grdCuotas.ItemsSource = dtCuotas;
                }

            }
        }

        private async void btnGrabar_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["En_nMail"] = txtCorreo.Text;

            if (dtDetalle.Rows.Count <= 0)
            {
                await DisplayAlert("Grabar", "El documento no tiene detalle", "OK");
                return;
            }

            if (dtCuotas.Rows.Count <= 0 && Convert.ToInt16(txtDias.Text) != 0)
            {
                await DisplayAlert("Grabar", "El documento debe tener minimo una cuota", "OK");
                return;
            }


            if (await ValidaCabecera() == true)
            {
                try
                {
                    if (await GrabarCliente() == true)
                    {
                        BuscarClienteLocal();

                        string cNummovVenta = Application.Current.Properties["Dvm_cNumMov"] as string;

                        if (cOpcion == Opciones.Nuevo)
                        {
                            cNummovVenta = "";
                        }

                        if (await GrabarVenta(cNummovVenta))
                        {
                            await Shell.Current.Navigation.PopAsync();
                        }
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

        private async Task<bool> ValidaCabecera()
        {
            bool bValidar = true;

            if (string.IsNullOrEmpty(txtTipoDoc.Text))
            {
                await DisplayAlert("Validación - Comprobante", "Seleccione un tipo de documento", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(txtSeriedoc.Text))
            {
                await DisplayAlert("Validación - Comprobante", "Seleccione una serie de documento", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(txtNumeroDoc.Text))
            {
                await DisplayAlert("Validación - Comprobante", "El número de documento es incorrecto", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(txtFechaEmision.Text))
            {
                await DisplayAlert("Validación - Comprobante", "Selecione una fecha de emision", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(txtFechaVencimiento.Text))
            {
                await DisplayAlert("Validación - Comprobante", "Selecione una fecha de vencimiento", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(txtNumeroDocId.Text))
            {
                await DisplayAlert("Validación - Cliente", "El número de documento de identidad es incorrecto", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(txtRazon.Text))
            {
                await DisplayAlert("Validación - Cliente", "El nombre del cliente no debe ser en blanco", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(txtDireccion.Text) && (General.PrimerValor(txtTipoDoc.Text) == "01"))
            {
                await DisplayAlert("Validación - Cliente", "Para una Factura La dirección no debe ser en blanco", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(txtTipoVenta.Text))
            {
                await DisplayAlert("Validación - Tipo Venta", "El tipo de venta es incorrecto", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(txtTipoMoneda.Text))
            {
                await DisplayAlert("Validación - Moneda", "La moneda es incorrecto", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(txtMedio.Text))
            {
                await DisplayAlert("Validación - Medio de Pago", "El medio de pago es incorrecto", "OK");
                return false;
            }


            if (string.IsNullOrEmpty(txtModalidad.Text))
            {
                await DisplayAlert("Validación - Modalidad", "El tipo  de venta es incorrecto", "OK");
                return false;
            }

            if (General.NE(txtTipoCambio.Text) <= 0)
            {
                await DisplayAlert("Validación - Tipo Cambio", "El TC es incorrecto", "OK");
                return false;
            }

            return bValidar;
        }

        private async Task<bool> GrabarVenta(string cNummovVenta)
        {
            bool bEstado = false;

            try
            {
                VENTAS_Entity oEntidad = new VENTAS_Entity();

                oEntidad.Cabecera = await GeneraVentaCabecera(cNummovVenta);
                oEntidad.Detalles = await GeneraVentaDetalle(cNummovVenta);
                oEntidad.Cuotas = await GeneraVentaCuotas(cNummovVenta);

                string resultPost = ProcedimientosAPI.GetPostGrabarVenta(oEntidad, cEmpresa, cAnio, cNummovVenta);

                MENSAJE_Entity oResult = JsonConvert.DeserializeObject<MENSAJE_Entity>(resultPost);

                if (oResult.Resultado == "OK")
                {
                    if (oResult.FilasAfectadas > 0)
                    {
                        EnviarComprobante(oResult.Mensaje);
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

        private async void EnviarComprobante(string cNummovVenta)
        {
            string cMail= Application.Current.Properties["En_nMail"]  as string;

            Nubefact oPreliminar = new Nubefact();
            string resultado = oPreliminar.GenerarComprobanteVenta(cEmpresa, cAnio, cNummovVenta, cMail);
            await Browser.OpenAsync(resultado, BrowserLaunchMode.SystemPreferred);

        }

        private async Task<VTM_DOC_VENTA_Entity> GeneraVentaCabecera(string cNummovVenta)
        {
            VTM_DOC_VENTA_Entity oEntidad = new VTM_DOC_VENTA_Entity();
            decimal nTC = Convert.ToDecimal(txtTipoCambio.Text);

            try
            {
                oEntidad.Accion = "INSERTAR";
                oEntidad.Emp_cCodigo = cEmpresa;
                oEntidad.Pan_cAnio = cAnio;
                oEntidad.Dvm_cNumMov = cNummovVenta;
                oEntidad.Mon_cCodigo = General.PrimerValor(txtTipoMoneda.Text);
                oEntidad.Alm_cAlmacen = "00001";
                oEntidad.Pvt_cCodigo = cPuntoVenta;

                oEntidad.Dvm_cTipoDoc = General.PrimerValor(txtTipoDoc.Text);
                oEntidad.Dvm_cSerieDoc = General.PrimerValor(txtSeriedoc.Text);
                oEntidad.Dvm_cNumDoc = General.PrimerValor(txtNumeroDoc.Text);

                oEntidad.Cli_cTipoEntidad = "C";
                oEntidad.Cli_cCodigo = txtCodigo.Text;
                oEntidad.Dvm_cUser = cUsuario;

                oEntidad.Dvm_cCondicion = "008";
                oEntidad.Dvm_nDiasCredito = Convert.ToInt16(txtDias.Text);
                oEntidad.Tpr_cCodigo = "01";

                oEntidad.Dvm_cFormaPago = "01";

                oEntidad.Per_cPeriodo = General.Right(General.Left(General.FechaISO(txtFechaEmision.Text), 6), 2);

                oEntidad.Dvm_dFechaEmision = General.ISO_To_DateTime(General.FechaISO(txtFechaEmision.Text));
                oEntidad.Dvm_dFechaVenc = General.ISO_To_DateTime(General.FechaISO(txtFechaVencimiento.Text));
                oEntidad.Moneda = General.PrimerValor(txtTipoMoneda.Text);
                oEntidad.Dvm_nTipoCambio = Convert.ToDecimal(txtTipoCambio.Text);

                oEntidad.Ven_cTipoEntidad = "T";
                oEntidad.Ven_cCodigo = "00001";

                oEntidad.Dvm_nTDscto1 = 0;
                oEntidad.Dvm_nTDscto2 = 0;
                oEntidad.Dvm_nTDscto3 = 0;

                if (General.PrimerValor(txtTipoMoneda.Text) == "038")
                {
                    oEntidad.Dvm_nSubTotal_MN = Convert.ToDecimal(txtBase.Value);
                    oEntidad.Dvm_nTotalImpto1_MN = Convert.ToDecimal(txtIGV.Value);
                    oEntidad.Dvm_nTotalImpto2_MN = 0;
                    oEntidad.Dvm_nTotalImpto3_MN = 0;
                    oEntidad.Dvm_nTotalImpto4_MN = 0;
                    oEntidad.Dvm_nTotalImpto5_MN = 0;
                    oEntidad.Dvm_nTotal_MN = Convert.ToDecimal(txtTotal.Value);

                    oEntidad.Dvm_nSubTotal = Math.Round(Convert.ToDecimal(txtBase.Value) / nTC, 2);
                    oEntidad.Dvm_nTotalImpto1 = Math.Round(Convert.ToDecimal(txtIGV.Value) / nTC, 2);
                    oEntidad.Dvm_nTotalImpto2 = 0;
                    oEntidad.Dvm_nTotalImpto3 = 0;
                    oEntidad.Dvm_nTotalImpto4 = 0;
                    oEntidad.Dvm_nTotalImpto5 = 0;
                    oEntidad.Dvm_nTotal = Math.Round(Convert.ToDecimal(txtTotal.Value) / nTC, 2);
                }
                else
                {
                    oEntidad.Dvm_nSubTotal = Convert.ToDecimal(txtBase.Value);
                    oEntidad.Dvm_nTotalImpto1 = Convert.ToDecimal(txtIGV.Value);
                    oEntidad.Dvm_nTotalImpto2 = 0;
                    oEntidad.Dvm_nTotalImpto3 = 0;
                    oEntidad.Dvm_nTotalImpto4 = 0;
                    oEntidad.Dvm_nTotalImpto5 = 0;
                    oEntidad.Dvm_nTotal = Convert.ToDecimal(txtTotal.Value);

                    oEntidad.Dvm_nSubTotal_MN = Math.Round(Convert.ToDecimal(txtBase.Value) * nTC, 2);
                    oEntidad.Dvm_nTotalImpto1_MN = Math.Round(Convert.ToDecimal(txtIGV.Value) * nTC, 2);
                    oEntidad.Dvm_nTotalImpto2_MN = 0;
                    oEntidad.Dvm_nTotalImpto3_MN = 0;
                    oEntidad.Dvm_nTotalImpto4_MN = 0;
                    oEntidad.Dvm_nTotalImpto5_MN = 0;
                    oEntidad.Dvm_nTotal_MN = Math.Round(Convert.ToDecimal(txtTotal.Value) * nTC, 2);
                }

                oEntidad.Dvm_cFlgCatalog = "S";
                oEntidad.Dvm_cNacExt = General.PrimerValor(txtTipoVenta.Text);
                oEntidad.Dvm_cBienServ = General.PrimerValor(txtModalidad.Text);

                oEntidad.Dvm_cEstado = "A";
                oEntidad.Dvm_cUser = cUsuario;

                if (chkIGV.IsChecked == true)
                    oEntidad.Dvm_IncluyeImpto = "1";
                else
                    oEntidad.Dvm_IncluyeImpto = "0";

                if (chkDetraccion.IsChecked == true)
                    oEntidad.Dvm_cDetraccion = "S";
                else
                    oEntidad.Dvm_cDetraccion = "N";

                oEntidad.Dvm_cDirFactura = "01";

                if (cOpcion == Opciones.Editar)
                {
                    oEntidad.Accion = "EDITAR";
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }

            return oEntidad;
        }

        private async Task<List<VTD_DOC_VENTA_Entity>> GeneraVentaDetalle(string cNummovVenta)
        {
            List<VTD_DOC_VENTA_Entity> oEntidades = new List<VTD_DOC_VENTA_Entity>();

            try
            {
                foreach (System.Data.DataRow oRow in dtDetalle.Rows)
                {

                    VTD_DOC_VENTA_Entity oEntidad = new VTD_DOC_VENTA_Entity();

                    oEntidad.Accion = "INSERTAR";
                    oEntidad.Emp_cCodigo = cEmpresa;
                    oEntidad.Pan_cAnio = cAnio;
                    oEntidad.Dvm_cNumMov = cNummovVenta;
                    oEntidad.Dvd_nItem = Convert.ToInt16(oRow["Dvd_nItem"]);
                    oEntidad.Dvd_cFlgCatalog = "S";
                    oEntidad.Cab_cCatalogo = General.CE(oRow["Cab_cCatalogo"]);
                    oEntidad.Dvd_cUnidad = General.CE(oRow["Dvd_cUnidad"]);
                    oEntidad.Dvd_nItem = Convert.ToInt16(oRow["Dvd_nItem"]);
                    oEntidad.Dvd_nCantidad = Convert.ToDecimal(oRow["Dvd_nCantidad"]);
                    oEntidad.Dvd_nTasaImpto1 = Convert.ToDecimal(nIGV * 100);
                    oEntidad.Dvd_nTasaImpto2 = 0;
                    oEntidad.Dvd_nTasaImpto3 = 0;
                    oEntidad.Dvd_nTasaImpto4 = 0;
                    oEntidad.Dvd_nTasaImpto5 = 0;
                    oEntidad.Dvd_cFlgPrecioFijo = "N";

                    oEntidad.Dvd_nDsctoGral = 0;
                    oEntidad.Dvd_nDsctoLinea = 0;


                    oEntidad.Dvd_nPrecioUnitario_MN = Convert.ToDecimal(oRow["Dvd_nPrecioUnitario_MN"]);
                    oEntidad.Dvd_nPrecioNeto_MN = Convert.ToDecimal(oRow["Dvd_nPrecioNeto_MN"]);
                    oEntidad.Dvd_nImporte_MN = Convert.ToDecimal(oRow["Dvd_nImporte_MN"]);

                    oEntidad.Dvd_nPrecioUnitario = Convert.ToDecimal(oRow["Dvd_nPrecioUnitario"]);
                    oEntidad.Dvd_nPrecioNeto = Convert.ToDecimal(oRow["Dvd_nPrecioNeto"]);
                    oEntidad.Dvd_nImporte = Convert.ToDecimal(oRow["Dvd_nImporte"]);


                    oEntidad.Pro_cCodigo = "01";
                    oEntidad.FlgAfecto = "";
                    oEntidad.Dvd_cCentroCosto = "";
                    oEntidad.Dvd_cFlgCCosto = "0";

                    oEntidad.Dvd_FlagIncImp = General.CE(oRow["Dvd_FlagIncImp"]);
                    oEntidad.Dvd_cFlgCatalog = General.CE(oRow["Dvd_cFlgCatalog"]);

                    oEntidad.Dvd_FlagBienServ = General.PrimerValor(txtModalidad.Text);


                    if (cOpcion == Opciones.Editar)
                    {
                        oEntidad.Accion = "EDITAR";
                    }

                    oEntidades.Add(oEntidad);


                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }

            return oEntidades;
        }

        private async Task<List<VTM_DOC_VENTA_CUOTAS_Entity>> GeneraVentaCuotas(string cNummovVenta)
        {

            List<VTM_DOC_VENTA_CUOTAS_Entity> oEntidades = new List<VTM_DOC_VENTA_CUOTAS_Entity>();
            try
            {
                foreach (System.Data.DataRow oRow in dtCuotas.Rows)
                {

                    VTM_DOC_VENTA_CUOTAS_Entity oEntidad = new VTM_DOC_VENTA_CUOTAS_Entity();

                    oEntidad.Accion = "INSERTAR";
                    oEntidad.Emp_cCodigo = cEmpresa;
                    oEntidad.Pan_cAnioVT = cAnio;
                    oEntidad.Dvm_cNumMov = cNummovVenta;
                    oEntidad.Ccu_nCorrel = Convert.ToInt16(oRow["Ccu_nCorrel"]);
                    oEntidad.Ccu_cMon = General.PrimerValor(txtTipoMoneda.Text);
                    oEntidad.Ccu_nMontoCuota = Convert.ToDecimal(oRow["Ccu_nMontoCuota"]);
                    oEntidad.Ccu_dFechaVenc = General.FE(oRow["Ccu_dFechaVenc"]);
                    //oEntidad.Ccu_dFechaVenc = General.ISO_To_DateTime(General.FechaISO(Convert.ToString(oRow["Ccu_dFechaVenc"])));

                    if (cOpcion == Opciones.Editar)
                    {
                        oEntidad.Accion = "EDITAR";
                    }

                    oEntidades.Add(oEntidad);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }

            return oEntidades;
        }

        private async void CalculaItems()
        {
            try
            {
                if (dtDetalle != null)
                {
                    int nFila = 1;
                    foreach (System.Data.DataRow oRow in dtDetalle.Rows)
                    {
                        oRow["Dvd_nItem"] = nFila;
                        nFila = nFila + 1;
                    }

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Validación - Comprobante", ex.Message, "OK");
            }
        }

        private async void CalculaItemsCuotas()
        {
            try
            {
                if (dtCuotas != null)
                {
                    int nFila = 1;
                    foreach (System.Data.DataRow oRow in dtCuotas.Rows)
                    {
                        oRow["Ccu_nCorrel"] = nFila;
                        nFila = nFila + 1;
                    }

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Validación - Cuotas", ex.Message, "OK");
            }
        }

        private void CalculaDetalle()
        {
            try
            {
                if (dtDetalle != null)
                {
                    double nCalculo = 0;
                    double nImporte = 0;
                    double nPrecioUnitario = 0;

                    if (cOpcion != Opciones.Consulta)
                    {
                        foreach (System.Data.DataRow oRow in dtDetalle.Rows)
                        {
                            if (General.PrimerValor(txtTipoMoneda.Text) == "038")
                                nPrecioUnitario = General.NE(oRow["Dvd_nPrecioUnitario_MN"]);
                            else
                                nPrecioUnitario = General.NE(oRow["Dvd_nPrecioUnitario"]);


                            nImporte = General.NE(oRow["Dvd_nCantidad"]) * nPrecioUnitario;
                            oRow["Dvd_nImporte"] = nImporte;

                            dtDetalle.AcceptChanges();
                            oRow.SetModified();

                            grdDetalle.ItemsSource = dtDetalle;
                        }

                    }


                    foreach (System.Data.DataRow oRow in dtDetalle.Rows)
                    {
                        if (General.PrimerValor(txtTipoMoneda.Text) == "038")
                            nImporte = General.NE(oRow["Dvd_nImporte_MN"]);
                        else
                            nImporte = General.NE(oRow["Dvd_nImporte"]);

                        nCalculo = nCalculo + nImporte;

                    }


                    if (chkIGV.IsChecked == true)
                    {
                        txtTotal.Value = nCalculo;
                        txtBase.Value = Math.Round(nCalculo / (1 + nIGV), 2);
                        txtIGV.Value = General.NE(txtTotal.Value) - General.NE(txtBase.Value);
                    }
                    else
                    {
                        txtBase.Value = nCalculo;
                        txtIGV.Value = Math.Round(nCalculo * nIGV, 2);
                        txtTotal.Value = General.NE(txtBase.Value) + General.NE(txtIGV.Value);
                    }

                    if (General.PrimerValor(txtTipoMoneda.Text) == "038")
                    {
                        txtBase.FormatString = "S/ #,##0.00";
                        txtIGV.FormatString = "S/ #,##0.00";
                        txtTotal.FormatString = "S/ #,##0.00";
                    }
                    else
                    {
                        txtBase.FormatString = "$ #,##0.00";
                        txtIGV.FormatString = "$ #,##0.00";
                        txtTotal.FormatString = "$ #,##0.00";
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        private async void CalculaCuota()
        {
            try
            {
                if (dtCuotas != null)
                {
                    double nCalculo = 0;
                    double nCuotas;

                    foreach (System.Data.DataRow oRow in dtCuotas.Rows)
                    {
                        nCuotas = General.NE(oRow["Ccu_nMontoCuota"]);
                        nCalculo = nCalculo + nCuotas;
                    }

                    txtCuotas.Value = General.NE(nCalculo);
                    txtSaldo.Value = General.NE(txtTotal.Value) - General.NE(nCalculo);

                    if (General.PrimerValor(txtTipoMoneda.Text) == "038")
                    {
                        txtCuotas.FormatString = "S/ #,##0.00";
                        txtSaldo.FormatString = "S/ #,##0.00";
                    }
                    else
                    {
                        txtCuotas.FormatString = "$ #,##0.00";
                        txtSaldo.FormatString = "S/ #,##0.00";
                    }

                    dtCuotas.AcceptChanges();
                    grdCuotas.ItemsSource = dtCuotas;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Validación - Cuotas", ex.Message, "OK");
            }
        }

        private void grdDetalle_SelectionChanged(object sender, Syncfusion.SfDataGrid.XForms.GridSelectionChangedEventArgs e)
        {
            try
            {
                Application.Current.Properties["Dvd_nItem"] = grdDetalle.GetCellValue(e.AddedItems[0], "Dvd_nItem");
            }
            catch (Exception)
            {
            }
        }

        private void grdCuotas_SelectionChanged(object sender, Syncfusion.SfDataGrid.XForms.GridSelectionChangedEventArgs e)
        {
            try
            {
                Application.Current.Properties["Ccu_nCorrel"] = grdCuotas.GetCellValue(e.AddedItems[0], "Ccu_nCorrel");
            }
            catch (Exception)
            {
            }
        }

        private void btnEliminarFilaDet_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (dtDetalle != null)
                {
                    var indiceEliminar = grdDetalle.SelectedIndex - 1;


                    dtDetalle.Rows.RemoveAt(indiceEliminar);
                    dtDetalle.AcceptChanges();


                    CalculaDetalle();
                    CalculaItems();

                    grdDetalle.Refresh();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnEliminarFilaCuo_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (dtCuotas != null)
                {
                    var indiceEliminar = grdCuotas.SelectedIndex - 1;


                    dtCuotas.Rows.RemoveAt(indiceEliminar);
                    dtCuotas.AcceptChanges();


                    CalculaCuota();
                    CalculaItemsCuotas();

                    grdCuotas.Refresh();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private async void btnAgregarFilaDet_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(VentasDetalleProductoPage)}");

            }
            catch (Exception ex)
            {
            }
        }

        private async void LlenaFilaCuotaVenta()
        {
            try
            {
                if (dtCuotas != null)
                {
                    int nFila = Convert.ToInt32(General.NE(dtCuotas.Rows.Count) + 1);
                    decimal nCuota = Convert.ToDecimal(Application.Current.Properties["Ccu_nMontoCuota"]);
                    string cFecha = Application.Current.Properties["Ccu_dFechaVenc"] as string;


                    VTM_DOC_VENTA_CUOTAS_Entity oEntidad = new VTM_DOC_VENTA_CUOTAS_Entity();

                    oEntidad.Accion = "";
                    oEntidad.Emp_cCodigo = cEmpresa;
                    oEntidad.Ccu_nCorrel = nFila;
                    oEntidad.Pan_cAnioVT = cAnio;

                    oEntidad.Dvm_cNumMov = "";
                    oEntidad.Ccu_dFechaVenc = General.ISO_To_DateTime(General.FechaISO(cFecha));
                    oEntidad.Ccu_cMon = General.PrimerValor(txtTipoMoneda.Text);
                    oEntidad.Ccu_nMontoCuota = nCuota;

                    oEntidad.Ccu_nCorrel = nFila;

                    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(VTM_DOC_VENTA_CUOTAS_Entity));

                    System.Data.DataRow row = dtCuotas.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                    {
                        row[prop.Name] = prop.GetValue(oEntidad) ?? DBNull.Value;
                    }

                    dtCuotas.Rows.Add(row);


                    CalculaCuota();
                    CalculaItemsCuotas();


                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Validación - Comprobante", ex.Message, "OK");
            }
        }

        private async void LlenaFilaDetalleVenta()
        {
            try
            {
                if (dtDetalle != null)
                {
                    int nFila = Convert.ToInt32(General.NE(dtDetalle.Rows.Count) + 1);
                    decimal nTC = Convert.ToDecimal(txtTipoCambio.Text);
                    string cCatalogo = Application.Current.Properties["Cab_cCatalogo"] as string;
                    decimal nCantidad = Convert.ToDecimal(Application.Current.Properties["Dvd_nCantidad"]);
                    decimal nPU = Convert.ToDecimal(Application.Current.Properties["PU"]);
                    string cCatalogoDescripcion = Application.Current.Properties["Cab_cDescripLarga"] as string;
                    string cUnidad = Application.Current.Properties["Cab_cUnidad"] as string;
                    string cUnidadDescripcion = Application.Current.Properties["Unidad"] as string;

                    VTD_DOC_VENTA_Entity oEntidad = new VTD_DOC_VENTA_Entity();

                    oEntidad.Accion = "";
                    oEntidad.Emp_cCodigo = cEmpresa;
                    oEntidad.Pan_cAnio = cAnio;

                    oEntidad.Producto = cCatalogoDescripcion;


                    oEntidad.Dvd_nItem = nFila;

                    if (General.PrimerValor(txtModalidad.Text) == "C")
                        oEntidad.Dvd_cFlgCatalog = "S";
                    else
                        oEntidad.Dvd_cFlgCatalog = "N";

                    oEntidad.Cab_cCatalogo = cCatalogo;
                    oEntidad.Dvd_cUnidad = cUnidad;
                    oEntidad.Dvd_nCantidad = nCantidad;
                    oEntidad.Dvd_nTasaImpto1 = Convert.ToDecimal(nIGV * 100);
                    oEntidad.Dvd_nTasaImpto2 = 0;
                    oEntidad.Dvd_nTasaImpto3 = 0;
                    oEntidad.Dvd_nTasaImpto4 = 0;
                    oEntidad.Dvd_nTasaImpto5 = 0;
                    oEntidad.Dvd_cFlgPrecioFijo = "N";

                    oEntidad.Dvd_nDsctoGral = 0;
                    oEntidad.Dvd_nDsctoLinea = 0;

                    if (General.PrimerValor(txtTipoMoneda.Text) == "038")
                    {
                        oEntidad.Dvd_nPrecioUnitario_MN = nPU;
                        oEntidad.Dvd_nPrecioNeto_MN = Math.Round(nPU * nCantidad, 2);
                        oEntidad.Dvd_nImporte_MN = Math.Round(nPU * nCantidad, 2);

                        oEntidad.Dvd_nPrecioUnitario = Math.Round(nPU / nTC, 2);
                        oEntidad.Dvd_nPrecioNeto = Math.Round((nPU * nCantidad) / nTC, 2);
                        oEntidad.Dvd_nImporte = Math.Round((nPU * nCantidad) / nTC, 2);
                    }
                    else
                    {
                        oEntidad.Dvd_nPrecioUnitario = nPU;
                        oEntidad.Dvd_nPrecioNeto = Math.Round(nPU * nCantidad, 2);
                        oEntidad.Dvd_nImporte = Math.Round(nPU * nCantidad, 2);

                        oEntidad.Dvd_nPrecioUnitario_MN = Math.Round(nPU * nTC, 2);
                        oEntidad.Dvd_nPrecioNeto_MN = Math.Round((nPU * nCantidad) * nTC, 2);
                        oEntidad.Dvd_nImporte_MN = Math.Round((nPU * nCantidad) * nTC, 2);
                    }

                    oEntidad.Pro_cCodigo = "01";
                    oEntidad.FlgAfecto = "";
                    oEntidad.Dvd_cCentroCosto = "";
                    oEntidad.Dvd_cFlgCCosto = "0";


                    if (chkIGV.IsChecked == true)
                        oEntidad.Dvd_FlagIncImp = "S";
                    else
                        oEntidad.Dvd_FlagIncImp = "N";

                    if (General.PrimerValor(txtModalidad.Text) == "C")
                        oEntidad.Dvd_cFlgCatalog = "B";
                    else
                        oEntidad.Dvd_cFlgCatalog = "S";

                    oEntidad.Pro_cCodigo = cCatalogoDescripcion;
                    oEntidad.Unidad = cUnidadDescripcion;

                    oEntidad.Accion = "INSERTAR";

                    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(VTD_DOC_VENTA_Entity));

                    System.Data.DataRow row = dtDetalle.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                    {
                        row[prop.Name] = prop.GetValue(oEntidad) ?? DBNull.Value;
                    }

                    dtDetalle.Rows.Add(row);



                    CalculaDetalle();
                    CalculaItems();
                    CalculaCuota();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Validación - Comprobante", ex.Message, "OK");
            }
        }

        private async void btnAgregarFilaCuo_Clicked(object sender, EventArgs e)
        {
            try
            {

                string cFecha = "19000101";

                foreach (System.Data.DataRow oRow in dtCuotas.Rows)
                {
                    cFecha = General.CE(oRow["Ccu_dFechaVenc"]);

                }

                Application.Current.Properties["TOTAL"] = General.CE(txtTotal.Value);
                Application.Current.Properties["TOTALCUOTAS"] = General.CE(txtCuotas.Value);
                Application.Current.Properties["FECHACUOTA"] = cFecha;

                await Shell.Current.GoToAsync($"{nameof(VentaDetalleCuotaPage)}");

            }
            catch (Exception ex)
            {
            }
        }

        private void grdCuotas_QueryRowHeight(object sender, Syncfusion.SfDataGrid.XForms.QueryRowHeightEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                e.Height = SfDataGridHelpers.GetRowHeight(grdCuotas, e.RowIndex);
                e.Handled = true;
            }
        }

        private void grdDetalle_QueryRowHeight(object sender, Syncfusion.SfDataGrid.XForms.QueryRowHeightEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                e.Height = SfDataGridHelpers.GetRowHeight(grdDetalle, e.RowIndex);
                e.Handled = true;
            }
        }

        private void tdbcMedio_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            txtMedio.Text = e.NewValue.ToString();
            Application.Current.Properties["Dvm_cCondicion"] = General.PrimerValor(e.NewValue.ToString());

        }

        private void btnMedio_Clicked(object sender, EventArgs e)
        {
            RedimencionarCombos();
            tdbcMedio.ItemsSource = LlenaMedioPago();
            tdbcMedio.IsOpen = true;

        }

        private void txtNumeroDocId_Unfocused(object sender, FocusEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtNumeroDocId.Text) && txtNumeroDocId.Text=="0" && General.PrimerValor( txtTipoDoc.Text )!= "01")
            {
                ClientePorDefectoBoleta();
            }
        }

        private void txtNumeroDocId_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtCodigo.Text = "";
            txtRazon.Text = "";
            txtDireccion.Text = "";
            txtUbigeo.Text = "";
            txtTipoDocumento.Text = "";
        }
    }
}
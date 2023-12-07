using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Ventas.Extensions;
using Ventas.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace Ventas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TipoCambioDetallePage : ContentPage
    {
        string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;
        string cOpcion = Application.Current.Properties["Opcion"] as string;
        string cFecha = Application.Current.Properties["Tca_dFecha"] as string;
        string cUsuario = Application.Current.Properties["Usu_cCodUsuario"] as string;

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }
        public TipoCambioDetallePage()
        {
            InitializeComponent();


            if (cOpcion == Opciones.Nuevo)
            {
                LimpiaDatos();
                BloquearDatos(false);
                btnFecha.IsVisible = true;
                
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
            Propiedades.SoloLecturaEntry(txtFecha, true);
            Propiedades.SoloLecturaEntry(txtCompra, bEstado);
            Propiedades.SoloLecturaEntry(txtVenta, bEstado);

            btnFecha.IsVisible = false;
            btnBuscar.IsVisible = !bEstado;
            btnGrabar.IsVisible = !bEstado;
        }

        private void LimpiaDatos()
        {
            txtFecha.Text = "";
            txtCompra.Text = "";
            txtVenta.Text = "";

        }

        private void LlenaDatos()
        {
            try
            {
                CNT_TIPO_CAMBIO_Entity oEntidad = new CNT_TIPO_CAMBIO_Entity();

                oEntidad.Accion = "SEL_TC";
                oEntidad.Emp_cCodigo = cEmpresa;
                oEntidad.Tca_dFecha = General.FechaISO(cFecha);
                oEntidad.Tca_cCodigoOrigen = "038";
                oEntidad.Tca_cCodigoDestino = "040";



                string result = ProcedimientosAPI.GetPostBuscarTipoCambio(oEntidad);

                result = "[" + result + "]";

                List<CNT_TIPO_CAMBIO_Entity> response = JsonConvert.DeserializeObject<List<CNT_TIPO_CAMBIO_Entity>>(result);

                foreach (var message in response)
                {

                    txtFecha.Text = General.ISOFecha( message.Tca_dFecha);
                    txtCompra.Text = General.NE(message.Tca_nCompra).ToString();
                    txtVenta.Text = General.NE(message.Tca_nVenta).ToString();
                }
            }
            catch (Exception)
            {
            }

        }



        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async Task<bool> Valida()
        {
            bool bValidar = true;

            if (string.IsNullOrEmpty(txtFecha.Text as string))
            {
                await DisplayAlert("Validación - Tipo de Cambio", "Se debe seleccionar o ingresar una fecha", "OK");
                return false;
            }


            if (string.IsNullOrEmpty(txtCompra.Text as string))
            {
                await DisplayAlert("Validación - Tipo de Cambio", "Se debe ingresar el TC de Compra", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(txtVenta.Text as string))
            {
                await DisplayAlert("Validación - Tipo de Cambio", "Se debe ingresar el TC de Venta", "OK");
                return false;
            }

            return bValidar;
        }

        private async void btnBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                string result = "";

                DIA_Entity oEntidad = new DIA_Entity();

                
                oEntidad.Fecha =General.ISO_To_DateTime( General.FechaISO(txtFecha.Text));

                result = ProcedimientosAPI.GetPostBuscarTC(oEntidad);

                result = "[" + result + "]";

                List<DIA_Entity> response = JsonConvert.DeserializeObject<List<DIA_Entity>>(result);

                foreach (var message in response)
                {

                    txtCompra.Text = General.NE(message.Compra).ToString();
                    txtVenta.Text = General.NE(message.Venta).ToString();


                }

            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void btnGrabar_Clicked(object sender, EventArgs e)
        {
            if (await Valida() == true)
            {
                try
                {
                    CNT_TIPO_CAMBIO_Entity oEntidad = new CNT_TIPO_CAMBIO_Entity();

                    oEntidad.Accion = "INSERTAR";
                    oEntidad.Emp_cCodigo = cEmpresa;
                    oEntidad.Tca_dFecha = General.FechaISO(txtFecha.Text);
                    //dtpFecha.ToString("yyyy-MM-dd");
                    oEntidad.Tca_cCodigoOrigen = "038";
                    oEntidad.Tca_cCodigoDestino = "040";
                    oEntidad.Tca_nCompra = Convert.ToDecimal(txtCompra.Text);
                    oEntidad.Tca_nVenta = Convert.ToDecimal(txtVenta.Text);
                    oEntidad.Tca_nCompraP = Convert.ToDecimal(txtCompra.Text);
                    oEntidad.Tca_nVentaP = Convert.ToDecimal(txtVenta.Text);
                    oEntidad.Tca_cUserCrea = cUsuario;
                    oEntidad.Tca_cPeriodo = General.Right(General.Left(txtFecha.Text, 5), 2);


                    string resultPost;

                    if (cOpcion == Opciones.Editar)
                    {
                        oEntidad.Accion = "EDITAR";
                        oEntidad.Tca_dFecha = General.FechaISO(txtFecha.Text);

                        resultPost = ProcedimientosAPI.GetPostEditarTipoCambio(oEntidad);
                    }
                    else
                    {
                        resultPost = ProcedimientosAPI.GetPostInsertarTipoCambio(oEntidad);
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


        private void dtpFechas_OkButtonClicked(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        {
            DateTime date =General.FE(  e.NewValue.ToString());

            txtFecha.Text = date.ToString("dd/MM/yyyy");
        }

        private void btnFecha_Clicked(object sender, EventArgs e)
        {
            dtpFechas.IsOpen = true;
        }
    }
}
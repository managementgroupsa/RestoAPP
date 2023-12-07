using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ventas.Extensions;
using Ventas.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Ventas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientesDetallePage : ContentPage
    {
        string cCodigoEntidad = Application.Current.Properties["Ent_cCodEntidad"] as string;
        string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;
        string cOpcion = Application.Current.Properties["Opcion"] as string;
        string cUsuario = Application.Current.Properties["Usu_cCodUsuario"] as string;

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

        public ClientesDetallePage()
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
            Propiedades.SoloLecturaEntry(txtCodigo, bEstado);
            Propiedades.SoloLecturaEntry(txtNumero, bEstado);
            Propiedades.SoloLecturaEntry(txtNombre, bEstado);
            Propiedades.SoloLecturaEntry(txtDireccion, bEstado);
            Propiedades.SoloLecturaEntry(txtUbigeo, bEstado);
            Propiedades.SoloLecturaEntry(txtTelefono, bEstado);
            Propiedades.SoloLecturaEntry(txtCorreo, bEstado);
            Propiedades.SoloLecturaEntry(txtEstado, bEstado);
            Propiedades.SoloLecturaEntry(txtCondicion, bEstado);

            Propiedades.SoloLecturaEditor(txtOtros, bEstado);

            btnBuscar.IsVisible = !bEstado;
            btnGrabar.IsVisible = !bEstado;
        }

        private void LimpiaDatos()
        {
            txtCodigo.Text = "";
            txtNumero.Text = "";
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtUbigeo.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            txtOtros.Text = "";
        }

        private void LlenaDatos()
        {

            CNM_ENTIDAD_Entity oEntidad = new CNM_ENTIDAD_Entity();

            oEntidad.Accion = "SEL_REG";
            oEntidad.Emp_cCodigo = cEmpresa;
            oEntidad.Ten_cTipoEntidad = "C";
            oEntidad.Ent_cCodEntidad = cCodigoEntidad;


            string result = ProcedimientosAPI.GetPostBuscarEntidades(oEntidad);

            List<CNM_ENTIDAD_Entity> response = JsonConvert.DeserializeObject<List<CNM_ENTIDAD_Entity>>(result);

            foreach (var message in response)
            {
                txtCodigo.Text = cCodigoEntidad;
                txtNumero.Text = message.Ent_nRuc;
                txtNombre.Text = message.Ent_cPersona;
                txtDireccion.Text = message.Ent_cDireccion;
                txtUbigeo.Text = message.Dir_cUbigeo;
                txtTelefono.Text = message.Ent_nTelefono;
                txtCorreo.Text = message.En_nMail;
                txtOtros.Text = message.Ent_Observacion;
                txtEstado.Text = message.Ent_cEstado;

                if (message.Ent_cTipoDoc == "04")
                    txtTipoDocumento.Text = "6";// RUC
                else
                    txtTipoDocumento.Text = "1";// DNI


                if (message.Ent_cEstadoEntidad == "S")
                {
                    txtCondicion.Text = "HABIDO";
                }

                if (message.Ent_cEstadoEntidad == "N")
                {
                    txtCondicion.Text = "NO HABIDO";
                }

                if (message.Ent_cEstadoEntidad == "X")
                {
                    txtCondicion.Text = "NO HALLADO";
                }

            }
        }

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void btnBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                string result = "";
                string cNombreEncotrado = "";

                CNM_ENTIDAD_Entity oEntidadCli = new CNM_ENTIDAD_Entity();

                oEntidadCli.Accion = "SEL_DOCID";
                oEntidadCli.Emp_cCodigo = cEmpresa;
                oEntidadCli.Ten_cTipoEntidad = "C";
                oEntidadCli.Ent_nRuc = txtNumero.Text;

                result = ProcedimientosAPI.GetPostConsultaDocumento(oEntidadCli);

                result = "[" + result + "]";

                List<CNM_ENTIDAD_Entity> responseCli = JsonConvert.DeserializeObject<List<CNM_ENTIDAD_Entity>>(result);

                foreach (var message in responseCli)
                {
                    cNombreEncotrado = message.Ent_cPersona;
                }

                if (!string.IsNullOrEmpty(cNombreEncotrado) && cOpcion == Opciones.Nuevo)
                {
                    await DisplayAlert("Validación", "El documento ingresado ya se encuentra en el sistema", "OK");
                }
                else
                {
                    SUNAT_Entity oEntidad = new SUNAT_Entity();

                    oEntidad.ruc = txtNumero.Text;


                    if (txtNumero.Text.Length == 8)
                        result = ProcedimientosAPI.GetPostBuscarDNI(oEntidad);
                    else
                        result = ProcedimientosAPI.GetPostBuscarRUC(oEntidad);

                    result = "[" + result + "]";

                    List<SUNAT_Entity> response = JsonConvert.DeserializeObject<List<SUNAT_Entity>>(result);

                    foreach (var message in response)
                    {
                        txtCodigo.Text = "";
                        txtNumero.Text = message.ruc;
                        txtNombre.Text = message.nombres;
                        txtDireccion.Text = message.direccion + " " + message.departamento + " - " + message.provincia + " - " + message.distrito;
                        txtUbigeo.Text = message.ubigeo;
                        txtTelefono.Text = message.telefono;
                        txtCorreo.Text = "";
                        txtOtros.Text = "";
                        txtTipoDocumento.Text = message.tipoDocumento;
                        txtEstado.Text = message.estado;
                        txtCondicion.Text = message.condiContribuyente;
                    }
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");

            }
        }

        private async Task<bool> Valida()
        {
            bool bValidar = true;

            if (string.IsNullOrEmpty(txtNumero.Text))
            {
                await DisplayAlert("Validación - Catalogo", "Se debe ingresar el numero de documento", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                await DisplayAlert("Validación - Catalogo", "Se debe ingresar el nombre o razon social", "OK");
                return false;
            }


            if ((txtNumero.Text.Length == 11) && (General.Left(txtNumero.Text, 2) == "20" || General.Left(txtNumero.Text, 2) == "10" || General.Left(txtNumero.Text, 2) == "15"))
            {

                if (string.IsNullOrEmpty(txtDireccion.Text))
                {
                    await DisplayAlert("Validación - Catalogo", "Se debe ingresar la direccion", "OK");
                    return false;
                }

                if (string.IsNullOrEmpty(txtUbigeo.Text))
                {
                    await DisplayAlert("Validación - Catalogo", "Se debe ingresar el ubigeo", "OK");
                    return false;
                }

            }



            return bValidar;
        }

        private async void btnGrabar_Clicked(object sender, EventArgs e)
        {
            if (await Valida() == true)
            {
                try
                {
                    CNM_ENTIDAD_Entity oEntidad = new CNM_ENTIDAD_Entity();

                    oEntidad.Accion = "INSERTAR";
                    oEntidad.Emp_cCodigo = cEmpresa;
                    oEntidad.Ten_cTipoEntidad = "C";
                    oEntidad.Ent_cCodEntidad = "";
                    oEntidad.Ent_nRuc = txtNumero.Text;
                    oEntidad.Ent_cPersona = txtNombre.Text;
                    oEntidad.Ent_cDireccion = txtDireccion.Text;
                    oEntidad.Dir_cUbigeo = txtUbigeo.Text;
                    oEntidad.Ent_nTelefono = txtTelefono.Text;
                    oEntidad.En_nMail = txtCorreo.Text;
                    oEntidad.Ent_Observacion = txtOtros.Text;
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



                    oEntidad.GrabaDireccion = "S";
                    oEntidad.Ent_cUserCrea = cUsuario;

                    string resultPost;

                    if (cOpcion == Opciones.Editar)
                    {
                        oEntidad.Accion = "EDITAR";
                        oEntidad.Ent_cCodEntidad = cCodigoEntidad;

                        resultPost = ProcedimientosAPI.GetPostEditarEntidad(oEntidad);
                    }
                    else
                    {
                        resultPost = ProcedimientosAPI.GetPostInsertarEntidad(oEntidad);
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
    }
}

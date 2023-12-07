using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Extensions;
using Ventas.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ventas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TipoPrecioDetallePage : ContentPage
    {
        string cCodigo = Application.Current.Properties["Tpr_cCodigo"] as string;
        string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;
        string cOpcion = Application.Current.Properties["Opcion"] as string;
        string cUsuario = Application.Current.Properties["Usu_cCodUsuario"] as string;

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

        public TipoPrecioDetallePage()
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
            Propiedades.SoloLecturaEntry(txtDescripcion, bEstado);


            btnGrabar.IsVisible = !bEstado;
        }

        private void LimpiaDatos()
        {
            txtCodigo.Text = "";
            txtDescripcion.Text = "";
        }

        private void LlenaDatos()
        {
            try
            {
                VTT_TIPO_PRECIO_Entity oEntidad = new VTT_TIPO_PRECIO_Entity();

                oEntidad.Accion = "BUSCARREGISTRO";
                oEntidad.Emp_cCodigo = cEmpresa;
                oEntidad.Tpr_cCodigo = cCodigo;
                

                string result = ProcedimientosAPI.GetPostBuscarTipoPrecio(oEntidad);

                result = "[" + result + "]";

                List<VTT_TIPO_PRECIO_Entity> response = JsonConvert.DeserializeObject<List<VTT_TIPO_PRECIO_Entity>>(result);

                foreach (var message in response)
                {
                    txtCodigo.Text = cCodigo;
                    txtDescripcion.Text = message.Tpr_cDescripcion;

                }

            }
            catch (Exception ex)
            {
            }

        }

        private async Task<bool> Valida()
        {
            bool bValidar = true;

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                await DisplayAlert("Tipo de Precio", "Se debe ingresar el cod. de Categoria", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(txtDescripcion.Text))
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
                    VTT_TIPO_PRECIO_Entity oEntidad = new VTT_TIPO_PRECIO_Entity();

                    oEntidad.Accion = "INSERTAR";
                    oEntidad.Emp_cCodigo = cEmpresa;
                    oEntidad.Tpr_cCodigo = txtCodigo.Text;
                    oEntidad.Tpr_cDescripcion = txtDescripcion.Text;
                    oEntidad.Tpr_cEstado = "A";
                    oEntidad.Tpr_cUser = cUsuario;


                    string resultPost;

                    if (cOpcion == Opciones.Editar)
                    {
                        oEntidad.Accion = "EDITAR";
                        

                        resultPost = ProcedimientosAPI.GetPostEditarTipoPrecio(oEntidad);
                    }
                    else
                    {
                        resultPost = ProcedimientosAPI.GetPostInsertarTipoPrecio(oEntidad);
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
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
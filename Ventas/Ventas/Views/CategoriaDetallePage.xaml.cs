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
    public partial class CategoriadetallePage : ContentPage
    {
        string cCodigo = Application.Current.Properties["Aca_cCategoria"] as string;
        string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;
        string cOpcion = Application.Current.Properties["Opcion"] as string;
        string cUsuario = Application.Current.Properties["Usu_cCodUsuario"] as string;

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

        public CategoriadetallePage()
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
                LGT_CATEGORIA_Entity oEntidad = new LGT_CATEGORIA_Entity();

                oEntidad.Accion = "BUSCARREGISTRO";
                oEntidad.Emp_cCodigo = cEmpresa;
                oEntidad.Aca_cCategoria = cCodigo;

                string result = ProcedimientosAPI.GetPostBuscarCategoria(oEntidad);

                result = "[" + result + "]";

                List<LGT_CATEGORIA_Entity> response = JsonConvert.DeserializeObject<List<LGT_CATEGORIA_Entity>>(result);

                foreach (var message in response)
                {
                    txtCodigo.Text = cCodigo;
                    txtDescripcion.Text = message.Aca_cDescripLarga;

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

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                await DisplayAlert("Categoria", "Se debe ingresar el cod. de Categoria", "OK");
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
                    LGT_CATEGORIA_Entity oEntidad = new LGT_CATEGORIA_Entity();

                    oEntidad.Accion = "INSERTAR";
                    oEntidad.Emp_cCodigo = cEmpresa;
                    oEntidad.Aca_cCategoria = txtCodigo.Text;
                    oEntidad.Aca_cDescripLarga = txtDescripcion.Text;
                    oEntidad.Aca_cDescripCorta = txtDescripcion.Text;
                    oEntidad.Aca_cEstado = "A";
                    oEntidad.Aca_cUser = cUsuario;


                    string resultPost;

                    if (cOpcion == Opciones.Editar)
                    {
                        oEntidad.Accion = "EDITAR";
                        oEntidad.Aca_cCategoria = cCodigo;

                        resultPost = ProcedimientosAPI.GetPostEditarCategoria(oEntidad);
                    }
                    else
                    {
                        resultPost = ProcedimientosAPI.GetPostInsertarCategoria(oEntidad);
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
    }
}
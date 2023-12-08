using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoAPP.Extensions;
using RestoAPP.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestoAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GrupodetallePage : ContentPage
    {
        string cCodigo = Application.Current.Properties["Gru_cGrupo"] as string;
        string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;
        string cOpcion = Application.Current.Properties["Opcion"] as string;
        string cUsuario = Application.Current.Properties["Usu_cCodUsuario"] as string;

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

        public GrupodetallePage()
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

        private void RedimencionarCombos()
        {
            Application.Current.Properties["AltoVentana"] = (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density) - 80;
            tdbcCategoria.PickerHeight = General.NE(Application.Current.Properties["AltoVentana"]);
        }

        private void BloquearDatos(bool bEstado)
        {

            Propiedades.SoloLecturaEntry(txtCodigo, bEstado);
            Propiedades.SoloLecturaEntry(txtDescripcion, bEstado);
            Propiedades.SoloLecturaEntry(txtCategoria, true);


            btnGrabar.IsVisible = !bEstado;
            btCategoria.IsVisible = !bEstado;
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
                LGT_GRUPO_BS_Entity oEntidad = new LGT_GRUPO_BS_Entity();

                oEntidad.Accion = "BUSCAR_REGAPP";
                oEntidad.Emp_cCodigo = cEmpresa;
                oEntidad.Gru_cGrupo = cCodigo;

                string result = ProcedimientosAPI.GetPostBuscarGrupo(oEntidad);

                result = "[" + result + "]";

                List<LGT_GRUPO_BS_Entity> response = JsonConvert.DeserializeObject<List<LGT_GRUPO_BS_Entity>>(result);

                foreach (var message in response)
                {

                    txtCodigo.Text = message.Gru_cGrupo;
                    txtDescripcion.Text = message.Gru_cDescripLarga;
                    txtCategoria.Text = message.Aca_cCategoria;


                }

            }
            catch (Exception ex)
            {
            }

        }

        private async Task<bool> Valida()
        {
            bool bValidar = true;

            if (string.IsNullOrEmpty(txtCategoria.Text))
            {
                await DisplayAlert("Grupo", "Se debe ingresar el cod. de Categoria", "OK");
                return false;
            }


            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                await DisplayAlert("Grupo", "Se debe ingresar el cod. de Grupo", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                await DisplayAlert("Grupo", "Se debe ingresar la descripción ", "OK");
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
                    LGT_GRUPO_BS_Entity oEntidad = new LGT_GRUPO_BS_Entity();

                    oEntidad.Accion = "INSERTAR";
                    oEntidad.Emp_cCodigo = cEmpresa;
                    oEntidad.Aca_cCategoria = General.PrimerValor(txtCategoria.Text);
                    oEntidad.Gru_cGrupo = txtCodigo.Text;
                    oEntidad.Gru_cDescripLarga = txtDescripcion.Text;
                    oEntidad.Gru_cDescripCorta = txtDescripcion.Text;
                    oEntidad.Gru_cEstado = "A";
                    oEntidad.Gru_cUser = cUsuario;


                    string resultPost ;

                    if (cOpcion == Opciones.Editar)
                    {
                        oEntidad.Accion = "EDITAR";
                        oEntidad.Gru_cGrupo = cCodigo;

                        resultPost = ProcedimientosAPI.GetPostEditarGrupo(oEntidad);
                    }
                    else
                    {
                        resultPost = ProcedimientosAPI.GetPostInsertarGrupo(oEntidad);
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

        private List<string> LlenaCategorias()
        {
            List<string> oLista = new List<string>();

            LGT_CATEGORIA_Entity oEntidad = new LGT_CATEGORIA_Entity();

            oEntidad.Accion = "BUSCARTODOS";
            oEntidad.Emp_cCodigo = cEmpresa;


            string result = ProcedimientosAPI.GetPostBuscarCategorias(oEntidad);

            List<LGT_CATEGORIA_Entity> response = JsonConvert.DeserializeObject<List<LGT_CATEGORIA_Entity>>(result);

            foreach (var message in response)
            {
                oLista.Add(message.Aca_cCategoria + " - " + message.Aca_cDescripLarga);
            }

            return oLista;
        }

        private void tdbcCategoria_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            txtCategoria.Text = e.NewValue.ToString();
            Application.Current.Properties["Aca_cCategoria"] = General.PrimerValor(e.NewValue.ToString());

        }

        private void btCategoria_Clicked(object sender, EventArgs e)
        {
            RedimencionarCombos();
            tdbcCategoria.ItemsSource = LlenaCategorias();
            tdbcCategoria.IsOpen = true;
        }
    }
}
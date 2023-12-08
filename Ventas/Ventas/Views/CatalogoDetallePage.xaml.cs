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
    public partial class CatalogoDetallePage : ContentPage
    {
        string cCodigo = Application.Current.Properties["Cab_cCatalogo"] as string;
        string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;
        string cOpcion = Application.Current.Properties["Opcion"] as string;
        string cUsuario = Application.Current.Properties["Usu_cCodUsuario"] as string;

        public CatalogoDetallePage()
        {
            InitializeComponent();

            if (cOpcion == Opciones.Nuevo)
            {
                LimpiaDatos();
                BloquearDatos(false);
                Propiedades.SoloLecturaEntry(txtCodigo, true);
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

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

        private void RedimencionarCombos()
        {
            Application.Current.Properties["AltoVentana"] = (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density) - 80;
            tdbcCategoria.PickerHeight = General.NE(Application.Current.Properties["AltoVentana"]);
            tdbcGrupo.PickerHeight = General.NE(Application.Current.Properties["AltoVentana"]);
            tdbcClase.PickerHeight = General.NE(Application.Current.Properties["AltoVentana"]);
            tdbcFamilia.PickerHeight = General.NE(Application.Current.Properties["AltoVentana"]);
            tdbcTipoBS.PickerHeight = General.NE(Application.Current.Properties["AltoVentana"]);
            tdbcUnidad.PickerHeight = General.NE(Application.Current.Properties["AltoVentana"]);
        }

        private void BloquearDatos(bool bEstado)
        {
            Propiedades.SoloLecturaEntry(txtTipoBS, true);
            Propiedades.SoloLecturaEntry(txtCategoria, true);
            Propiedades.SoloLecturaEntry(txtGrupo, true);
            Propiedades.SoloLecturaEntry(txtClase, true);
            Propiedades.SoloLecturaEntry(txtFamilia, true);
            Propiedades.SoloLecturaEntry(txtUnidad, true);

            Propiedades.SoloLecturaEntry(txtCodigo, bEstado);
            Propiedades.SoloLecturaEntry(txtDescripcion, bEstado);
            Propiedades.SoloLecturaEntry(txtPropio, bEstado);
            Propiedades.SoloLecturaEditor(txtEspecificaciones, bEstado);

            btnGrabar.IsVisible = !bEstado;
            btnTipoBS.IsVisible = !bEstado; 
            btnCategoria.IsVisible = !bEstado;
            btnGrupo.IsVisible = !bEstado;
            btnClase.IsVisible = !bEstado;  
            btnFamilia.IsVisible = !bEstado;
            btnUnidad.IsVisible = !bEstado;

        }

        private void LimpiaDatos()
        {
            txtCategoria.Text = "";
            txtGrupo.Text = "";
            txtClase.Text = "";
            txtFamilia.Text = "";
            txtUnidad.Text = "";
            txtCodigo.Text = "";
            txtDescripcion.Text = "";
            txtPropio.Text = "";
            txtEspecificaciones.Text = "";
        }

        private void LlenaDatos()
        {
            LGM_CATALOGO_BS_Entity oEntidad = new LGM_CATALOGO_BS_Entity();

            oEntidad.Accion = "BUSCAR_REGAPP";
            oEntidad.Emp_cCodigo = cEmpresa;
            oEntidad.Cab_cCatalogo = cCodigo;

            string result = ProcedimientosAPI.GetPostBuscarCatalogo(oEntidad);

            result = "[" + result + "]";

            List<LGM_CATALOGO_BS_Entity> response = JsonConvert.DeserializeObject<List<LGM_CATALOGO_BS_Entity>>(result);

            foreach (var message in response)
            {
                txtTipoBS.Text = message.Cab_cTipoBS;
                txtCategoria.Text = message.Aca_cCategoria;
                txtGrupo.Text = message.Gru_cGrupo;
                txtClase.Text = message.Cla_cClase;
                txtFamilia.Text = message.Fam_cFamilia;
                txtUnidad.Text = message.Cab_cUnidad;
                txtCodigo.Text = cCodigo;
                txtDescripcion.Text = message.Cab_cDescripLarga;
                txtPropio.Text = message.Cab_cCodPropio;
                txtEspecificaciones.Text = message.Cab_Especificacion;
            }
        }

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async Task<bool> Valida()
        {
            bool bValidar = true;

            if (string.IsNullOrEmpty(txtTipoBS.Text))
            {
                await DisplayAlert("Catalogo", "Se debe seleccionar el tipo", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(txtCategoria.Text))
            {
                await DisplayAlert("Catalogo", "Se debe seleccionar una categoria", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(txtGrupo.Text))
            {
                await DisplayAlert("Catalogo", "Se debe seleccionar una grupo", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(txtClase.Text))
            {
                await DisplayAlert("Catalogo", "Se debe seleccionar una clase", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(txtFamilia.Text))
            {
                await DisplayAlert("Catalogo", "Se debe seleccionar una familia", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(txtUnidad.Text))
            {
                await DisplayAlert("Catalogo", "Se debe seleccionar la unidad", "OK");
                return false;
            }


            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                await DisplayAlert("Catalogo", "Se debe ingresar la descripción del producto", "OK");
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
                    LGM_CATALOGO_BS_Entity oEntidad = new LGM_CATALOGO_BS_Entity();

                    oEntidad.Accion = "INSERTAR";
                    oEntidad.Emp_cCodigo = cEmpresa;
                    oEntidad.Cab_cCatalogo = "";
                    oEntidad.Aca_cCategoria = General.PrimerValor(txtCategoria.Text);
                    oEntidad.Gru_cGrupo = General.PrimerValor(txtGrupo.Text);
                    oEntidad.Cla_cClase = General.PrimerValor(txtClase.Text);
                    oEntidad.Fam_cFamilia = General.PrimerValor(txtFamilia.Text);

                    oEntidad.Cab_Especificacion = txtEspecificaciones.Text;
                    oEntidad.Cab_cCodPropio = txtPropio.Text;

                    oEntidad.Cab_cDescripLarga = txtDescripcion.Text;
                    oEntidad.Cab_cDescripCorta = txtDescripcion.Text;
                    oEntidad.Cab_cEstado = "A";

                    string cTipoBS = General.PrimerValor(txtTipoBS.Text);

                    oEntidad.Cab_cTipoBS = cTipoBS;

                    if (cTipoBS == "B")
                        oEntidad.Cab_cCtrlStock = "S";
                    else
                        oEntidad.Cab_cCtrlStock = "N";

                    oEntidad.Cab_cTipoConjunto = "N";
                    oEntidad.Cab_cTipoMonedaCom = "038";
                    oEntidad.Cab_cTipoMonedaVen = "038";

                    oEntidad.Cab_cUnidad = General.PrimerValor(txtUnidad.Text);

                    oEntidad.Cab_cUser = cUsuario;

                    string resultPost;

                    if (cOpcion == Opciones.Editar)
                    {
                        oEntidad.Accion = "EDITAR";
                        oEntidad.Cab_cCatalogo = cCodigo;

                        resultPost = ProcedimientosAPI.GetPostEditarCatalogo(oEntidad);
                    }
                    else
                    {
                        resultPost = ProcedimientosAPI.GetPostInsertarCatalogo(oEntidad);
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

        private List<string> LlenaTipoBS()
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

        private List<string> LlenaUnidades()
        {
            List<string> oLista = new List<string>();

            TABLA_Entity oEntidad = new TABLA_Entity();

            oEntidad.Accion = "BUSCAR_ALL";
            oEntidad.Emp_cCodigo = cEmpresa;
            oEntidad.Tab_cTabla = "053";

            string result = ProcedimientosAPI.GetPostBuscarTablas(oEntidad);

            List<TABLA_Entity> response = JsonConvert.DeserializeObject<List<TABLA_Entity>>(result);

            foreach (var message in response)
            {
                oLista.Add(message.Tab_cCodigo + " - " + message.Tab_cDescripCampo);
            }

            return oLista;
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

        private List<string> LlenaGrupos()
        {
            List<string> oLista = new List<string>();

            LGT_GRUPO_BS_Entity oEntidad = new LGT_GRUPO_BS_Entity();

            oEntidad.Accion = "BUSCARTODOS";
            oEntidad.Emp_cCodigo = cEmpresa;
            oEntidad.Aca_cCategoria = Application.Current.Properties["Aca_cCategoria"] as string;

            string result = ProcedimientosAPI.GetPostBuscarGrupos(oEntidad);

            List<LGT_GRUPO_BS_Entity> response = JsonConvert.DeserializeObject<List<LGT_GRUPO_BS_Entity>>(result);

            foreach (var message in response)
            {
                oLista.Add(message.Gru_cGrupo + " - " + message.Gru_cDescripLarga);
            }

            return oLista;
        }

        private List<string> LlenaClases()
        {
            List<string> oLista = new List<string>();

            LGT_CLASE_BS_Entity oEntidad = new LGT_CLASE_BS_Entity();

            oEntidad.Accion = "BUSCARTODOS";
            oEntidad.Emp_cCodigo = cEmpresa;
            oEntidad.Aca_cCategoria = Application.Current.Properties["Aca_cCategoria"] as string;
            oEntidad.Gru_cGrupo = Application.Current.Properties["Gru_cGrupo"] as string;

            string result = ProcedimientosAPI.GetPostBuscarClases(oEntidad);

            List<LGT_CLASE_BS_Entity> response = JsonConvert.DeserializeObject<List<LGT_CLASE_BS_Entity>>(result);

            foreach (var message in response)
            {
                oLista.Add(message.Cla_cClase + " - " + message.Cla_cDescripLarga);
            }

            return oLista;
        }

        private List<string> LlenaFamilia()
        {
            List<string> oLista = new List<string>();

            LGT_FAMILIA_Entity oEntidad = new LGT_FAMILIA_Entity();

            oEntidad.Accion = "BUSCARTODOS";
            oEntidad.Emp_cCodigo = cEmpresa;
            oEntidad.Aca_cCategoria = Application.Current.Properties["Aca_cCategoria"] as string;
            oEntidad.Gru_cGrupo = Application.Current.Properties["Gru_cGrupo"] as string;
            oEntidad.Cla_cClase = Application.Current.Properties["Cla_cClase"] as string;

            string result = ProcedimientosAPI.GetPostBuscarFamilias(oEntidad);

            List<LGT_FAMILIA_Entity> response = JsonConvert.DeserializeObject<List<LGT_FAMILIA_Entity>>(result);

            foreach (var message in response)
            {
                oLista.Add(message.Fam_cFamilia + " - " + message.Fam_cDescripLarga);
            }

            return oLista;
        }

        private void tdbcCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = tdbcCategoria.SelectedItem as string;
            Application.Current.Properties["Aca_cCategoria"] = General.PrimerValor(selectedItem);

            tdbcGrupo.ItemsSource = LlenaGrupos();
        }

        private void tdbcGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = tdbcGrupo.SelectedItem as string;
            Application.Current.Properties["Gru_cGrupo"] = General.PrimerValor(selectedItem);

            tdbcClase.ItemsSource = LlenaClases();
        }

        private void tdbcClase_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = tdbcClase.SelectedItem as string;
            Application.Current.Properties["Cla_cClase"] = General.PrimerValor(selectedItem);

            tdbcFamilia.ItemsSource = LlenaFamilia();
        }

        private void tdbcTipoBS_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            txtTipoBS.Text = e.NewValue.ToString();
            Application.Current.Properties["Cab_cTipoBS"] = General.PrimerValor(e.NewValue.ToString());
        }

        private void tdbcCategoria_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            txtCategoria.Text = e.NewValue.ToString();
            Application.Current.Properties["Aca_cCategoria"] = General.PrimerValor(e.NewValue.ToString());
        }

        private void tdbcGrupo_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            txtGrupo.Text = e.NewValue.ToString();
            Application.Current.Properties["Gru_cGrupo"] = General.PrimerValor(e.NewValue.ToString());
        }


        private void tdbcClase_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            txtClase.Text = e.NewValue.ToString();
            Application.Current.Properties["Cla_cClase"] = General.PrimerValor(e.NewValue.ToString());
        }

        private void tdbcFamilia_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            txtFamilia.Text = e.NewValue.ToString();
            Application.Current.Properties["Fam_cFamilia"] = General.PrimerValor(e.NewValue.ToString());
        }

        private void tdbcUnidad_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            txtUnidad.Text = e.NewValue.ToString();
            Application.Current.Properties["Cab_cUnidad"] = General.PrimerValor(e.NewValue.ToString());
        }

        private void btnTipoBS_Clicked(object sender, EventArgs e)
        {
            RedimencionarCombos();
            tdbcTipoBS.ItemsSource = LlenaTipoBS();
            tdbcTipoBS.IsOpen = true;
        }

        private void btnCategoria_Clicked(object sender, EventArgs e)
        {
            RedimencionarCombos();
            tdbcCategoria.ItemsSource = LlenaCategorias();
            tdbcCategoria.IsOpen = true;
        }

        private void btnGrupo_Clicked(object sender, EventArgs e)
        {
            RedimencionarCombos();
            tdbcGrupo.ItemsSource = LlenaGrupos();
            tdbcGrupo.IsOpen = true;
        }

        private void btnClase_Clicked(object sender, EventArgs e)
        {
            RedimencionarCombos();
            tdbcClase.ItemsSource = LlenaClases();
            tdbcClase.IsOpen = true;
        }

        private void btnFamilia_Clicked(object sender, EventArgs e)
        {
            RedimencionarCombos();
            tdbcFamilia.ItemsSource = LlenaFamilia();
            tdbcFamilia.IsOpen = true;
        }

        private void btnUnidad_Clicked(object sender, EventArgs e)
        {
            RedimencionarCombos();
            tdbcUnidad.ItemsSource = LlenaUnidades();
            tdbcUnidad.IsOpen = true;
        }

    }
}
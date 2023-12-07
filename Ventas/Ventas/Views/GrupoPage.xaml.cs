﻿using Newtonsoft.Json;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Extensions;
using Ventas.Models;
using Ventas.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ventas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GrupoPage : ContentPage
    {

        string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;
        string cUsuario = Application.Current.Properties["Usu_cCodUsuario"] as string;
        string cCategoria = Application.Current.Properties["Aca_cCategoria"] as string;

        DataView view;

        public GrupoPage()
        {
            InitializeComponent();

            try
            {
                dataGrid.ColumnSizer = ColumnSizer.LastColumnFill;

                dataGrid.CanUseViewFilter = true;
                view = new DataView();

                LlenarTabla();

                dataGrid.ItemsSource = view;
            }
            catch (Exception)
            {
            }
        }

        private async void btnNuevo_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["Opcion"] = Opciones.Nuevo;

            await Shell.Current.GoToAsync($"{nameof(GrupodetallePage)}");
        }

        private async void btnConsultar_Clicked(object sender, EventArgs e)
        {
            string cCodigo = Application.Current.Properties["Gru_cGrupo"] as string;

            if (General.CE(cCodigo) != "")
            {
                Application.Current.Properties["Opcion"] = Opciones.Consulta;

                await Shell.Current.GoToAsync($"{nameof(GrupodetallePage)}");


            }
        }

        private async void btnModificar_Clicked(object sender, EventArgs e)
        {
            string cCodigo = Application.Current.Properties["Gru_cGrupo"] as string;

            if (General.CE(cCodigo) != "")
            {
                Application.Current.Properties["Opcion"] = Opciones.Editar;

                await Shell.Current.GoToAsync($"{nameof(GrupodetallePage)}");
            }
        }

        private async void Eliminar()
        {
            try
            {
                string cGrupo = Application.Current.Properties["Gru_cGrupo"] as string;
                string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;
                string cOpcion = Application.Current.Properties["Opcion"] as string;
                string cCategoria = Application.Current.Properties["Aca_cCategoria"] as string;


                LGT_GRUPO_BS_Entity oEntidad = new LGT_GRUPO_BS_Entity();

                oEntidad.Accion = "ELIMINAR";
                oEntidad.Emp_cCodigo = cEmpresa;
                oEntidad.Aca_cCategoria = cCategoria;
                oEntidad.Gru_cGrupo = cGrupo;


                string  resultPost = ProcedimientosAPI.GetPostEliminarGrupo(oEntidad);
                MENSAJE_Entity oResult = JsonConvert.DeserializeObject<MENSAJE_Entity>(resultPost);

                if (oResult.Resultado == "OK")
                {
                    if (oResult.FilasAfectadas > 0)
                        await DisplayAlert(Title, "Se eliminó correctamente el registro", "OK");
                    else
                        await DisplayAlert(Title, "No se eliminó correctamente el registro", "OK");
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
        
        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                    bool answer = await DisplayAlert("Eliminar", "Deseas eliminar el registro seleccionado", "SI", "NO");

                    if (answer == true)
                    {
                        Eliminar();
                        LlenarTabla();
                    }
                
            }
            catch (Exception ex)
            {

                await DisplayAlert("Mensaje", ex.Message, "Aceptar");
            }
        }

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//DefaultPage");
        }

        private void filterText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != null)
            {
                view.RowFilter = "Gru_cGrupo like '%" + e.NewTextValue + "%' or Gru_cDescripLarga like '%" + e.NewTextValue + "%' ";
            }
        }

        private void dataGrid_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            try
            {
                Application.Current.Properties["Gru_cGrupo"] = dataGrid.GetCellValue(e.AddedItems[0], "Gru_cGrupo");
                Application.Current.Properties["Aca_cCategoria"] = dataGrid.GetCellValue(e.AddedItems[0], "Aca_cCategoria");
            }
            catch (Exception)
            {
            }

        }

        private void LlenarTabla()
        {
            var oEntidad = new GrupoViewModel();
            view.Table = oEntidad.GetData();

        }

        private void btnRefrescar_Clicked(object sender, EventArgs e)
        {
            LlenarTabla();
        }


    }
}
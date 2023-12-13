using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Helpers;
using RestoPLUS.Models;
using System.Threading.Tasks;
using RestoPLUS.Views;
using Newtonsoft.Json;
using RestoPLUS.Extensions;
using System.Data;
using System.Windows.Input;
using Syncfusion.SfRangeSlider.XForms;
using System.ComponentModel;
namespace RestoPLUS.ViewModels
{
    public class PedidosViewModel : INotifyPropertyChanged
    {

        internal TableInfo TappedItem { get; set; }
        public Command<object> ButtonCommand { get; set; }
        public Command<object> ItemTappedCommand { get; set; }

        public ICommand RefreshCommand { get; private set; }

        private ObservableCollection<TableInfo> tableInfo;
        public ObservableCollection<TableInfo> TableInfo
        {
            get { return tableInfo; }
            set
            {
                tableInfo = value;

                if (tableInfo != value)
                {
                    tableInfo = value;
                    OnPropertyChanged(nameof(TableInfo));
                }
            }
        }

        public PedidosViewModel()
        {
            TableInfo = new ObservableCollection<TableInfo>();

            RefreshCommand = new Command(RefreshList);

            ItemTappedCommand = new Command<object>(OnItemTapped);

            RefreshList();
        }


       public DataTable GetData()
        {
            try {
                string Pan_cAnio = Application.Current.Properties["Pan_cAnio"] as string;
                string Emp_cCodigo = Application.Current.Properties["Emp_cCodigo"] as string;
                string Pvt_cCodigo = Application.Current.Properties["Pvt_cCodigo"] as string;
                string Usu_cCodUsuario = Application.Current.Properties["Usu_cCodUsuario"] as string;

                VTM_RESTO_MESAS_Entity oEntidad = new VTM_RESTO_MESAS_Entity();

                oEntidad.Accion = "BUSCARMESASXUSUARIO";
                oEntidad.Emp_cCodigo = Emp_cCodigo;
                oEntidad.Pvt_cCodigo = Pvt_cCodigo;
                oEntidad.Mes_cUser = Usu_cCodUsuario;


                string result = ProcedimientosAPI.BuscarMesasPorUsuario(oEntidad);

                DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                dt.TableName = "Mesas";
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }


        }



        internal void RefreshList()
        {
            try
            {
                tableInfo.Clear();

                tableInfo = new ObservableCollection<TableInfo>();

                DataTable dt = new DataTable();
                dt = GetData();

                foreach (DataRow row in dt.Rows)
                {
                    TableInfo elemento = new TableInfo();
                    elemento.Mes_cDescripcion = row["Mes_cDescripcion"].ToString();
                    elemento.Ent_cPersona = row["Ent_cPersona"].ToString();
                    elemento.Ape_cUserCrea = row["Ape_cUserCrea"].ToString();
                    elemento.Mes_cCodigo = row["Mes_cCodigo"].ToString();
                    elemento.Pvt_cCodigo = row["Pvt_cCodigo"].ToString();
                    elemento.Res_cNummov = row["Res_cNummov"].ToString();
                    elemento.Ped_cNummov = row["Ped_cNummov"].ToString();


                    if (row["Ape_dFechaCrea"] != DBNull.Value && DateTime.TryParse(row["Ape_dFechaCrea"].ToString(), out DateTime fechaCrea))
                    {
                        if (fechaCrea.Year == 1900)
                        {
                            elemento.Ape_dFechaCrea = null;
                        }
                        else
                        {
                            elemento.Ape_dFechaCrea = fechaCrea;
                        }
                    }
                    else
                    {
                        elemento.Ape_dFechaCrea = null;
                    }

                    tableInfo.Add(elemento);
                }

                OnPropertyChanged(nameof(TableInfo));
            }
            catch (Exception ex)
            {

            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #region Methods

        private async void OnItemTapped(object obj)
        {
            // Title = "Mesas";

            var ItemData = (obj as Syncfusion.ListView.XForms.ItemTappedEventArgs).ItemData as TableInfo;

            Application.Current.Properties["Mes_cCodigo"] = ItemData.Mes_cCodigo;
            Application.Current.Properties["Mes_cDescripcion"] = ItemData.Mes_cDescripcion;
            Application.Current.Properties["Ent_cPersona"] = ItemData.Ent_cPersona;
            //Application.Current.Properties["Pvt_cCodigo"] = ItemData.Pvt_cCodigo;
            Application.Current.Properties["Res_cNummov"] = ItemData.Res_cNummov;
            Application.Current.Properties["Ped_cNummov"] = ItemData.Ped_cNummov;

            String UserTable = ItemData.Ape_cUserCrea;

            if (ItemData.Res_cNummov == "")
            {
                Application.Current.Properties["Opcion"] = Opciones.Nuevo;
            }
            else
            {
                Application.Current.Properties["Opcion"] = Opciones.Editar;
            }

            string Usu_cCodUsuario = Application.Current.Properties["Usu_cCodUsuario"] as string;

            if (Usu_cCodUsuario == UserTable || UserTable == "")
            {
                await Shell.Current.GoToAsync($"{nameof(PedidosDetallePage)}");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("La " + ItemData.Mes_cDescripcion, " esta atendida por " + ItemData.Ent_cPersona, "Ok");
            }
        }


        #endregion
    }
}

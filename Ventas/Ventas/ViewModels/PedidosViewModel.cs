using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Helpers;
using RestoAPP.Models;
using System.Threading.Tasks;
using RestoAPP.Views;
using Newtonsoft.Json;
using RestoAPP.Extensions;
using System.Data;
namespace RestoAPP.ViewModels
{
    public class PedidosViewModel : BaseViewModel
    {
        private ObservableCollection<TableInfo> tableInfo;
        internal TableInfo TappedItem { get; set; }
        public Command<object> ButtonCommand { get; set; }
        public Command<object> ItemTappedCommand { get; set; }

        public ObservableCollection<TableInfo> TableInfo
        {
            get { return tableInfo; }
            set { tableInfo = value; }
        }

        public PedidosViewModel()
        {
            Title = "Pedidos";

            ItemTappedCommand = new Command<object>(OnItemTapped);

            GenerateTableInfo();
        }

        public DataTable GetData()
        {
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
            dt.TableName = Title;
            return dt;
        }


        internal void GenerateTableInfo()
        {
            tableInfo = new ObservableCollection<TableInfo>();

            DataTable dt = new DataTable();
            dt = GetData();

            foreach (DataRow row  in dt.Rows )
            {
 

                tableInfo.Add(new TableInfo() { Mes_cDescripcion = row["Mes_cDescripcion"].ToString() , 
                                                Ent_cPersona = row["Ent_cPersona"].ToString(),
                                                Ape_cUserCrea = row["Ape_cUserCrea"].ToString(),
                                                Mes_cCodigo = row["Mes_cCodigo"].ToString(),
                                                Pvt_cCodigo = row["Pvt_cCodigo"].ToString(),
                                                Res_cNummov = row["Res_cNummov"].ToString(),
                                                Ped_cNummov = row["Ped_cNummov"].ToString()
                                               }); ;
            }


        }

        #region Methods

        private async void OnItemTapped(object obj)
        {
            Title = "Mesas";

            var ItemData = (obj as Syncfusion.ListView.XForms.ItemTappedEventArgs).ItemData as TableInfo;

            Application.Current.Properties["Mes_cCodigo"] = ItemData.Mes_cCodigo;
            Application.Current.Properties["Mes_cDescripcion"] = ItemData.Mes_cDescripcion;
            Application.Current.Properties["Ent_cPersona"] = ItemData.Ent_cPersona;
            Application.Current.Properties["Pvt_cCodigo"] = ItemData.Pvt_cCodigo;
            Application.Current.Properties["Res_cNummov"] = ItemData.Res_cNummov;
            Application.Current.Properties["Ped_cNummov"] = ItemData.Ped_cNummov;

            String UserTable = ItemData.Ape_cUserCrea;

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

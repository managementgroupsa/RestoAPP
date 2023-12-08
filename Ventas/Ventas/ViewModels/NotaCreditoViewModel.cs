using Newtonsoft.Json;
using RestoAPP.Models;
using Xamarin.Forms;
using System.Data;
using RestoAPP.Extensions;
using System;
using System.Collections.ObjectModel;

namespace RestoAPP.ViewModels
{
    internal class NotaCreditoViewModel : BaseViewModel
    {

        private ObservableCollection<Meses> mesesCollection;
        public ObservableCollection<Meses> MesesCollection
        {
            get { return mesesCollection; }
            set { mesesCollection = value; }
        }


        public DataTable GetData()
        {
            string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;
            string cAnio = Application.Current.Properties["Pan_cAnio"] as string;
            string cPuntoVenta = Application.Current.Properties["Pvt_cCodigo"] as string;
            string cPeriodo = Application.Current.Properties["Per_cPeriodo"] as string;


            //if (string.IsNullOrEmpty( cPeriodo ))
            //{
            //    cPeriodo = General.Right( "00" + DateTime.Now.Month.ToString(),2);
            //}

            VTM_NOTACREDITO_Entity oEntidad = new VTM_NOTACREDITO_Entity();

            oEntidad.Accion = "BUSCARTODOS_APP";
            oEntidad.Emp_cCodigo = cEmpresa;
            oEntidad.Pan_cAnio = cAnio;
            oEntidad.Per_cPeriodo = cPeriodo;
            oEntidad.Pvt_cCodigo = cPuntoVenta;

            string result = ProcedimientosAPI.GetPostBuscarNotas_CAB(oEntidad);

            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
            dt.TableName = Title;
            return dt;
        }

        public NotaCreditoViewModel()
        {
            Title = "Nota de Credito";

            MesesViewModel oMesesViewModel = new MesesViewModel();
            mesesCollection = oMesesViewModel.MesesCollection;

        }

    }
}




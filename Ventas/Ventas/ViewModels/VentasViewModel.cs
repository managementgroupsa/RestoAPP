using Newtonsoft.Json;
using Ventas.Models;
using Xamarin.Forms;
using System.Data;
using Ventas.Extensions;
using System;
using System.Collections.ObjectModel;

namespace Ventas.ViewModels
{
    internal class VentasViewModel:BaseViewModel
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
            
            VTM_DOC_VENTA_Entity oEntidad = new VTM_DOC_VENTA_Entity();

            oEntidad.Accion = "BUSCARTODOS_APP";
            oEntidad.Emp_cCodigo = cEmpresa;
            oEntidad.Pan_cAnio= cAnio;
            oEntidad.Per_cPeriodo = cPeriodo;
            oEntidad.Pvt_cCodigo = cPuntoVenta;

            string result = ProcedimientosAPI.GetPostBuscarVentas_CAB(oEntidad);

            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
            dt.TableName = Title;
            return dt;
        }

        public VentasViewModel()
        {
            Title = "Ventas";

            MesesViewModel oMesesViewModel = new MesesViewModel();
            mesesCollection = oMesesViewModel.MesesCollection;

        }
    }
}

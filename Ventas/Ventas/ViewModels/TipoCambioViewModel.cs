using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Ventas.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Data;
using System.ComponentModel;
using System.Dynamic;
using Ventas.Extensions;

namespace Ventas.ViewModels
{
    public class TipoCambioViewModel : BaseViewModel
    {
        public DataTable GetData()
        {
            

            string cEmpresa= Application.Current.Properties["Emp_cCodigo"] as string;
            string cPeriodo = Application.Current.Properties["Per_cPeriodo"] as string;

            CNT_TIPO_CAMBIO_Entity oEntidad = new CNT_TIPO_CAMBIO_Entity();

            oEntidad.Accion = "SEL_ALL_APP";
            oEntidad.Emp_cCodigo = cEmpresa;
            oEntidad.Tca_dFecha = DateTime.UtcNow.ToString("yyyy-MM-dd");
            oEntidad.Tca_cCodigoOrigen = "038";
            oEntidad.Tca_cCodigoDestino = "040";            
            oEntidad.Tca_cPeriodo = cPeriodo;


            string result = ProcedimientosAPI.GetPostBuscarTipoCambios(oEntidad);

            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
            dt.TableName = Title;
            return dt;
        }
        public TipoCambioViewModel()
        {
            Title = "Tipo de Cambio";
        }

    }
}

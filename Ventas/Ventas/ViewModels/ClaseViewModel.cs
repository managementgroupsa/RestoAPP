using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Ventas.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Data;
using Ventas.Extensions;

namespace Ventas.ViewModels
{
    public class ClaseViewModel : BaseViewModel
    {
        public DataTable GetData()
        {

            Title = "Clase";

            string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;

            LGT_CLASE_BS_Entity oEntidad = new LGT_CLASE_BS_Entity();

            oEntidad.Accion = "BUSCARTODOS";
            oEntidad.Emp_cCodigo = cEmpresa;


            string result = ProcedimientosAPI.GetPostBuscarClases(oEntidad);

            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
            dt.TableName = Title;
            return dt;
        }

        public ClaseViewModel()
        {

            Title = "Clase";
        }


    }
}


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using RestoAPP.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Data;
using RestoAPP.Extensions;

namespace RestoAPP.ViewModels
{
    public class FamiliaViewModel : BaseViewModel
    {
        public DataTable GetData()
        {

            Title = "Familia";

            string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;

            LGT_FAMILIA_Entity oEntidad = new LGT_FAMILIA_Entity();

            oEntidad.Accion = "BUSCARTODOS";
            oEntidad.Emp_cCodigo = cEmpresa;


            string result = ProcedimientosAPI.GetPostBuscarFamilias(oEntidad);

            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
            dt.TableName = Title;
            return dt;
        }

        public FamiliaViewModel()
        {

            Title = "Familia";
        }


    }
}


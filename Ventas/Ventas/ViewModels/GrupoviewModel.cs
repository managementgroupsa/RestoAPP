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
    public class GrupoViewModel : BaseViewModel
    {
        public DataTable GetData()
        {

            string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;

            LGT_GRUPO_BS_Entity oEntidad = new LGT_GRUPO_BS_Entity();

            oEntidad.Accion = "BUSCARTODOS";
            oEntidad.Emp_cCodigo = cEmpresa;


            string result = ProcedimientosAPI.GetPostBuscarGrupos(oEntidad);

            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
            dt.TableName = Title;
            return dt;
        }

        public GrupoViewModel()
        {

            Title = "Grupo";
        }


    }
}


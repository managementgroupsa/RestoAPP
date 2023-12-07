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
    public class CatalogoViewModel:BaseViewModel
    {
        public DataTable GetData()
        {

            string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;

            LGM_CATALOGO_BS_Entity oEntidad = new LGM_CATALOGO_BS_Entity();

            oEntidad.Accion = "BUSCARTODOS";
            oEntidad.Emp_cCodigo = cEmpresa;


            string result = ProcedimientosAPI.GetPostBuscarCatalogos(oEntidad);

            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
            dt.TableName = Title;
            return dt;
        }

        public CatalogoViewModel()
        {

            Title = "Catalogo";
        }


    }
}


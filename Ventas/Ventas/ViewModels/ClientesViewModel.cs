using Newtonsoft.Json;
using RestoAPP.Models;
using Xamarin.Forms;
using System.Data;
using RestoAPP.Extensions;

namespace RestoAPP.ViewModels
{
    public class ClientesViewModel : BaseViewModel
    {

        public DataTable GetData()
        {

            string cEmpresa;
            string cTipoEntidad;

            cTipoEntidad = "C";
            cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;

            CNM_ENTIDAD_Entity oEntidad = new CNM_ENTIDAD_Entity();

            oEntidad.Accion = "SEL_ALL";
            oEntidad.Emp_cCodigo = cEmpresa;
            oEntidad.Ten_cTipoEntidad = cTipoEntidad;
            

            string result = ProcedimientosAPI.GetPostBuscarEntidades(oEntidad);

            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
            dt.TableName = Title;
            return dt;
        }

        public ClientesViewModel() 
        {
            Title = "Clientes";
        }




    }



}

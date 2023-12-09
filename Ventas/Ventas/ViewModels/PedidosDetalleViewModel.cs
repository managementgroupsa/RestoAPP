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
namespace RestoAPP.ViewModels
{
    public class PedidosDetalleViewModel : BaseViewModel
    {


        public PedidosDetalleViewModel()
        {
            
            Title = Application.Current.Properties["Mes_cDescripcion"].ToString()  + " - " + Application.Current.Properties["Ent_cPersona"].ToString () ;

        }

        

    }
}

﻿using Newtonsoft.Json;
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
    public class CatalogoPrecioViewModel : BaseViewModel
    {
        public DataTable GetData()
        {

            string cEmpresa = Application.Current.Properties["Emp_cCodigo"] as string;

            VTD_CATALOGO_PRECIO_Entity oEntidad = new VTD_CATALOGO_PRECIO_Entity();

            oEntidad.Accion = "BUSCARTODOS";
            oEntidad.Emp_cCodigo = cEmpresa;


            string result = ProcedimientosAPI.GetPostBuscarPrecios(oEntidad);

            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
            dt.TableName = Title;
            return dt;
        }

        public CatalogoPrecioViewModel()
        {

            Title = "Lista de Precios";
        }


    }
}
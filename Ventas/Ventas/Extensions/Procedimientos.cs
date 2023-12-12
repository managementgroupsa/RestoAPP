using System;
using RestoPLUS.Models;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Xamarin.Forms;
using RestoPLUS.Views;
using System.Threading;
using System.Reflection;

namespace RestoPLUS.Extensions
{
    class ProcedimientosAPI
    {
        private static string cURL = Application.Current.Properties["URL"] as string;



        #region API-Funciones
        public static string GetPostResultString(string url, object oEntidad)
        {
            string result = "";

            try
            {
                string token = Application.Current.Properties["Token"] as string;

                if (!string.IsNullOrEmpty(token))
                { 
                    WebRequest oRequest = WebRequest.Create(url);
                    oRequest.Method = "POST";
                    oRequest.ContentType = "application/json; charset=utf-8";
                    oRequest.Headers.Add("Authorization", "Bearer " + token);


                    using (var oSW = new StreamWriter(oRequest.GetRequestStream()))
                    {
                        string json = JsonConvert.SerializeObject(oEntidad);

                        oSW.Write(json);
                        oSW.Flush();
                        oSW.Close();
                    }

                    WebResponse oResponse = oRequest.GetResponse();

                    using (var oSR = new StreamReader(oResponse.GetResponseStream()))
                    {
                        result = oSR.ReadToEnd().Trim();
                    }
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public static string GetPostBuscarToken(Login_Entity oEntidad)
        {
            string result = "";

            try
            {



                WebRequest oRequest = WebRequest.Create(cURL + "/LOGIN");
                oRequest.Method = "POST";
                oRequest.ContentType = "application/json";

                System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicy) => { return true; };

                using (var oSW = new StreamWriter(oRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(oEntidad);

                    oSW.Write(json);
                    oSW.Flush();
                    oSW.Close();
                }

                WebResponse oResponse = oRequest.GetResponse();

                using (var oSR = new StreamReader(oResponse.GetResponseStream()))
                {
                    result = oSR.ReadToEnd().Trim();
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }

            return result;

        }

        #endregion

        #region API-Logeo




        public static string GetPostBuscarCredenciales(Login_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LOGIN/ValidaIngreso", oEntidad);
        }

        public static string GetPostBuscarPuntosVenta(Login_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LOGIN/BuscarPuntosVenta", oEntidad);
        }


        public static string GetPostBuscarAnios(Login_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LOGIN/BuscarAnios", oEntidad);
        }

        public static string GetPostBuscarEmpresas(Login_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LOGIN/BuscarEmpresas", oEntidad);
        }

        #endregion

        #region API-NEGOCIO

        public static string GetPostBuscarMesas(Login_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LOGIN/ValidaIngreso", oEntidad);
        }

        public static string GetPostGrabarPedido(PEDIDO_Entity oEntidad)
        {

            return GetPostResultString(cURL + "/GENERAL/GrabarPedido", oEntidad);
        }
        
        public static string BuscarPedidosPorUsuario(VTD_RESTO_APERTURA_PEDIDO_Entity oEntidad)
        {

            return GetPostResultString(cURL + "/VTD_RESTO_APERTURA_PEDIDO/BuscarPedidosPorUsuario", oEntidad);
        }

        public static string BuscarMesasPorUsuario(VTM_RESTO_MESAS_Entity oEntidad)
        {

            return GetPostResultString(cURL + "/VTM_RESTO_MESAS/BuscarMesasPorUsuario", oEntidad);
        }

        public static string BuscarCatalogo(VTD_RESTO_APERTURA_PEDIDO_Entity oEntidad)
        {

            return GetPostResultString(cURL + "/VTD_RESTO_APERTURA_PEDIDO/BuscarCatalogo", oEntidad);
        }
        #endregion

        #region API-Tabla

        public static string GetPostBuscarTablas(TABLA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/MAN_TABLA/SeleccionarTodos", oEntidad);
        }

        public static string GetPostBuscarTabla(TABLA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/MAN_TABLA/SeleccionarRegistro", oEntidad);
        }


        #endregion





    }
}







using System;
using Ventas.Models;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Xamarin.Forms;
using Ventas.Views;
using System.Threading;
using System.Reflection;

namespace Ventas.Extensions
{
    class ProcedimientosAPI
    {
        private static string cURL = Application.Current.Properties["URL"] as string;


   

        #region API-Sunat
        public static string GetPostBuscarRUC(SUNAT_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/SUNAT/ConsultaRUC?cDocumento=" + oEntidad.ruc, oEntidad);
        }

        public static string GetPostBuscarDNI(SUNAT_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/SUNAT/ConsultaDNI?cDocumento=" + oEntidad.ruc, oEntidad);
        }

        public static string GetPostBuscarTC(DIA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/SUNAT/ConsultaTC?cFecha=" + oEntidad.Fecha.ToString("yyyy-MM-dd"), oEntidad);
        }

        #endregion

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

        #region API-Catalogo
        public static string GetPostBuscarCatalogos(LGM_CATALOGO_BS_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGM_CATALOGO_BS/SeleccionarTodos", oEntidad);
        }

        public static string GetPostBuscarCatalogo(LGM_CATALOGO_BS_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGM_CATALOGO_BS/SeleccionarRegistro", oEntidad);
        }

        public static string GetPostInsertarCatalogo(LGM_CATALOGO_BS_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGM_CATALOGO_BS/InsertarRegistro", oEntidad);
        }

        public static string GetPostEditarCatalogo(LGM_CATALOGO_BS_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGM_CATALOGO_BS/EditarRegistro", oEntidad);
        }

        public static string GetPostEliminarCatalogo(LGM_CATALOGO_BS_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGM_CATALOGO_BS/EliminarRegistro", oEntidad);
        }

        #endregion

        #region API-Categoria

        public static string GetPostBuscarCategoria(LGT_CATEGORIA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGT_CATEGORIA/SeleccionarRegistro", oEntidad);
        }

        public static string GetPostInsertarCategoria(LGT_CATEGORIA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGT_CATEGORIA/InsertarRegistro", oEntidad);
        }

        public static string GetPostEditarCategoria(LGT_CATEGORIA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGT_CATEGORIA/EditarRegistro", oEntidad);
        }

        public static string GetPostEliminarCategoria(LGT_CATEGORIA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGT_CATEGORIA/EliminarRegistro", oEntidad);
        }

        #endregion

        #region API-Grupo

        public static string GetPostBuscarGrupo(LGT_GRUPO_BS_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGT_GRUPO_BS/SeleccionarRegistro", oEntidad);
        }

        public static string GetPostInsertarGrupo(LGT_GRUPO_BS_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGT_GRUPO_BS/InsertarRegistro", oEntidad);
        }

        public static string GetPostEditarGrupo(LGT_GRUPO_BS_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGT_GRUPO_BS/EditarRegistro", oEntidad);
        }

        public static string GetPostEliminarGrupo(LGT_GRUPO_BS_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGT_GRUPO_BS/EliminarRegistro", oEntidad);
        }

        #endregion

        #region API-Clase

        public static string GetPostBuscarClase(LGT_CLASE_BS_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGT_CLASE_BS/SeleccionarRegistro", oEntidad);
        }

        public static string GetPostInsertarClase(LGT_CLASE_BS_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGT_CLASE_BS/InsertarRegistro", oEntidad);
        }

        public static string GetPostEditarClase(LGT_CLASE_BS_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGT_CLASE_BS/EditarRegistro", oEntidad);
        }

        public static string GetPostEliminarClase(LGT_CLASE_BS_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGT_CLASE_BS/EliminarRegistro", oEntidad);
        }

        #endregion

        #region API-Familia

        public static string GetPostBuscarFamilia(LGT_FAMILIA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGT_FAMILIA/SeleccionarRegistro", oEntidad);
        }

        public static string GetPostInsertarFamilia(LGT_FAMILIA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGT_FAMILIA/InsertarRegistro", oEntidad);
        }

        public static string GetPostEditarFamilia(LGT_FAMILIA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGT_FAMILIA/EditarRegistro", oEntidad);
        }

        public static string GetPostEliminarFamilia(LGT_FAMILIA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGT_FAMILIA/EliminarRegistro", oEntidad);
        }

        #endregion

        #region API-TipoCambio

        public static string GetPostBuscarTipoCambios(CNT_TIPO_CAMBIO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/CNT_TIPO_CAMBIO/SeleccionarTodos", oEntidad);
        }

        public static string GetPostBuscarTipoCambio(CNT_TIPO_CAMBIO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/CNT_TIPO_CAMBIO/SeleccionarRegistro", oEntidad);
        }

        public static string GetPostInsertarTipoCambio(CNT_TIPO_CAMBIO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/CNT_TIPO_CAMBIO/InsertarRegistro", oEntidad);
        }

        public static string GetPostEditarTipoCambio(CNT_TIPO_CAMBIO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/CNT_TIPO_CAMBIO/EditarRegistro", oEntidad);
        }

        public static string GetPostEliminarTipoCambio(CNT_TIPO_CAMBIO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/CNT_TIPO_CAMBIO/EliminarRegistro", oEntidad);
        }


        #endregion

        #region API-Entidades

        public static string GetPostBuscarEntidades(CNM_ENTIDAD_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/CNM_ENTIDAD/SeleccionarTodos", oEntidad);
        }

        public static string GetPostBuscarEntidad(CNM_ENTIDAD_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/CNM_ENTIDAD/SeleccionarRegistro", oEntidad);
        }

        public static string GetPostInsertarEntidad(CNM_ENTIDAD_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/CNM_ENTIDAD/InsertarRegistro", oEntidad);
        }

        public static string GetPostEditarEntidad(CNM_ENTIDAD_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/CNM_ENTIDAD/EditarRegistro", oEntidad);
        }

        public static string GetPostEliminarEntidad(CNM_ENTIDAD_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/CNM_ENTIDAD/EliminarRegistro", oEntidad);
        }

        public static string GetPostConsultaDocumento(CNM_ENTIDAD_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/CNM_ENTIDAD/ConsultaDocumento", oEntidad);
        }


        #endregion

        #region API-Clasificaciones
        public static string GetPostBuscarCategorias(LGT_CATEGORIA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGT_CATEGORIA/SeleccionarTodos", oEntidad);
        }
        public static string GetPostBuscarGrupos(LGT_GRUPO_BS_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGT_GRUPO_BS/SeleccionarTodos", oEntidad);
        }
        public static string GetPostBuscarClases(LGT_CLASE_BS_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGT_CLASE_BS/SeleccionarTodos", oEntidad);
        }
        public static string GetPostBuscarFamilias(LGT_FAMILIA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/LGT_FAMILIA/SeleccionarTodos", oEntidad);
        }
        #endregion

        #region API-VentasCAB


        public static string GetPostBuscarVentas_CAB(VTM_DOC_VENTA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTM_DOC_VENTA/SeleccionarTodos", oEntidad);
        }

        public static string GetPostBuscarVenta_CAB(VTM_DOC_VENTA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTM_DOC_VENTA/SeleccionarRegistro", oEntidad);
        }

        public static string GetPostInsertarVenta_CAB(VTM_DOC_VENTA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTM_DOC_VENTA/InsertarRegistro", oEntidad);
        }

        public static string GetPostEditarVenta_CAB(VTM_DOC_VENTA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTM_DOC_VENTA/EditarRegistro", oEntidad);
        }

        public static string GetPostEliminarVenta_CAB(VTM_DOC_VENTA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTM_DOC_VENTA/EliminarRegistro", oEntidad);
        }
        public static string GetPostAnularVenta_CAB(VTM_DOC_VENTA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTM_DOC_VENTA/AnularRegistro", oEntidad);
        }

        #endregion

        #region API-VentasDET

        public static string GetPostBuscarVentas_DET(VTD_DOC_VENTA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTD_DOC_VENTA/SeleccionarTodos", oEntidad);
        }

        public static string GetPostBuscarVenta_DET(VTD_DOC_VENTA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTD_DOC_VENTA/SeleccionarRegistro", oEntidad);
        }

        public static string GetPostInsertarVenta_DET(VTD_DOC_VENTA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTD_DOC_VENTA/InsertarRegistro", oEntidad);
        }

        public static string GetPostEditarVenta_DET(VTD_DOC_VENTA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTD_DOC_VENTA/EditarRegistro", oEntidad);
        }

        public static string GetPostEliminarVenta_DET(VTD_DOC_VENTA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTD_DOC_VENTA/EliminarRegistro", oEntidad);
        }


        #endregion

        #region API-NotasCAB

        public static string GetPostBuscarNotas_CAB(VTM_NOTACREDITO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTM_NOTACREDITO/SeleccionarTodos", oEntidad);
        }

        public static string GetPostBuscarNota_CAB(VTM_NOTACREDITO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTM_NOTACREDITO/SeleccionarRegistro", oEntidad);
        }

        public static string GetPostInsertarNotas_CAB(VTM_NOTACREDITO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTM_NOTACREDITO/InsertarRegistro", oEntidad);
        }

        public static string GetPostEditarNotas_CAB(VTM_NOTACREDITO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTM_NOTACREDITO/EditarRegistro", oEntidad);
        }

        public static string GetPostEliminarNotas_CAB(VTM_NOTACREDITO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTM_NOTACREDITO/EliminarRegistro", oEntidad);
        }


        #endregion

        #region API-NotasDET

        public static string GetPostBuscarNotas_DET(VTD_NOTACREDITO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTD_DOC_VENTA/SeleccionarTodos", oEntidad);
        }

        public static string GetPostBuscarNota_DET(VTD_NOTACREDITO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTD_NOTACREDITO/SeleccionarRegistro", oEntidad);
        }

        public static string GetPostInsertarNotas_DET(VTD_NOTACREDITO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTD_NOTACREDITO/InsertarRegistro", oEntidad);
        }

        public static string GetPostEditarNotas_DET(VTD_NOTACREDITO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTD_NOTACREDITO/EditarRegistro", oEntidad);
        }

        public static string GetPostEliminarNotas_DET(VTD_NOTACREDITO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTD_NOTACREDITO/EliminarRegistro", oEntidad);
        }


        #endregion

        #region API-VentasCUOTAS

        public static string GetPostBuscarVentas_CUOTAS(VTM_DOC_VENTA_CUOTAS_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTM_DOC_VENTA_CUOTAS/SeleccionarTodos", oEntidad);
        }

        public static string GetPostBuscarVenta_CUOTAS(VTM_DOC_VENTA_CUOTAS_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTM_DOC_VENTA_CUOTAS/SeleccionarRegistro", oEntidad);
        }

        public static string GetPostInsertarVenta_CUOTAS(VTM_DOC_VENTA_CUOTAS_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTM_DOC_VENTA_CUOTAS/InsertarRegistro", oEntidad);
        }

        public static string GetPostEditarVenta_CUOTAS(VTM_DOC_VENTA_CUOTAS_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTM_DOC_VENTA_CUOTAS/EditarRegistro", oEntidad);
        }

        public static string GetPostEliminarVenta_CUOTAS(VTM_DOC_VENTA_CUOTAS_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTM_DOC_VENTA_CUOTAS/EliminarRegistro", oEntidad);
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

        #region API-General

        public static string GetPostEditaEstadoVenta(Estado_Documento oEntidad, string cEmpresa, string cAnio, string cNummov, string cValidaSunat)
        {
            string json = JsonConvert.SerializeObject(oEntidad, Formatting.Indented);

            return GetPostResultString(cURL + "/GENERAL/ActualizaEstadoSunat?cEmpresa=" + cEmpresa + "&" + "cAnio=" + cAnio + "&" + "cNummov=" + cNummov + "&" + "cValidaSunat=" + cValidaSunat, oEntidad);
        }

        public static string GetPostGrabarVenta(VENTAS_Entity oEntidad, string cEmpresa, string cAnio, string cNummov)
        {
            string cUrlApi = "";

            if (string.IsNullOrEmpty(cNummov))
                cUrlApi = cURL + "/GENERAL/GrabaVenta?cEmpresa=" + cEmpresa + "&" + "cAnio=" + cAnio;
            else
                cUrlApi = cURL + "/GENERAL/GrabaVenta?cEmpresa=" + cEmpresa + "&" + "cAnio=" + cAnio + "&" + "cNummov=" + cNummov;

            return GetPostResultString(cUrlApi, oEntidad);
        }
        public static string SiguienteNummovVentas(VTM_DOC_VENTA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/GENERAL/SiguienteNummovVentas", oEntidad);
        }
        public static string SiguienteNumCorrel(VTD_CATALOGO_PRECIO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/GENERAL/SiguienteNumCorrel", oEntidad);
        }
        public static string GetPostBuscarMeses(CNT_PERIODO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/GENERAL/Meses", oEntidad);
        }
        public static string GetPostBuscarTipoDocumento(CNT_TIPODOC_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/GENERAL/TipoDocumento", oEntidad);
        }
        public static string GetPostBuscarSerieDocumento(LGT_SERIEDOC_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/GENERAL/SerieDocumento", oEntidad);
        }
        public static string GetPostBuscarNumeroDocumento(LGT_SERIEDOC_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/GENERAL/NumeroDocumento", oEntidad);
        }
        public static string GetPostBuscarTipoMoneda(CNT_TIPO_MONEDA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/GENERAL/TipoMoneda", oEntidad);
        }
        public static string GetPostBuscarIncluyeIGV(CFG_PARAMETROS_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/GENERAL/IncluyeIGV", oEntidad);
        }
        public static string GetPostBuscarDatosDetraccion(EMPRESA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/GENERAL/DatosDetraccion", oEntidad);
        }
        public static string GetPostBuscarDastosRetencion(EMPRESA_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/GENERAL/DastosRetencion", oEntidad);
        }


        #endregion

        #region API-CatalogoPrecio


        public static string GetPostBuscarPrecios(VTD_CATALOGO_PRECIO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTD_CATALOGO_PRECIO/SeleccionarTodos", oEntidad);
        }

        public static string GetPostBuscarPrecio(VTD_CATALOGO_PRECIO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTD_CATALOGO_PRECIO/SeleccionarRegistro", oEntidad);
        }

        public static string GetPostInsertarPrecio(VTD_CATALOGO_PRECIO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTD_CATALOGO_PRECIO/InsertarRegistro", oEntidad);
        }

        public static string GetPostEditarPrecio(VTD_CATALOGO_PRECIO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTD_CATALOGO_PRECIO/EditarRegistro", oEntidad);
        }

        public static string GetPostEliminarPrecio(VTD_CATALOGO_PRECIO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTD_CATALOGO_PRECIO/EliminarRegistro", oEntidad);
        }


        #endregion


        #region API-TipoPrecio


        public static string GetPostBuscarTipoPrecios(VTT_TIPO_PRECIO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTT_TIPO_PRECIO/SeleccionarTodos", oEntidad);
        }

        public static string GetPostBuscarTipoPrecio(VTT_TIPO_PRECIO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTT_TIPO_PRECIO/SeleccionarRegistro", oEntidad);
        }

        public static string GetPostInsertarTipoPrecio(VTT_TIPO_PRECIO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTT_TIPO_PRECIO/InsertarRegistro", oEntidad);
        }

        public static string GetPostEditarTipoPrecio(VTT_TIPO_PRECIO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTT_TIPO_PRECIO/EditarRegistro", oEntidad);
        }

        public static string GetPostEliminarTipoPrecio(VTT_TIPO_PRECIO_Entity oEntidad)
        {
            return GetPostResultString(cURL + "/VTT_TIPO_PRECIO/EliminarRegistro", oEntidad);
        }


        #endregion

    }
}







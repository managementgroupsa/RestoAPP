using System;
using System.Collections.Generic;
using System.IO;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using RestoAPP.Models;
using Newtonsoft.Json;

using System.Data;
using System.Net;
using System.Text;

namespace RestoAPP.Extensions
{
    public class Nubefact
    {

        public string ConsultarComprobanteVenta(string cEmpresa, string cAnio, string cNummov)
        {
            string result = "";

            try
            {
                NubeFact_Consulta_Generacion oComprobante = new NubeFact_Consulta_Generacion();

                string url = "https://api.pse.pe/api/v1/255e13e0a0494eeb969a64b10d966f99e619cb2b82f74b5b949b70105165b8e8";
                string token = "eyJhbGciOiJIUzI1NiJ9.IjQyOGRlNDhkYTZiZDRlZDdhMzkzNDZhNThhMTllYTAyMjUwNmUyZGMyODFkNGUwMmIxYTQzOGMxOTZjMzJlODUi.G1UuMohsZRbxvXAJtm30tIQFOfDB5K2wfqbsjbx8jCM";
                string cTipo = "";

                //---------------------------------------------------------------
                //------------------------ CABECERA -----------------------------
                //---------------------------------------------------------------

                VTM_DOC_VENTA_Entity oEntidad = new VTM_DOC_VENTA_Entity();

                oEntidad.Accion = "BUSCARREGISTRO";
                oEntidad.Emp_cCodigo = cEmpresa;
                oEntidad.Pan_cAnio = cAnio;
                oEntidad.Dvm_cNumMov = cNummov;

                result = ProcedimientosAPI.GetPostBuscarVenta_CAB(oEntidad);

                result = "[" + result + "]";

                List<VTM_DOC_VENTA_Entity> response = JsonConvert.DeserializeObject<List<VTM_DOC_VENTA_Entity>>(result);

                oComprobante.operacion = "consultar_comprobante"; 


                foreach (var message in response)
                {
                    cTipo = message.Dvm_cTipoDoc;

                    oComprobante.tipo_de_comprobante = 2; // boleta
                    if (cTipo == "01")
                        oComprobante.tipo_de_comprobante = 1; // factura
                    if (cTipo == "07")
                        oComprobante.tipo_de_comprobante = 3; // nota de credito
                    if (cTipo == "08")
                        oComprobante.tipo_de_comprobante = 4; // nota de debito

                    oComprobante.serie = message.Dvm_cSerieDoc;
                    oComprobante.numero = Convert.ToInt16(message.Dvm_cNumDoc);
                }

                //---------------------------------------------------------------
                //----------------------- ENVIA JSON ----------------------------
                //---------------------------------------------------------------


                string json = JsonConvert.SerializeObject(oComprobante, Formatting.Indented);

                byte[] bytes = Encoding.Default.GetBytes(json);
                string json_en_utf_8 = Encoding.UTF8.GetString(bytes);

                string json_de_respuesta = SendJson(url, json_en_utf_8, token);
                Respuesta r = JsonConvert.DeserializeObject<Respuesta>(json_de_respuesta);
                string r2 = JsonConvert.SerializeObject(r, Formatting.Indented);
                Respuesta json_r_in = JsonConvert.DeserializeObject<Respuesta>(r2);

                //---------------------------------------------------------------
                //----------------------- RESPUESTA -----------------------------
                //---------------------------------------------------------------

                NubeFact_Respuesta_Generacion leer_respuesta = JsonConvert.DeserializeObject<NubeFact_Respuesta_Generacion>(json_de_respuesta);

                Console.WriteLine(json_r_in);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("TIPO: " + leer_respuesta.tipo_de_comprobante);
                Console.WriteLine("SERIE: " + leer_respuesta.serie);
                Console.WriteLine("NUMERO: " + leer_respuesta.numero);
                Console.WriteLine("URL: " + leer_respuesta.enlace);
                Console.WriteLine("ACEPTADA_POR_SUNAT: " + leer_respuesta.aceptada_por_sunat);
                Console.WriteLine("DESCRIPCION SUNAT: " + leer_respuesta.sunat_description);
                Console.WriteLine("NOTA SUNAT: " + leer_respuesta.sunat_note);
                Console.WriteLine("CODIGO RESPUESTA SUNAT: " + leer_respuesta.sunat_responsecode);
                Console.WriteLine("SUNAT ERROR SOAP: " + leer_respuesta.sunat_soap_error);
                Console.WriteLine("PDF EN BASE64: " + leer_respuesta.enlace_del_pdf);
                Console.WriteLine("XML EN BASE64: " + leer_respuesta.enlace_del_xml);
                Console.WriteLine("CDR EN BASE64: " + leer_respuesta.enlace_del_cdr);
                Console.WriteLine("CODIGO QR: " + leer_respuesta.cadena_para_codigo_qr);
                Console.WriteLine("CODIGO HASH: " + leer_respuesta.codigo_hash);
                Console.WriteLine("CODIGO DE BARRAS: " + leer_respuesta.codigo_de_barras);

                result = leer_respuesta.enlace_del_pdf;

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return result;
        }

        public string GenerarComprobanteVenta(string cEmpresa, string cAnio, string cNummov, string cCorreo)
        {
            string result = "";

            try
            {
                NubeFact_Invoice oComprobante = new NubeFact_Invoice();

                string cMoneda = "038";

                string url = "https://api.pse.pe/api/v1/255e13e0a0494eeb969a64b10d966f99e619cb2b82f74b5b949b70105165b8e8";
                string token = "eyJhbGciOiJIUzI1NiJ9.IjQyOGRlNDhkYTZiZDRlZDdhMzkzNDZhNThhMTllYTAyMjUwNmUyZGMyODFkNGUwMmIxYTQzOGMxOTZjMzJlODUi.G1UuMohsZRbxvXAJtm30tIQFOfDB5K2wfqbsjbx8jCM";
                string cTipo = "";

                //---------------------------------------------------------------
                //------------------------ CABECERA -----------------------------
                //---------------------------------------------------------------

                #region CABECERA



                VTM_DOC_VENTA_Entity oEntidad = new VTM_DOC_VENTA_Entity();

                oEntidad.Accion = "BUSCARREGISTRO";
                oEntidad.Emp_cCodigo = cEmpresa;
                oEntidad.Pan_cAnio = cAnio;
                oEntidad.Dvm_cNumMov = cNummov;

                result = ProcedimientosAPI.GetPostBuscarVenta_CAB(oEntidad);

                result = "[" + result + "]";

                List<VTM_DOC_VENTA_Entity> response = JsonConvert.DeserializeObject<List<VTM_DOC_VENTA_Entity>>(result);

                oComprobante.operacion = "generar_comprobante"; // boleta


                foreach (var message in response)
                {
                    cTipo = message.Dvm_cTipoDoc;

                    oComprobante.tipo_de_comprobante = 2; // boleta
                    if (cTipo == "01")
                        oComprobante.tipo_de_comprobante = 1; // factura
                    if (cTipo == "07")
                        oComprobante.tipo_de_comprobante = 3; // nota de credito
                    if (cTipo == "08")
                        oComprobante.tipo_de_comprobante = 4; // nota de debito

                    oComprobante.serie = message.Dvm_cSerieDoc;
                    oComprobante.numero = Convert.ToInt16(message.Dvm_cNumDoc);

                    if (message.Dvm_cNacExt == "N")
                        oComprobante.sunat_transaction = 1;// venta interna
                    if (message.Dvm_cNacExt == "E")
                        oComprobante.sunat_transaction = 2;// exportacion


                    //-----------------------------

                    CNM_ENTIDAD_Entity oCliente = new CNM_ENTIDAD_Entity();
                    oCliente.Accion = "SEL_CODENT";
                    oCliente.Emp_cCodigo = cEmpresa;
                    oCliente.Ten_cTipoEntidad = "C";
                    oCliente.Ent_cCodEntidad = message.Cli_cCodigo;

                    string resultCliente = ProcedimientosAPI.GetPostBuscarEntidad(oCliente);

                    resultCliente = "[" + resultCliente + "]";

                    List<CNM_ENTIDAD_Entity> responseCliente = JsonConvert.DeserializeObject<List<CNM_ENTIDAD_Entity>>(resultCliente);

                    foreach (var messageCliente in responseCliente)
                    {
                        oComprobante.cliente_tipo_de_documento = "-"; // varios

                        if (messageCliente.Ent_cTipoDoc == "04")
                            oComprobante.cliente_tipo_de_documento = "6";//factura

                        if (messageCliente.Ent_cTipoDoc == "01")
                            oComprobante.cliente_tipo_de_documento = "1";//dni

                        oComprobante.cliente_numero_de_documento = messageCliente.Ent_nRuc;
                        oComprobante.cliente_denominacion = messageCliente.Ent_cPersona;
                        oComprobante.cliente_direccion = messageCliente.Ent_cDireccion;
                        oComprobante.cliente_email = "mlopez@mgsa.com.pe";// messageCliente.En_nMail;
                    }

                    //-----------------------------

                    oComprobante.fecha_de_emision = General.ISO_To_DateTime(General.FechaISO(message.Dvm_dFechaEmision.ToString("dd/MM/yyyy")));
                    oComprobante.fecha_de_vencimiento = General.ISO_To_DateTime(General.FechaISO(message.Dvm_dFechaVenc.ToString("dd/MM/yyyy")));


                    cMoneda = message.Mon_cCodigo;

                    if (message.Mon_cCodigo == "040")
                    {
                        oComprobante.moneda = 2;// dolares
                        oComprobante.total_gravada = message.Dvm_nSubTotal;
                        oComprobante.total_igv = message.Dvm_nTotalImpto1;
                        oComprobante.total = message.Dvm_nTotal;
                    }
                    else
                    {
                        oComprobante.moneda = 1;// soles
                        oComprobante.total_gravada = message.Dvm_nSubTotal_MN;
                        oComprobante.total_igv = message.Dvm_nTotalImpto1_MN;
                        oComprobante.total = message.Dvm_nTotal_MN;
                    }


                    oComprobante.tipo_de_cambio = message.Dvm_nTipoCambio;
                    oComprobante.porcentaje_de_igv = 18;

                    oComprobante.descuento_global = 0;
                    oComprobante.total_descuento = 0;
                    oComprobante.total_anticipo = 0;
                    oComprobante.total_inafecta = 0;
                    oComprobante.total_exonerada = 0;
                    oComprobante.total_gratuita = 0;
                    oComprobante.total_otros_cargos = 0;
                    oComprobante.total_isc = 0;
                    oComprobante.percepcion_tipo = null; // 2%
                    oComprobante.percepcion_base_imponible = 0;
                    oComprobante.total_percepcion = 0;
                    oComprobante.total_incluido_percepcion = 0;
                    oComprobante.retencion_tipo = null;// 3%

                    oComprobante.retencion_base_imponible = 0;
                    oComprobante.total_retencion = 0;
                    oComprobante.total_impuestos_bolsas = 0;

                    oComprobante.detraccion = false;
                    oComprobante.observaciones = "";

                    oComprobante.enviar_automaticamente_a_la_sunat = true;

                    if (string.IsNullOrEmpty(cCorreo))
                    {
                        oComprobante.enviar_automaticamente_al_cliente = false;
                        oComprobante.cliente_email = "";
                    }
                    else
                    {
                        oComprobante.enviar_automaticamente_al_cliente = true;
                        oComprobante.cliente_email = cCorreo;
                    }
                    

                    if (message.Dvm_nDiasCredito > 0)
                        oComprobante.condiciones_de_pago = "CREDITO " + General.CE(message.Dvm_nDiasCredito) + " DIAS";

                    if (message.Dvm_nDiasCredito > 0)
                        oComprobante.medio_de_pago = "venta_al_credito";
                    else
                        oComprobante.medio_de_pago = message.MedioPago;
                        

                    oComprobante.placa_vehiculo = "";
                    oComprobante.orden_compra_servicio = "";
                    oComprobante.formato_de_pdf = "TICKET";
                }

                #endregion

                //---------------------------------------------------------------
                //------------------------ DETALLE -----------------------------
                //---------------------------------------------------------------

                #region Detalle


                VTD_DOC_VENTA_Entity oEntidadDetalle = new VTD_DOC_VENTA_Entity();

                oEntidadDetalle.Accion = "BUSCARTODOS";
                oEntidadDetalle.Emp_cCodigo = cEmpresa;
                oEntidadDetalle.Pan_cAnio = cAnio;
                oEntidadDetalle.Dvm_cNumMov = cNummov;

                string resultDetalle = ProcedimientosAPI.GetPostBuscarVentas_DET(oEntidadDetalle);
                DataTable dtDetalle = new DataTable();

                dtDetalle = JsonConvert.DeserializeObject<DataTable>(resultDetalle);
                dtDetalle.TableName = "Detalle";

                //--------------------------

                oComprobante.items = new List<Items>();


                foreach (System.Data.DataRow oRow in dtDetalle.Rows)
                {

                    Items oDetalle = new Items();

                    oDetalle.unidad_de_medida = "NIU";
                    oDetalle.codigo = "001";
                    oDetalle.codigo_producto_sunat = "10000000";

                    oDetalle.descripcion = General.CE(oRow["Producto"]);

                    decimal nCantidad = Convert.ToDecimal(oRow["Dvd_nCantidad"]);

                    if (cMoneda == "038")
                    {
                        oDetalle.subtotal = Convert.ToDecimal(oRow["BASE_MN"]);
                        oDetalle.igv = Convert.ToDecimal(oRow["IGV_MN"]);
                        oDetalle.total = Convert.ToDecimal(oRow["TOTAL_MN"]);
                        oDetalle.valor_unitario = Convert.ToDecimal(oRow["VU_MN"]);
                        oDetalle.precio_unitario = Convert.ToDecimal(oRow["PU_MN"]);
                    }
                    else
                    {
                        oDetalle.subtotal = Convert.ToDecimal(oRow["BASE"]);
                        oDetalle.igv = Convert.ToDecimal(oRow["IGV"]);
                        oDetalle.total = Convert.ToDecimal(oRow["TOTAL"]);
                        oDetalle.valor_unitario = Convert.ToDecimal(oRow["VU"]);
                        oDetalle.precio_unitario = Convert.ToDecimal(oRow["PU"]);
                    }

                    oDetalle.cantidad = nCantidad;
                    oDetalle.descuento = 0;
                    oDetalle.tipo_de_igv = 1;

                    oDetalle.anticipo_regularizacion = false;
                    oDetalle.anticipo_documento_serie = "";
                    oDetalle.anticipo_documento_numero = null;

                    oComprobante.items.Add(oDetalle);
                }

                #endregion

                //---------------------------------------------------------------
                //------------------------ COUTAS -----------------------------
                //---------------------------------------------------------------

                #region Cuotas

                VTM_DOC_VENTA_CUOTAS_Entity oEntidadCuota = new VTM_DOC_VENTA_CUOTAS_Entity();

                oEntidadCuota.Accion = "BUSCARTODOS";
                oEntidadCuota.Emp_cCodigo = cEmpresa;
                oEntidadCuota.Pan_cAnioVT = cAnio;
                oEntidadCuota.Dvm_cNumMov = cNummov;

                string resultCuota = ProcedimientosAPI.GetPostBuscarVentas_CUOTAS(oEntidadCuota);
                DataTable dtDetalleCouta = new DataTable();

                dtDetalleCouta = JsonConvert.DeserializeObject<DataTable>(resultCuota);
                dtDetalleCouta.TableName = "Cuotas";

                //-----------------------------------------------------------------------------------------

                oComprobante.venta_al_credito = new List<Cuotas>();

                foreach (System.Data.DataRow oRow in dtDetalleCouta.Rows)
                {

                    Cuotas oDetalleCuota = new Cuotas();

                    oDetalleCuota.cuota = Convert.ToInt16(oRow["Ccu_nCorrel"]);
                    oDetalleCuota.fecha_de_pago = General.FE(oRow["Ccu_dFechaVenc"]);
                    oDetalleCuota.importe = Convert.ToDecimal(oRow["Ccu_nMontoCuota"]);

                    oComprobante.venta_al_credito.Add(oDetalleCuota);
                }

                #endregion

                //---------------------------------------------------------------
                //----------------------- ENVIA JSON ----------------------------
                //---------------------------------------------------------------

                #region Envia Json

                
                string json = JsonConvert.SerializeObject(oComprobante, Formatting.Indented);

                byte[] bytes = Encoding.Default.GetBytes(json);
                string json_en_utf_8 = Encoding.UTF8.GetString(bytes);

                string json_de_respuesta = SendJson(url, json_en_utf_8, token);
                Respuesta r = JsonConvert.DeserializeObject<Respuesta>(json_de_respuesta);
                string r2 = JsonConvert.SerializeObject(r, Formatting.Indented);
                Respuesta json_r_in = JsonConvert.DeserializeObject<Respuesta>(r2);

                #endregion

                //---------------------------------------------------------------
                //----------------------- RESPUESTA -----------------------------
                //---------------------------------------------------------------

                NubeFact_Respuesta_Generacion leer_respuesta = JsonConvert.DeserializeObject<NubeFact_Respuesta_Generacion>(json_de_respuesta);

                Console.WriteLine(json_r_in);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("TIPO: " + leer_respuesta.tipo_de_comprobante);
                Console.WriteLine("SERIE: " + leer_respuesta.serie);
                Console.WriteLine("NUMERO: " + leer_respuesta.numero);
                Console.WriteLine("URL: " + leer_respuesta.enlace);
                Console.WriteLine("ACEPTADA_POR_SUNAT: " + leer_respuesta.aceptada_por_sunat);
                Console.WriteLine("DESCRIPCION SUNAT: " + leer_respuesta.sunat_description);
                Console.WriteLine("NOTA SUNAT: " + leer_respuesta.sunat_note);
                Console.WriteLine("CODIGO RESPUESTA SUNAT: " + leer_respuesta.sunat_responsecode);
                Console.WriteLine("SUNAT ERROR SOAP: " + leer_respuesta.sunat_soap_error);
                Console.WriteLine("PDF EN BASE64: " + leer_respuesta.enlace_del_pdf);
                Console.WriteLine("XML EN BASE64: " + leer_respuesta.enlace_del_xml);
                Console.WriteLine("CDR EN BASE64: " + leer_respuesta.enlace_del_cdr);
                Console.WriteLine("CODIGO QR: " + leer_respuesta.cadena_para_codigo_qr);
                Console.WriteLine("CODIGO HASH: " + leer_respuesta.codigo_hash);
                Console.WriteLine("CODIGO DE BARRAS: " + leer_respuesta.codigo_de_barras);
                Console.WriteLine("ERROR: " + leer_respuesta.errors);

                if (!String.IsNullOrEmpty(leer_respuesta.errors))
                    result = "Error: " + leer_respuesta.errors;
                else
                    result = leer_respuesta.enlace_del_pdf;

                //---------------------------------------------------------------
                //-------------------- ACTUALIZA ESTADO -------------------------
                //---------------------------------------------------------------

                Estado_Documento oEstado = new Estado_Documento();
                oEstado.cStatVtaElect="OK";
                oEstado.cValResElect = leer_respuesta.codigo_hash;


                if (leer_respuesta.aceptada_por_sunat== true)

                    ProcedimientosAPI.GetPostEditaEstadoVenta(oEstado, cEmpresa,cAnio, cNummov, Estado_Sunat.Aceptado);
                else
                    ProcedimientosAPI.GetPostEditaEstadoVenta(oEstado, cEmpresa, cAnio, cNummov, Estado_Sunat.Rechazado);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return result;
        }

        static string SendJson(string ruta, string json, string token)
        {
            try
            {
                using (var client = new WebClient())
                {
                    /// ESPECIFICAMOS EL TIPO DE DOCUMENTO EN EL ENCABEZADO
                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                    /// ASI COMO EL TOKEN UNICO
                    client.Headers[HttpRequestHeader.Authorization] = "Token token=" + token;
                    /// OBTENEMOS LA RESPUESTA
                    string respuesta = client.UploadString(ruta, "POST", json);
                    /// Y LA 'RETORNAMOS'
                    return respuesta;
                }
            }
            catch (WebException ex)
            {
                /// EN CASO EXISTA ALGUN ERROR, LO TOMAMOS
                var respuesta = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                /// Y LO 'RETORNAMOS'
                return respuesta;
            }
        }


   


    }


}

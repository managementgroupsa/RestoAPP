using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ventas.Models
{
    #region Comprobante



    public class NubeFact_Invoice
    {
        public string operacion { get; set; }
        public int tipo_de_comprobante { get; set; }
        public string serie { get; set; }
        public int numero { get; set; }
        public int sunat_transaction { get; set; }
        public string cliente_tipo_de_documento { get; set; }
        public string cliente_numero_de_documento { get; set; }
        public string cliente_denominacion { get; set; }
        public string cliente_direccion { get; set; }
        public string cliente_email { get; set; }
        public string cliente_email_1 { get; set; }
        public string cliente_email_2 { get; set; }
        public DateTime fecha_de_emision { get; set; }
        public DateTime fecha_de_vencimiento { get; set; }
        public int moneda { get; set; }
        public dynamic tipo_de_cambio { get; set; }
        public decimal porcentaje_de_igv { get; set; }
        public dynamic descuento_global { get; set; }
        public dynamic total_descuento { get; set; }
        public dynamic total_anticipo { get; set; }
        public dynamic total_gravada { get; set; }
        public dynamic total_inafecta { get; set; }
        public dynamic total_exonerada { get; set; }
        public decimal total_igv { get; set; }
        public dynamic total_gratuita { get; set; }
        public dynamic total_otros_cargos { get; set; }
        public decimal total_isc { get; set; }
        public decimal total { get; set; }
        public dynamic percepcion_tipo { get; set; }
        public dynamic percepcion_base_imponible { get; set; }
        public dynamic total_percepcion { get; set; }
        public dynamic total_incluido_percepcion { get; set; }
        public dynamic retencion_tipo { get; set; }
        public dynamic retencion_base_imponible { get; set; }
        public dynamic total_retencion { get; set; }
        public dynamic total_impuestos_bolsas { get; set; }
        public bool detraccion { get; set; }
        public string observaciones { get; set; }
        public dynamic documento_que_se_modifica_tipo { get; set; }
        public string documento_que_se_modifica_serie { get; set; }
        public dynamic documento_que_se_modifica_numero { get; set; }
        public dynamic tipo_de_nota_de_credito { get; set; }
        public dynamic tipo_de_nota_de_debito { get; set; }
        public bool enviar_automaticamente_a_la_sunat { get; set; }
        public bool enviar_automaticamente_al_cliente { get; set; }
        public string codigo_unico { get; set; }
        public string condiciones_de_pago { get; set; }
        public string medio_de_pago { get; set; }
        public string placa_vehiculo { get; set; }
        public string orden_compra_servicio { get; set; }
        public string detraccion_tipo { get; set; }
        public decimal detraccion_total { get; set; }
        public decimal detraccion_porcentaje { get; set; }
        public decimal medio_de_pago_detraccion { get; set; }
        public dynamic ubigeo_origen { get; set; }
        public string direccion_origen { get; set; }
        public dynamic ubigeo_destino { get; set; }
        public string direccion_destino { get; set; }
        public string detalle_viaje { get; set; }
        public dynamic val_ref_serv_trans { get; set; }
        public dynamic val_ref_carga_efec { get; set; }
        public dynamic val_ref_carga_util { get; set; }

        //-----------------------------------------
        public string formato_de_pdf { get; set; }
        public bool generado_por_contingencia { get; set; }
        public bool bienes_region_selva { get; set; }
        public bool servicios_region_selva { get; set; }

        //-----------------------------------------
        public List<Items> items { get; set; }
        public List<Guias> guias { get; set; }

        public List<Cuotas> venta_al_credito { get; set; }
    }

    public class Items
    {
        public string unidad_de_medida { get; set; }
        public string codigo { get; set; }
        public string codigo_producto_sunat { get; set; }
        public string descripcion { get; set; }
        public decimal cantidad { get; set; }
        public decimal valor_unitario { get; set; }
        public decimal precio_unitario { get; set; }
        public dynamic descuento { get; set; }
        public decimal subtotal { get; set; }
        public int tipo_de_igv { get; set; }
        public decimal igv { get; set; }
        public decimal impuesto_bolsas { get; set; }
        public decimal total { get; set; }
        public bool anticipo_regularizacion { get; set; }
        public dynamic anticipo_documento_serie { get; set; }
        public dynamic anticipo_documento_numero { get; set; }
    }

    public class Guias
    {
        public int guia_tipo { get; set; }
        public string guia_serie_numero { get; set; }

    }

    public class Cuotas
    {
        public int cuota { get; set; }
        public DateTime fecha_de_pago { get; set; }
        public decimal importe { get; set; }

    }

    public class NubeFact_Consulta_Generacion
    {
        public string operacion { get; set; }
        public int tipo_de_comprobante { get; set; }
        public string serie { get; set; }
        public int numero { get; set; }
    }

    public class NubeFact_Respuesta_Generacion
    {
        public int tipo_de_comprobante { get; set; }
        public string serie { get; set; }
        public int numero { get; set; }
        public string enlace { get; set; }
        public bool aceptada_por_sunat { get; set; }
        public string sunat_description { get; set; }
        public string sunat_note { get; set; }
        public string sunat_responsecode { get; set; }
        public string sunat_soap_error { get; set; }
        public string cadena_para_codigo_qr { get; set; }
        public string codigo_hash { get; set; }
        public string codigo_de_barras { get; set; }
        public string enlace_del_pdf { get; set; }
        public string enlace_del_xml { get; set; }
        public string enlace_del_cdr { get; set; }

        public string errors { get; set; }
    }

    public class Respuesta
    {
        public string errors { get; set; }
        public int tipo { get; set; }
        public string serie { get; set; }
        public int numero { get; set; }
        public string url { get; set; }
        public bool aceptada_por_sunat { get; set; }
        public string sunat_description { get; set; }
        public string sunat_note { get; set; }
        public string sunat_responsecode { get; set; }
        public string sunat_soap_error { get; set; }
        public string pdf_zip_base64 { get; set; }
        public string xml_zip_base64 { get; set; }
        public string cdr_zip_base64 { get; set; }
        public string cadena_para_codigo_qr { get; set; }
        public string codigo_hash { get; set; }
        public string codigo_de_barras { get; set; }
    }

    #endregion

    #region Anulacion

    public class NubeFact_Anulacion_Envio
    {
        public string operacion { get; set; }
        public int tipo_de_comprobante { get; set; }
        public string serie { get; set; }
        public int numero { get; set; }
        public string motivo { get; set; }
        public string codigo_unico { get; set; }
    }

    public class NubeFact_Consulta_Anulacion
    {
        public string operacion { get; set; }
        public int tipo_de_comprobante { get; set; }
        public string serie { get; set; }
        public int numero { get; set; }
    }

    public class NubeFact_Respuesta_Anulacion
    {

        public int numero { get; set; }
        public string enlace { get; set; }
        public string sunat_ticket_numero { get; set; }
        public bool aceptada_por_sunat { get; set; }
        public string sunat_description { get; set; }
        public string sunat_note { get; set; }
        public string sunat_responsecode { get; set; }
        public string sunat_soap_error { get; set; }
        public string enlace_del_pdf { get; set; }
        public string enlace_del_xml { get; set; }
        public string enlace_del_cdr { get; set; }
    }

    #endregion
}

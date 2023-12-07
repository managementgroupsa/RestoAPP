using System;
using System.ComponentModel.DataAnnotations;

namespace Ventas.Models
{
    public class DIA_Entity
    {
        public DateTime Fecha { get; set; }
        public double Compra { get; set; }
        public double Venta { get; set; }
    }


    public class SUNAT_Entity
    {
        [MaxLength(1000)]
        public string nombres { get; set; }
        [MaxLength(10)]
        public string ubigeo { get; set; }
        [MaxLength(20)]
        public string ruc { get; set; }
        [MaxLength(10)]
        public string tipoDocumento { get; set; }
        [MaxLength(1000)]
        public string apePaterno { get; set; }
        [MaxLength(1000)]
        public string direccion { get; set; }
        [MaxLength(100)]
        public string telefono { get; set; }
        [MaxLength(1000)]
        public string tipoEmpresa { get; set; }
        [MaxLength(1000)]
        public string razonSocial { get; set; }
        [MaxLength(1000)]
        public string nomComercial { get; set; }
        [MaxLength(100)]
        public string estado { get; set; }
        [MaxLength(100)]
        public string condiContribuyente { get; set; }
        [MaxLength(100)]
        public string fechaBaja { get; set; }
        [MaxLength(100)]
        public string inicioActi { get; set; }
        [MaxLength(100)]
        public string fIncripcion { get; set; }
        [MaxLength(100)]
        public string emiComprobante { get; set; }
        [MaxLength(100)]
        public string sisContabilidad { get; set; }
        [MaxLength(100)]
        public string comerExterior { get; set; }
        [MaxLength(100)]
        public string actiEconomica { get; set; }
        [MaxLength(100)]
        public string oficio { get; set; }
        [MaxLength(500)]
        public string departamento { get; set; }
        [MaxLength(500)]
        public string provincia { get; set; }
        [MaxLength(500)]
        public string distrito { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ventas.Models
{
    public class EMPRESA_Entity
    {
        [MaxLength(20)]
        public string Accion { get; set; }
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(80)]
        public string Emp_cNombreLargo { get; set; }
        [MaxLength(20)]
        public string Emp_cNombreCorto { get; set; }
        [MaxLength(80)]
        public string Emp_cDireccion { get; set; }
        [MaxLength(15)]
        public string Emp_cNumRuc { get; set; }
        [MaxLength(1)]
        public string Emp_cEstado { get; set; }
        [MaxLength(50)]
        public string Emp_cCargo2 { get; set; }
        [MaxLength(50)]
        public string Emp_cRepres2 { get; set; }
        [MaxLength(50)]
        public string Emp_cCargo1 { get; set; }
        [MaxLength(40)]
        public string Emp_cRepres1 { get; set; }
        [MaxLength(100)]
        public string Emp_cMail { get; set; }
        [MaxLength(30)]
        public string Emp_cFax { get; set; }
        [MaxLength(30)]
        public string Emp_cTelefono { get; set; }
        [MaxLength(20)]
        public string Emp_cUser { get; set; }
        public DateTime Emp_dFechaCrea { get; set; }
        [MaxLength(3)]
        public string Emp_cCodSuc { get; set; }
        [MaxLength(1)]
        public string Emp_Bymoneda { get; set; }
        [MaxLength(5000)]
        public string cOtrosAtri { get; set; }
        [MaxLength(1)]
        public string Emp_cEsAgenteRetencion { get; set; }
        public decimal Emp_nRetencionPorcentaje { get; set; }
        [MaxLength(20)]
        public string Emp_cDetraccionCuenta { get; set; }
        [MaxLength(3)]
        public string Emp_cDetraccionCodigo { get; set; }
        public decimal Emp_nDetraccionPorcentaje { get; set; }
        [MaxLength(1)]
        public string Emp_cMensajeAgenteRet { get; set; }

    }
}

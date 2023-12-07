using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ventas.Models
{
    public class VTM_DOC_VENTA_CUOTAS_Entity
    {
        [MaxLength(20)]
        public string Accion { get; set; }
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        public int Ccu_nCorrel { get; set; }
        [MaxLength(4)]
        public string Pan_cAnioVT { get; set; }
        [MaxLength(10)]
        public string Dvm_cNumMov { get; set; }
        public DateTime Ccu_dFechaVenc { get; set; }
        [MaxLength(3)]
        public string Ccu_cMon { get; set; }
        public decimal Ccu_nMontoCuota { get; set; }
    }
}

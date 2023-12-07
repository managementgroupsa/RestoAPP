using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ventas.Models
{
    public class CNT_TIPO_CAMBIO_Entity
    {
        [MaxLength(20)]
        public string Accion { get; set; }
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(10)]
        public string Tca_dFecha { get; set; }
        
        [MaxLength(3)]
        public string Tca_cCodigoOrigen { get; set; }
        [MaxLength(3)]
        public string Tca_cCodigoDestino { get; set; }
        public decimal Tca_nCompra { get; set; }
        public decimal Tca_nVenta { get; set; }
        public decimal Tca_nCompraP { get; set; }
        public decimal Tca_nVentaP { get; set; }
        [MaxLength(20)]
        public string Tca_cUserCrea { get; set; }
        [MaxLength(2)]
        public string Tca_cPeriodo { get; set; }
    }
}

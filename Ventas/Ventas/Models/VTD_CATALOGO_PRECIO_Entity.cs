using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace RestoAPP.Models
{
    public class VTD_CATALOGO_PRECIO_Entity
    {
        [MaxLength(20)]
        public string @Accion { get; set; }
        [MaxLength(3)]
        public string @Emp_cCodigo { get; set; }
        [MaxLength(6)]
        public string @Cab_cCatalogo { get; set; }
        [MaxLength(2)]
        public string @Tpr_cCodigo { get; set; }
        [MaxLength(5)]
        public string @Cpr_cCorrelativo { get; set; }
        [MaxLength(3)]
        public string @Mon_cCodigo { get; set; }
        public decimal @Cpr_nPrecio { get; set; }
        public decimal @Cpr_nPrecioMin { get; set; }
        public decimal @Cpr_nPrecioMax { get; set; }
        public DateTime @Cpr_dFechaVigIni { get; set; }
        [MaxLength(1)]
        public string @Cpr_cFlgIncImpto { get; set; }
        [MaxLength(1)]
        public string @Cpr_cEstado { get; set; }
        [MaxLength(20)]
        public string @Cpr_cUser { get; set; }
        public decimal @nTipoCambio { get; set; }

        [Display(Name = "NoParameter")]
        [MaxLength(50)]
        public string @TipoPrecio { get; set; }

        [Display(Name = "NoParameter")]
        [MaxLength(100)]
        public string @Producto { get; set; }

        [Display(Name = "NoParameter")]
        [MaxLength(10)]
        public string @Moneda { get; set; }


        public DateTime @Cpr_dFechaVigFin { get; set; }

    }
}

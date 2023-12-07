using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Ventas.Models
{
    public class TABLA_Entity
    {
        [MaxLength(50)]
        public string Accion { get; set; }
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(3)]
        public string Tab_cTabla { get; set; }
        [MaxLength(4)]
        public string Tab_cCodigo { get; set; }
        [MaxLength(255)]
        public string Tab_cDescripCampo { get; set; }

        [MaxLength(40)]
        public string Tab_cDescripTabla { get; set; }
        public decimal Tab_nLongitud { get; set; }
        [MaxLength(6)]
        public string Tab_cCodSunat { get; set; }
        [MaxLength(1)]
        public string Tab_cEstado { get; set; }
        [MaxLength(20)]
        public string Tab_cUser { get; set; }
        [MaxLength(250)]
        public string Tab_cValor { get; set; }

    }
}

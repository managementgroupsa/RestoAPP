using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Ventas.Models
{
    public class LGT_CATEGORIA_Entity
    {
        [MaxLength(20)]
        public string Accion { get; set; }
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(2)]
        public string Aca_cCategoria { get; set; }
        [MaxLength(120)]
        public string Aca_cDescripLarga { get; set; }
        [MaxLength(40)]
        public string Aca_cDescripCorta { get; set; }
        [MaxLength(1)]
        public string Aca_cEstado { get; set; }
        [MaxLength(20)]
        public string Aca_cUser { get; set; }

    }
}

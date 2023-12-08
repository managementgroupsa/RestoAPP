using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestoAPP.Models
{
    public class VTT_TIPO_PRECIO_Entity
    {
        [MaxLength(20)]
        public string @Accion { get; set; }
        [MaxLength(3)]
        public string @Emp_cCodigo { get; set; }
        [MaxLength(2)]
        public string @Tpr_cCodigo { get; set; }
        [MaxLength(50)]
        public string @Tpr_cDescripcion { get; set; }
        [MaxLength(1)]
        public string @Tpr_cEstado { get; set; }
        [MaxLength(20)]
        public string @Tpr_cUser { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace RestoAPP.Models
{
    public class LGT_CLASE_BS_Entity
    {
        [MaxLength(40)]
        public string Accion { get; set; }
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(2)]
        public string Aca_cCategoria { get; set; }
        [MaxLength(5)]
        public string Gru_cGrupo { get; set; }
        [MaxLength(4)]
        public string Cla_cClase { get; set; }
        [MaxLength(120)]
        public string Cla_cDescripLarga { get; set; }
        [MaxLength(40)]
        public string Cla_cDescripCorta { get; set; }
        [MaxLength(1)]
        public string Cla_cEstado { get; set; }
        [MaxLength(20)]
        public string Cla_cUser { get; set; }

    }
}

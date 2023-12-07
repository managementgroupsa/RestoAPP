using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Ventas.Models
{
    public class LGT_GRUPO_BS_Entity
    {
        [MaxLength(20)]
        public string Accion { get; set; }
        [MaxLength(length: 3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(length: 100)]
        public string Aca_cCategoria { get; set; }
        [MaxLength(5)]
        public string Gru_cGrupo { get; set; }
        [MaxLength(120)]
        public string Gru_cDescripLarga { get; set; }
        [MaxLength(40)]
        public string Gru_cDescripCorta { get; set; }
        [MaxLength(1)]
        public string Gru_cEstado { get; set; }
        [MaxLength(20)]
        public string Gru_cUser { get; set; }

    }
}

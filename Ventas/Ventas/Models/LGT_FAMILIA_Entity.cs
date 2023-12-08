using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace RestoAPP.Models
{
    public class LGT_FAMILIA_Entity
    {
        [MaxLength(20)]
        public string Accion { get; set; }
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(2)]
        public string Aca_cCategoria { get; set; }
        [MaxLength(5)]
        public string Gru_cGrupo { get; set; }
        [MaxLength(4)]
        public string Cla_cClase { get; set; }
        [MaxLength(7)]
        public string Fam_cFamilia { get; set; }
        [MaxLength(500)]
        public string Fam_cDescripLarga { get; set; }
        [MaxLength(500)]
        public string Fam_cDescripCorta { get; set; }
        [MaxLength(1)]
        public string Fam_cEstado { get; set; }
        [MaxLength(20)]
        public string Fam_cUser { get; set; }
        [MaxLength(1)]
        public string ImportarExcel { get; set; }
        [MaxLength(1)]
        public string FlgVerificaFam { get; set; }

    }
}

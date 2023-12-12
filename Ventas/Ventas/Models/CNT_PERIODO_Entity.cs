using System.ComponentModel.DataAnnotations;

namespace RestoPLUS.Models
{
    public class CNT_PERIODO_Entity
    {
        [MaxLength(20)]
        public string Accion { get; set; }
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(4)]
        public string Pan_cAnio { get; set; }
        [MaxLength(2)]
        public string Per_cPeriodo { get; set; }
        [MaxLength(40)]
        public string Per_cDescripPeriodo { get; set; }
        [MaxLength(20)]
        public string Per_cUser { get; set; }
        [MaxLength(20)]
        public string Per_cUserModifica { get; set; }
    }
}

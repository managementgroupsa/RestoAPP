using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestoPLUS.Models
{
    public class VTM_RESTO_APERTURA_Entity
    {
        [MaxLength(20)]
        public string Accion { get; set; }
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(4)]
        public string Pan_cAnio { get; set; }
        [MaxLength(10)]
        public string Res_cNummov { get; set; }
        [MaxLength(5)]
        public string Pvt_cCodigo { get; set; }
        [MaxLength(2)]
        public string Per_cPeriodo { get; set; }

        public DateTime Ape_dFecha { get; set; }
        [MaxLength(1)]
        public string Ape_cEstado { get; set; }
        [MaxLength(1000)]
        public string Ape_cComentario { get; set; }
        [MaxLength(20)]
        public string Ape_cUser { get; set; }


    }

}


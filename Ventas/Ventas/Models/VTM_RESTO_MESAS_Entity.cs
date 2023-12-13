using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestoPLUS.Models
{
    public class VTM_RESTO_MESAS_Entity
    {
        [MaxLength(20)]
        public string Accion { get; set; }
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(5)]
        public string Pvt_cCodigo { get; set; }
        [MaxLength(5)]
        public string Mes_cCodigo { get; set; }
        [MaxLength(100)]
        public string Mes_cDescripcion { get; set; }
        [MaxLength(1)]
        public string Mes_cEstado { get; set; }
        [MaxLength(20)]
        public string Mes_cUser { get; set; }

        [Display(Name = "NoParameter")]
        [MaxLength(100)]
        public string Ent_cPersona { get; set; }


        [Display(Name = "NoParameter")]
        [MaxLength(20)]
        public string Ape_cUserCrea { get; set; }

        [Display(Name = "NoParameter")]
        [MaxLength(10)]
        public string Res_cNummov { get; set; }

        [Display(Name = "NoParameter")]
        [MaxLength(10)]
        public string Ped_cNummov { get; set; }

        [Display(Name = "NoParameter")]
        public DateTime Ape_dFechaCrea { get; set; }

    }



}


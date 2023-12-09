using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestoAPP.Models
{
    public class VTD_RESTO_APERTURA_PEDIDO_Entity
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
        [MaxLength(10)]
        public string Ped_cNummov { get; set; }

        public int Ped_nItem { get; set; }
        [MaxLength(6)]
        public string Cab_cCatalogo { get; set; }
        public int Ped_nCantidad { get; set; }
        [MaxLength(1000)]
        public string Ped_cComentario { get; set; }
        [MaxLength(1)]
        public string Ped_cEstado { get; set; }
        [MaxLength(20)]
        public string Ped_cUser { get; set; }

        [Display(Name = "NoParameter")]
        [MaxLength(1000)]
        public string Cab_cDescripLarga { get; set; }

        [Display(Name = "NoParameter")]
        [MaxLength(100)]
        public string Ent_cPersona { get; set; }

        [Display(Name = "NoParameter")]
        [MaxLength(100)]
        public string Mes_cDescripcion { get; set; }


        [Display(Name = "NoParameter")]
        [MaxLength(5)]
        public string Mes_cCodigo { get; set; }

        [Display(Name = "NoParameter")]
        [MaxLength(20)]
        public string Ape_cUserCrea { get; set; }


    }


}


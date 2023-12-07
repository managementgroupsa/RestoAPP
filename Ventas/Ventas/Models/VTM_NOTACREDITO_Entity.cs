using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Ventas.Models
{
    public class VTM_NOTACREDITO_Entity
    {
        [MaxLength(20)]
        public string Accion { get; set; }
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(4)]
        public string Pan_cAnio { get; set; }
        [MaxLength(10)]
        public string Ncm_cNumMov { get; set; }
        [MaxLength(5)]
        public string Pvt_cCodigo { get; set; }
        [MaxLength(2)]
        public string Ncm_cTipoDoc { get; set; }
        [MaxLength(4)]
        public string Ncm_cSerieDoc { get; set; }
        [MaxLength(8)]
        public string Ncm_cNumDoc { get; set; }
        [MaxLength(1)]
        public string Cli_cTipoEntidad { get; set; }
        [MaxLength(5)]
        public string Cli_cCodigo { get; set; }
        [MaxLength(2)]
        public string Ncm_cDirFactura { get; set; }
        [MaxLength(4)]
        public string Pan_cAnioDV { get; set; }
        [MaxLength(10)]
        public string Dvm_cNumMov { get; set; }
        [MaxLength(2)]
        public string Per_cPeriodo { get; set; }
        public DateTime Ncm_dFechaEmision { get; set; }
        [MaxLength(3)]
        public string Mon_cCodigo { get; set; }
        public decimal Ncm_nTipoCambio { get; set; }
        [MaxLength(400)]
        public string Ncm_cComentarioGral { get; set; }
        public decimal Ncm_nSubTotal { get; set; }
        public decimal Ncm_nTotalImpto1 { get; set; }
        public decimal Ncm_nTotalImpto2 { get; set; }
        public decimal Ncm_nTotalImpto3 { get; set; }
        public decimal Ncm_nTotalImpto4 { get; set; }
        public decimal Ncm_nTotalImpto5 { get; set; }
        public decimal Ncm_nTotal { get; set; }
        public decimal Ncm_nSubTotal_MN { get; set; }
        public decimal Ncm_nTotalImpto1_MN { get; set; }
        public decimal Ncm_nTotalImpto2_MN { get; set; }
        public decimal Ncm_nTotalImpto3_MN { get; set; }
        public decimal Ncm_nTotalImpto4_MN { get; set; }
        public decimal Ncm_nTotalImpto5_MN { get; set; }
        public decimal Ncm_nTotal_MN { get; set; }
        [MaxLength(1)]
        public string Ncm_cFlgCatalog { get; set; }
        [MaxLength(1)]
        public string Ncm_cNacExt { get; set; }
        [MaxLength(1)]
        public string Ncm_cBienServ { get; set; }
        [MaxLength(1)]
        public string Ncm_cEstado { get; set; }
        [MaxLength(20)]
        public string Ncm_cUser { get; set; }
        public DateTime Ncm_dFechaEmisionFin { get; set; }
        [MaxLength(1)]
        public string Ncm_cTipoNC { get; set; }
        [MaxLength(2)]
        public string Ncm_cRefTipoDoc { get; set; }
        [MaxLength(4)]
        public string Ncm_cRefSerie { get; set; }
        [MaxLength(8)]
        public string Ncm_cRefNumero { get; set; }
        [MaxLength(400)]
        public string Ncm_cOtros { get; set; }
        public DateTime ncm_dfecharef { get; set; }
        [MaxLength(3)]
        public string ncm_IncluyeImpto { get; set; }
        [MaxLength(4)]
        public string Pan_cAnio_Doc { get; set; }

        [Display(Name = "NoParameter")]
        [MaxLength(100)]
        public string TipoDoc { get; set; }

        [Display(Name = "NoParameter")]
        [MaxLength(500)]
        public string Cliente { get; set; }

        [Display(Name = "NoParameter")]
        [MaxLength(50)]
        public string ValidaSunat { get; set; }

        [Display(Name = "NoParameter")]
        [MaxLength(50)]
        public string TipoPrecio { get; set; }

        [Display(Name = "NoParameter")]
        [MaxLength(10)]
        public string Moneda { get; set; }

        [Display(Name = "NoParameter")]
        public decimal ImporteTotal { get; set; }

    }
}

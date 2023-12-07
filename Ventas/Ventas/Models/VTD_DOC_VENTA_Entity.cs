using System.ComponentModel.DataAnnotations;

namespace Ventas.Models
{
    public class VTD_DOC_VENTA_Entity
    {
        [MaxLength(20)]
        public string Accion { get; set; }
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(4)]
        public string Pan_cAnio { get; set; }
        [MaxLength(10)]
        public string Dvm_cNumMov { get; set; }
        public int Dvd_nItem { get; set; }
        [MaxLength(1)]
        public string Dvd_cFlgCatalog { get; set; }
        [MaxLength(10)]
        public string Cab_cCatalogo { get; set; }
        [MaxLength(3)]
        public string Dvd_cUnidad { get; set; }
        [MaxLength(-1)]
        public string Dvd_cNoCatalog { get; set; }
        public decimal Dvd_nCantidad { get; set; }
        public decimal Dvd_nTasaImpto1 { get; set; }
        public decimal Dvd_nTasaImpto2 { get; set; }
        public decimal Dvd_nTasaImpto3 { get; set; }
        public decimal Dvd_nTasaImpto4 { get; set; }
        public decimal Dvd_nTasaImpto5 { get; set; }
        [MaxLength(1)]
        public string Dvd_cFlgPrecioFijo { get; set; }
        public decimal Dvd_nPrecioUnitario { get; set; }
        public decimal Dvd_nDsctoLinea { get; set; }
        public decimal Dvd_nDsctoGral { get; set; }
        public decimal Dvd_nPrecioNeto { get; set; }
        public decimal Dvd_nImporte { get; set; }
        public decimal Dvd_nPrecioUnitario_MN { get; set; }
        public decimal Dvd_nPrecioNeto_MN { get; set; }
        public decimal Dvd_nImporte_MN { get; set; }
        [MaxLength(4)]
        public string Pro_cCodigo { get; set; }
        [MaxLength(1)]
        public string FlgAfecto { get; set; }
        [MaxLength(12)]
        public string Dvd_cCentroCosto { get; set; }
        [MaxLength(1)]
        public string Dvd_cFlgCCosto { get; set; }
        [MaxLength(1)]
        public string Dvd_FlagIncImp { get; set; }
        [MaxLength(1)]
        public string Dvd_FlagBienServ { get; set; }
        [MaxLength(10)]
        public string Grm_cNumMov { get; set; }
        [MaxLength(4)]
        public string Grm_Anio { get; set; }
        [MaxLength(10)]
        public string Pdm_cNumMov { get; set; }
        [MaxLength(4)]
        public string Pdm_Anio { get; set; }
        [MaxLength(4000)]
        public string Dvd_Obs { get; set; }
        [MaxLength(10)]
        public string Pfm_cNumMovIE { get; set; }
        [MaxLength(4)]
        public string Pfm_AnioIE { get; set; }
        [MaxLength(8000)]
        public string Dvd_Ubicacion { get; set; }
        public decimal Dvd_nPiezas { get; set; }

        [Display(Name = "NoParameter")]
        [MaxLength(100)]
        public string Producto{ get; set; }

        [Display(Name = "NoParameter")]
        [MaxLength(100)]
        public string Unidad { get; set; }

        [Display(Name = "NoParameter")]
        public decimal BASE_MN { get; set; }

        [Display(Name = "NoParameter")]
        public decimal IGV_MN { get; set; }

        [Display(Name = "NoParameter")]
        public decimal TOTAL_MN { get; set; }

        [Display(Name = "NoParameter")]
        public decimal VU_MN { get; set; }

        [Display(Name = "NoParameter")]
        public decimal PU_MN { get; set; }

        [Display(Name = "NoParameter")]
        public decimal BASE { get; set; }

        [Display(Name = "NoParameter")]
        public decimal IGV { get; set; }

        [Display(Name = "NoParameter")]
        public decimal TOTAL { get; set; }

        [Display(Name = "NoParameter")]
        public decimal VU { get; set; }

        [Display(Name = "NoParameter")]
        public decimal PU { get; set; }

    }
}

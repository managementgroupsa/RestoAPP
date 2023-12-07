using System.ComponentModel.DataAnnotations;

namespace Ventas.Models
{
    public class VTD_NOTACREDITO_Entity
    {
        [MaxLength(20)]
        public string Accion { get; set; }
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(4)]
        public string Pan_cAnio { get; set; }
        [MaxLength(10)]
        public string Ncm_cNumMov { get; set; }
        public short Ncd_nItem { get; set; }
        [MaxLength(1)]
        public string Ncd_cFlgCatalog { get; set; }
        [MaxLength(6)]
        public string Cab_cCatalogo { get; set; }
        [MaxLength(3)]
        public string Ncd_cUnidad { get; set; }
        [MaxLength(400)]
        public string Ncd_cNoCatalog { get; set; }
        public float Ncd_nCantidad { get; set; }
        public decimal Ncd_nTasaImpto1 { get; set; }
        public decimal Ncd_nTasaImpto2 { get; set; }
        public decimal Ncd_nTasaImpto3 { get; set; }
        public decimal Ncd_nTasaImpto4 { get; set; }
        public decimal Ncd_nTasaImpto5 { get; set; }
        [MaxLength(1)]
        public string Ncd_cFlgPrecioFijo { get; set; }
        public decimal Ncd_nPrecioUnitario { get; set; }
        public decimal Ncd_nPrecioNeto { get; set; }
        public decimal Ncd_nImporte { get; set; }
        public decimal Ncd_nPrecioUnitario_MN { get; set; }
        public decimal Ncd_nPrecioNeto_MN { get; set; }
        public decimal Ncd_nImporte_MN { get; set; }
        [MaxLength(3)]
        public string Pro_cCodigo { get; set; }
        [MaxLength(1)]
        public string flgIncluyeImp { get; set; }
        [MaxLength(12)]
        public string Ncd_cCentroCosto { get; set; }
        public int Ncd_nItemRef { get; set; }
        public int Ncd_nPiezas { get; set; }

    }
}

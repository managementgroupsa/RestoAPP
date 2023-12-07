using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ventas.Models
{
    public class VTM_DOC_VENTA_Entity
    {
        [MaxLength(20)]
        public string Accion { get; set; }
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(4)]
        public string Pan_cAnio { get; set; }
        [MaxLength(10)]
        public string Dvm_cNumMov { get; set; }
        [MaxLength(5)]
        public string Pvt_cCodigo { get; set; }
        [MaxLength(5)]
        public string Alm_cAlmacen { get; set; }
        [MaxLength(2)]
        public string Dvm_cTipoDoc { get; set; }
        [MaxLength(4)]
        public string Dvm_cSerieDoc { get; set; }
        [MaxLength(10)]
        public string Dvm_cNumDoc { get; set; }
        [MaxLength(1)]
        public string Cli_cTipoEntidad { get; set; }
        [MaxLength(5)]
        public string Cli_cCodigo { get; set; }
        [MaxLength(2)]
        public string Dvm_cDirFactura { get; set; }
        [MaxLength(2)]
        public string Dvm_cDirEntrega { get; set; }
        [MaxLength(4)]
        public string Pan_cAnioPF { get; set; }
        [MaxLength(10)]
        public string Pfm_cNumMov { get; set; }
        [MaxLength(4)]
        public string Pan_cAnioPD { get; set; }
        [MaxLength(10)]
        public string Pdm_cNumMov { get; set; }
        [MaxLength(3)]
        public string Dvm_cCondicion { get; set; }
        public int Dvm_nDiasCredito { get; set; }
        [MaxLength(2)]
        public string Tpr_cCodigo { get; set; }
        public DateTime Dvm_dFechaEmision { get; set; }
        public DateTime Dvm_dFechaVenc { get; set; }
        public DateTime Dvm_dFechaCanc { get; set; }
        [MaxLength(3)]
        public string Mon_cCodigo { get; set; }
        public decimal Dvm_nTipoCambio { get; set; }
        [MaxLength(400)]
        public string Dvm_cComentarioGral { get; set; }
        [MaxLength(1)]
        public string Ven_cTipoEntidad { get; set; }
        [MaxLength(5)]
        public string Ven_cCodigo { get; set; }
        public decimal Dvm_nTDscto1 { get; set; }
        public decimal Dvm_nTDscto2 { get; set; }
        public decimal Dvm_nTDscto3 { get; set; }
        [MaxLength(400)]
        public string Dvm_cComentarioDscto { get; set; }
        public decimal Dvm_nSubTotal { get; set; }
        public decimal Dvm_nTotalImpto1 { get; set; }
        public decimal Dvm_nTotalImpto2 { get; set; }
        public decimal Dvm_nTotalImpto3 { get; set; }
        public decimal Dvm_nTotalImpto4 { get; set; }
        public decimal Dvm_nTotalImpto5 { get; set; }
        public decimal Dvm_nTotal { get; set; }
        public decimal Dvm_nSubTotal_MN { get; set; }
        public decimal Dvm_nTotalImpto1_MN { get; set; }
        public decimal Dvm_nTotalImpto2_MN { get; set; }
        public decimal Dvm_nTotalImpto3_MN { get; set; }
        public decimal Dvm_nTotalImpto4_MN { get; set; }
        public decimal Dvm_nTotalImpto5_MN { get; set; }
        public decimal Dvm_nTotal_MN { get; set; }
        [MaxLength(1)]
        public string Dvm_cFlgCatalog { get; set; }
        [MaxLength(1)]
        public string Dvm_cEstado { get; set; }
        [MaxLength(1)]
        public string Dvm_cNacExt { get; set; }
        [MaxLength(1)]
        public string Dvm_cBienServ { get; set; }
        [MaxLength(20)]
        public string Dvm_cUser { get; set; }
        [MaxLength(3)]
        public string Ase_cTipoVoucher { get; set; }
        [MaxLength(1)]
        public string Dvm_cDirectaAntic { get; set; }
        [MaxLength(1)]
        public string Dvm_cConSinDescAlm { get; set; }
        [MaxLength(2)]
        public string Dvm_cFormaPago { get; set; }
        [MaxLength(2)]
        public string Grm_cTipoDoc { get; set; }
        [MaxLength(4)]
        public string Grm_cSerieDoc { get; set; }
        [MaxLength(8)]
        public string Grm_cNumDoc { get; set; }
        [MaxLength(10)]
        public string Ctrl_cCodigo { get; set; }
        [MaxLength(10)]
        public string Doc_cNumMov { get; set; }
        [MaxLength(6)]
        public string Inm_cNumMov { get; set; }
        [MaxLength(10)]
        public string Inm_cPagoMov { get; set; }
        [MaxLength(50)]
        public string Dvm_Vapor { get; set; }
        [MaxLength(50)]
        public string Dvm_Naviera { get; set; }
        [MaxLength(100)]
        public string Dvm_PuertoEmb { get; set; }
        [MaxLength(100)]
        public string Dvm_PuertoDestino { get; set; }
        public decimal Dvm_PesoBruto { get; set; }
        public decimal Dvm_PesoNeto { get; set; }
        [MaxLength(1)]
        public string Dvm_IncluyeImpto { get; set; }
        [MaxLength(10)]
        public string Dvm_OrdenCompra { get; set; }
        [MaxLength(1)]
        public string Dvm_cDetraccion { get; set; }
        public decimal Dvm_nImpDetra { get; set; }
        [MaxLength(1)]
        public string ChkModificaIGV { get; set; }
        [MaxLength(5)]
        public string Veh_cCodigo { get; set; }
        [MaxLength(200)]
        public string Dvm_Transaccion { get; set; }
        [MaxLength(2)]
        public string Per_cPeriodo { get; set; }
        [MaxLength(400)]
        public string Pdm_cSiniestro { get; set; }
        [MaxLength(400)]
        public string Pdm_cPoliza { get; set; }
        [MaxLength(400)]
        public string Pdm_cCaso { get; set; }
        [MaxLength(400)]
        public string Pdm_cEjecutivo { get; set; }
        [MaxLength(400)]
        public string Pdm_cMarca { get; set; }
        [MaxLength(400)]
        public string Pdm_cRodaje { get; set; }
        [MaxLength(400)]
        public string Pdm_cColor { get; set; }
        [MaxLength(400)]
        public string Pdm_cModelo { get; set; }
        [MaxLength(400)]
        public string Pdm_cAnioVehiculo { get; set; }
        [MaxLength(400)]
        public string Pdm_cKm { get; set; }
        [MaxLength(1)]
        public string Dvm_cRptAgrupado { get; set; }
        [MaxLength(1)]
        public string Dvm_cRptDescEditable { get; set; }
        [MaxLength(20)]
        public string Dvm_cOC { get; set; }
        public decimal Dvm_cSeguro { get; set; }
        public decimal Dvm_cFlete { get; set; }
        public decimal Dvm_cGastosExp { get; set; }
        [MaxLength(250)]
        public string Dvm_cCodigo { get; set; }
        [MaxLength(250)]
        public string Dvm_cARC { get; set; }
        [MaxLength(250)]
        public string Dvm_cIncoterm { get; set; }
        [MaxLength(1)]
        public string Dvm_cRetencion { get; set; }
        public decimal Dvm_nImpReten { get; set; }

        [MaxLength(50)]
        public string @Dvm_cValidaSunat { get; set; }


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

        [Display(Name = "NoParameter")]
        [MaxLength(50)]
        public string MedioPago { get; set; }

    }
}

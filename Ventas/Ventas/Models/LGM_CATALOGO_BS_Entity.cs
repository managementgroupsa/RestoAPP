using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace RestoAPP.Models
{
    public class LGM_CATALOGO_BS_Entity
    {
        
        [MaxLength(20)]
        public string Accion { get; set; }
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(6)]
        public string Cab_cCatalogo { get; set; }
        [MaxLength(500)]
        public string Aca_cCategoria { get; set; }
        [MaxLength(500)]
        public string Gru_cGrupo { get; set; }
        [MaxLength(500)]
        public string Cla_cClase { get; set; }
        [MaxLength(500)]
        public string Fam_cFamilia { get; set; }
        [MaxLength(600)]
        public string Cab_cDescripLarga { get; set; }
        [MaxLength(600)]
        public string Cab_cDescripCorta { get; set; }
        [MaxLength(1000)]
        public string Cab_Especificacion { get; set; }
        [MaxLength(1)]
        public string Cab_cTipoBS { get; set; }
        [MaxLength(3)]
        public string Cab_cTipoMonedaCom { get; set; }
        [MaxLength(3)]
        public string Cab_cTipoMonedaVen { get; set; }
        [MaxLength(1)]
        public string Cab_cPrecioFijo { get; set; }
        [MaxLength(1)]
        public string Cab_cMetodoCosteo { get; set; }
        [MaxLength(3)]
        public string Mar_cMarca { get; set; }
        [MaxLength(40)]
        public string Mod_cModelo { get; set; }
        [MaxLength(10)]
        public string Cab_cUnidad { get; set; }
        [MaxLength(1)]
        public string Cab_cCtrlStock { get; set; }
        public int Cab_nStockMin { get; set; }
        public int Cab_nStockMax { get; set; }
        public int Cab_nStockReOrden { get; set; }
        public int Cab_nDiasSurtido { get; set; }
        public int Cab_nDiasGarantiaPrv { get; set; }
        public int Cab_nDiasGarantiaCli { get; set; }
        public int Cab_nVidaUtil { get; set; }
        public decimal Cab_nPeso { get; set; }
        [MaxLength(3)]
        public string Cab_cColor { get; set; }
        [MaxLength(1)]
        public string Cab_cTipoConjunto { get; set; }
        [MaxLength(1)]
        public string Cab_cLote { get; set; }
        [MaxLength(1)]
        public string Cab_cNacionalidad { get; set; }
        [MaxLength(3)]
        public string Cab_cPaisOrigen { get; set; }
        [MaxLength(1)]
        public string Ten_cTipoEntidad { get; set; }
        [MaxLength(5)]
        public string Prv_cCodigo { get; set; }
        [MaxLength(1)]
        public string Cab_cImpuestoCom1 { get; set; }
        [MaxLength(1)]
        public string Cab_cImpuestoCom2 { get; set; }
        [MaxLength(1)]
        public string Cab_cImpuestoCom3 { get; set; }
        [MaxLength(1)]
        public string Cab_cImpuestoCom4 { get; set; }
        [MaxLength(1)]
        public string Cab_cImpuestoCom5 { get; set; }
        [MaxLength(1)]
        public string Cab_cImpuestoVta1 { get; set; }
        [MaxLength(1)]
        public string Cab_cImpuestoVta2 { get; set; }
        [MaxLength(1)]
        public string Cab_cImpuestoVta3 { get; set; }
        [MaxLength(1)]
        public string Cab_cImpuestoVta4 { get; set; }
        [MaxLength(1)]
        public string Cab_cImpuestoVta5 { get; set; }
        [MaxLength(3)]
        public string Cab_cAlmacena { get; set; }
        [MaxLength(3)]
        public string Cab_cEmbalaje { get; set; }
        [MaxLength(50)]
        public string Cab_cReferencia { get; set; }
        [MaxLength(12)]
        public string Cab_cCuentaActivo { get; set; }
        [MaxLength(12)]
        public string Cab_cCuentaDepre { get; set; }
        [MaxLength(12)]
        public string Cab_cCuentaPagar { get; set; }
        [MaxLength(12)]
        public string Cab_cCuentaGasto { get; set; }
        [MaxLength(12)]
        public string Cab_cCuentaEgrBaja { get; set; }
        [MaxLength(12)]
        public string Cab_cCuentaIngBaja { get; set; }
        [MaxLength(1)]
        public string Cab_cEstado { get; set; }
        [MaxLength(20)]
        public string Cab_cUser { get; set; }
        [MaxLength(40)]
        public string Cab_cCodBarras { get; set; }
        [MaxLength(50)]
        public string Cab_cCodPropio { get; set; }
        [MaxLength(1)]
        public string Cab_cFlgEsActivo { get; set; }
        [MaxLength(3)]
        public string Exi_cCodigo { get; set; }
        [MaxLength(100)]
        public string Cab_cUbicacion { get; set; }
        [MaxLength(7)]
        public string Cab_cPieza1 { get; set; }
        [MaxLength(7)]
        public string Cab_cPieza2 { get; set; }
        [MaxLength(1)]
        public string ImportarExcel { get; set; }
        public decimal Cab_cValor { get; set; }
        [MaxLength(1)]
        public string Cab_Consignacion { get; set; }
        [MaxLength(6)]
        public string CodCatalogo { get; set; }
        [MaxLength(4)]
        public string Pan_cAnioRef { get; set; }
        [MaxLength(1)]
        public string Cab_cCosto { get; set; }
        public float Cab_nMargen { get; set; }
        [MaxLength(5)]
        public string Ent_cCodigoMerc { get; set; }
        [MaxLength(12)]
        public string CtaContableV { get; set; }
        [MaxLength(12)]
        public string CtaContableC { get; set; }
        [MaxLength(20)]
        public string nConceptoV { get; set; }
        [MaxLength(20)]
        public string nConceptoC { get; set; }
        [MaxLength(12)]
        public string nCentroCosto { get; set; }
        [MaxLength(20)]
        public string Cab_cClasificacion { get; set; }
        [MaxLength(20)]
        public string Cab_cCodcolor { get; set; }
        [MaxLength(20)]
        public string Cab_cMPrima { get; set; }
        [MaxLength(20)]
        public string Cab_cCodArticulo { get; set; }
        [MaxLength(20)]
        public string Cab_cEspesor { get; set; }
        [MaxLength(20)]
        public string Cab_cEspesorDescrip { get; set; }
        public decimal Cab_nPieza { get; set; }
        public decimal Cab_nPies { get; set; }
        [MaxLength(100)]
        public string Cab_cCodSunat { get; set; }
        public decimal Cab_nUnidadCaja { get; set; }
        [MaxLength(6)]
        public string Pan_cAnioMesCrea { get; set; }

    }

}


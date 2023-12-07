using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ventas.Models
{
    public class CNM_ENTIDAD_Entity
    {
        [MaxLength(20)]
        public string Accion { get; set; }
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(5)]
        public string Ent_cCodEntidad { get; set; }
        [MaxLength(1)]
        public string Ten_cTipoEntidad { get; set; }
        [MaxLength(120)]
        public string Ent_cPersona { get; set; }
        [MaxLength(100)]
        public string Ent_cDireccion { get; set; }
        [MaxLength(20)]
        public string Ent_nRuc { get; set; }
        [MaxLength(80)]
        public string Ent_cRepresentante { get; set; }
        [MaxLength(3)]
        public string Ent_cTipoDoc { get; set; }
        [MaxLength(1)]
        public string Ent_cFlagPersona { get; set; }
        [MaxLength(1)]
        public string Ent_cEstadoEntidad { get; set; }
        [MaxLength(1)]
        public string Ent_cEstado { get; set; }
        [MaxLength(20)]
        public string Ent_cUserCrea { get; set; }
        [MaxLength(1)]
        public string Ent_cNoDaot { get; set; }
        [MaxLength(3)]
        public string Ent_cCondicion { get; set; }
        [MaxLength(3)]
        public string Ent_Mon_cCodigo { get; set; }
        public float Ent_LineaCredito { get; set; }
        [MaxLength(4000)]
        public string Ent_Observacion { get; set; }
        [MaxLength(1)]
        public string Ent_ValidaLC { get; set; }
        [MaxLength(50)]
        public string Ent_nTelefono { get; set; }
        [MaxLength(100)]
        public string En_nMail { get; set; }
        [MaxLength(50)]
        public string Ent_cHistClinica { get; set; }
        [MaxLength(1)]
        public string Ent_cTitular { get; set; }
        public DateTime Ent_dFechaApe { get; set; }
        public DateTime Ent_dFechaNac { get; set; }
        [MaxLength(10)]
        public string Ent_cSexo { get; set; }
        public decimal Ent_nEdad { get; set; }
        [MaxLength(2000)]
        public string Ent_cNacionalidad { get; set; }
        [MaxLength(2000)]
        public string Ent_cRaza { get; set; }
        [MaxLength(5)]
        public string Ent_cTipoSangre { get; set; }
        [MaxLength(50)]
        public string Ent_cEstadoCivil { get; set; }
        [MaxLength(2000)]
        public string Ent_cTipoCliente { get; set; }
        [MaxLength(2000)]
        public string Ent_cMedicoTratante { get; set; }
        [MaxLength(2000)]
        public string Ent_cCondicionPaciente { get; set; }
        [MaxLength(2000)]
        public string Ent_cOcupacion { get; set; }
        [MaxLength(2000)]
        public string Ent_cCentroTrab { get; set; }
        [MaxLength(50)]
        public string Ent_cTelefTrab { get; set; }
        public decimal Ent_nPeso { get; set; }
        public decimal Ent_nTalla { get; set; }
        public decimal Ent_nIMC { get; set; }
        [MaxLength(2000)]
        public string Ent_cNombreEsposo { get; set; }
        [MaxLength(20)]
        public string Ent_cDniEsposo { get; set; }
        [MaxLength(2000)]
        public string Ent_cCentroTrabEsposo { get; set; }
        [MaxLength(2000)]
        public string Ent_cTelefTrabEsposo { get; set; }
        [MaxLength(4000)]
        public string Ent_cMedioInf { get; set; }
        [MaxLength(20)]
        public string Ent_cTelefRecom { get; set; }
        [MaxLength(2000)]
        public string Ent_cDireccRecom { get; set; }
        [MaxLength(2000)]
        public string Ent_cDistritoNac { get; set; }
        [MaxLength(100)]
        public string DireccionP { get; set; }
        [MaxLength(10)]
        public string Ent_cTipoCredito { get; set; }
        [MaxLength(1)]
        public string Ent_cObligadosFE { get; set; }
        public DateTime Ent_dFecInicio { get; set; }
        [MaxLength(1000)]
        public string cOtrosAtri { get; set; }
        [MaxLength(1)]
        public string GrabaDireccion { get; set; }
        [MaxLength(8)]
        public string Dir_cUbigeo { get; set; }
        [MaxLength(5)]
        public string Ven_cCodigo { get; set; }

    }
}

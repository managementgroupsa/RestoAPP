﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestoPLUS.Models
{

    public class PEDIDO_Entity
    {

        public VTM_RESTO_APERTURA_Entity Cabecera { get; set; }

        public VTD_RESTO_APERTURA_Entity Detalle { get; set; }

        public List<VTD_RESTO_APERTURA_PEDIDO_Entity> Pedidos{ get; set; }

    }

    public class LGT_SERIEDOC_Entity
    {
        [MaxLength(50)]
        public string Accion { get; set; }
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(3)]
        public string Doc_cTipoDoc { get; set; }
        [MaxLength(4)]
        public string Doc_cSerie { get; set; }
        [MaxLength(10)]
        public string Doc_cNumInicio { get; set; }
    }


    public class CNT_TIPO_MONEDA_Entity
    {
        [MaxLength(50)]
        public string Accion { get; set; }
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(3)]
        public string Mon_cCodigo { get; set; }
        [MaxLength(60)]
        public string Mon_cNombreLargo { get; set; }

    }

    public class CNT_TIPODOC_Entity
    {
        [MaxLength(50)]
        public string Accion { get; set; }
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(2)]
        public string Tdo_cCodigo { get; set; }
        [MaxLength(60)]
        public string Tdo_cNombreLargo { get; set; }

    }



    public class CFG_PARAMETROS_Entity
    {
        [MaxLength(50)]
        public string Accion { get; set; }
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(4)]
        public string Pan_cAnio { get; set; }
        [MaxLength(30)]
        public string Valor { get; set; }
    }

   

}

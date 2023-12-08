using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RestoAPP.Models
{


    public class Login_Entity
    {
        [MaxLength(50)]
        public string Accion { get; set; }
        [MaxLength(50)]
        public string Usu_cCodUsuario { get; set; }
        [MaxLength(20)]
        public string Usu_cClave { get; set; }
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(3)]
        public string Soft_cCodSoft { get; set; }

        [MaxLength(300)]
        [Display(Name = "NoParameter")]
        public string usu_cRole { get; set; }

        [MaxLength(300)]
        [Display(Name = "NoParameter")]
        public string usu_cNombre { get; set; }

        [MaxLength(300)]
        [Display(Name = "NoParameter")]
        public string usu_cApellido { get; set; }

        [MaxLength(300)]
        [Display(Name = "NoParameter")]
        public string usu_cCorreo { get; set; }
    }

    class Respuesta_Entity
    {
        [MaxLength(1)]
        public string Respuesta { get; set; }
        [MaxLength(300)]
        public string Nombres { get; set; }

        [MaxLength(300)]
        public string Role { get; set; }
        [MaxLength(300)]
        public string Nombre { get; set; }
        [MaxLength(300)]
        public string Apellido { get; set; }
        [MaxLength(300)]
        public string Correo { get; set; }
    }



    public class PuntoVenta_Entity
    {
        public string Pvt_cCodigo { get; set; }
        public string Pvt_cDescripcion { get; set; }
    }


    class Empresa_Entity
    {
        [MaxLength(3)]
        public string Emp_cCodigo { get; set; }
        [MaxLength(100)]
        public string Emp_cNombreLargo { get; set; }
    }

    class Anio_Entity
    {
        [MaxLength(4)]
        public string Pan_cAnio { get; set; }
        [MaxLength(1)]
        public string Pan_cEstado { get; set; }
    }
}

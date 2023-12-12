using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RestoPLUS.Models
{
    public class VTD_RESTO_APERTURA_PEDIDO_Entity : INotifyPropertyChanged
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

        private int cantidad;
        public int Ped_nCantidad 
        {
            get { return cantidad; }
            set
            {
                if (cantidad != value)
                {
                    cantidad = value;
                    OnPropertyChanged(nameof(Ped_nCantidad));
                }
            }
        }



        [MaxLength(1000)]
        private string descripcion;
        public string Ped_cComentario
        {
            get { return descripcion; }
            set
            {
                if (descripcion != value)
                {
                    descripcion = value;
                    OnPropertyChanged(nameof(Ped_cComentario));
                }
            }
        }


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



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }


}


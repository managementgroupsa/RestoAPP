using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestoPLUS.Models
{
    public class MENSAJE_Entity
    {
        [MaxLength(100)]
        public string Codigo { get; set; }
        [MaxLength(2000)]
        public string Mensaje { get; set; }
        [MaxLength(3)]
        public string Resultado { get; set; } // error / ok

        public decimal FilasAfectadas { get; set; }

    }
}

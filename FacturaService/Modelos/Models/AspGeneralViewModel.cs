using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Models
{
    public class GeneralViewModel
    {
        public int Id { get; set; }

        //[Display(Name = "Sigla")]
        //[Required(ErrorMessage = "El campo {0} es requerido")]
        //[StringLength(10, ErrorMessage = "El campo {0} debe tener una longitud máxima de 10 caracteres")]
        //public string Sigla { get; set; }

        //[Display(Name = "Nombre")]
        //[Required(ErrorMessage = "El campo {0} es requerido")]
        //[StringLength(100, ErrorMessage = "El campo {0} debe tener una longitud máxima de {1} caracteres")]
        public string Nombre { get; set; }

        public int Estregistro { get; set; }
    }
}

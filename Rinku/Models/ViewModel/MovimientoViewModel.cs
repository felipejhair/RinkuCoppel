using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Rinku.Models.ViewModel
{
    public class MovimientoViewModel
    {
        [Required(ErrorMessage = "ingresa el {0}")]
        [Display(Name = "Numero del empleado")]
        [MaxLength(50, ErrorMessage = "El {0} debe tener un maximo de {1} caracteeres")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "ingresa el {0}")]
        [Display(Name = "Nombre del empleado")]
        [MaxLength(100, ErrorMessage = "El {0} debe tener un maximo de {1} caracteeres")]
        public string NombreEmpleado { get; set; }
        [Required(ErrorMessage = "ingresa el {0}")]
        [Display(Name = "Nombre del rol")]
        [MaxLength(50, ErrorMessage = "El {0} debe tener un maximo de {1} caracteeres")]
        public string NombreRol { get; set; }
        [Required(ErrorMessage = "ingresa el {0}")]
        [Display(Name = "Mes")]
        public int Mes { get; set; }
        [Required(ErrorMessage = "ingresa el {0}")]
        [Display(Name = "Entregas")]
        public int Entregas { get; set; }
    }
}

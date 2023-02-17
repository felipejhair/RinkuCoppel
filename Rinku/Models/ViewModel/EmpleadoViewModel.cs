using System.ComponentModel.DataAnnotations;

namespace Rinku.Models.ViewModel
{
    /// <summary>
    /// Modelo para la vista de ingresar empleado, contiene
    /// validaciones para el formualrio
    /// </summary>
    public class EmpleadoViewModel
    {
        [Required(ErrorMessage = "ingresa el {0}")]
        [Display(Name = "Nombre del empleado")]
        [MaxLength(100, ErrorMessage = "El {0} debe tener un maximo de {1} caracteeres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "ingresa el {0}")]
        [Display(Name = "Numero de empleado")]
        [MaxLength(50, ErrorMessage = "El {0} debe tener un maximo de {1} caracteeres")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "ingresa el {0}")]
        [Display(Name = "Rol")]
        public int RolId { get; set; }
    }
}

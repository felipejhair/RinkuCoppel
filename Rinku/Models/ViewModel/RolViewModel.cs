using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Rinku.Models.ViewModel
{
    /// <summary>
    /// Modelo para la vista de añadir rol
    /// contiene sus respectivas validaciones
    /// </summary>
    public class RolViewModel
    {
        [Required(ErrorMessage = "ingresa el {0}")]
        [Display(Name = "Nombre del rol")]
        [MaxLength(50, ErrorMessage = "El {0} debe tener un maximo de {1} caracteeres")]
        public string Name { get; set; }
    }
}

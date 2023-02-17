using System.Text.Json.Serialization;

namespace Rinku.Models.POCO
{
    /// <summary>
    /// Modelo para formatear un resultado de la base de datos, que incluye
    /// datos del empleado y el rol
    /// </summary>
    public class EmpleadoRol
    {
        [JsonPropertyName("numero")]
        public string Numero { get; set; }
        [JsonPropertyName("name")]
        public string NombreEmpleado { get; set; }
        [JsonPropertyName("nombre")]
        public string NombreRol { get; set; }
    }
}

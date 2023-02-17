using System.Text.Json.Serialization;

namespace Rinku.Models.POCO
{
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


using System.Text.Json.Serialization;

namespace Rinku.Models
{
    /// <summary>
    /// Modelo de Rol, tal como aparece en la base de datos
    /// </summary>
    public class Rol
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }
        [JsonPropertyName("status")]
        public bool Status { get; set; }
    }
}

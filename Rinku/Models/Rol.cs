using System.Text.Json.Serialization;

namespace Rinku.Models
{
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

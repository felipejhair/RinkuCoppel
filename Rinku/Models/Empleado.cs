using System.Text.Json.Serialization;

namespace Rinku.Models
{
    public class Empleado
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("numero")]
        public string Numero { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("rolid")]
        public int RolId { get; set; }
    }
}

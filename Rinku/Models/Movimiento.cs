using System.Text.Json.Serialization;

namespace Rinku.Models
{
    public class Movimiento
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("mes")]
        public int Mes { get; set; }
        [JsonPropertyName("entregas")]
        public int Entregas { get; set; }
        [JsonPropertyName("empleadoid")]
        public int EmpleadoId { get; set; }
    }
}

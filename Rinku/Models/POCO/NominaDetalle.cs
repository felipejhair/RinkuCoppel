using System.Text.Json.Serialization;

namespace Rinku.Models.POCO
{
    public class NominaDetalle
    {
        [JsonPropertyName("nombreempleado")]
        public string NombreEmpleado { get; set; }
        [JsonPropertyName("rolname")]
        public string RolName { get; set; }
        [JsonPropertyName("entregas")]
        public int Entregas { get; set; }
        [JsonPropertyName("bonoadicional")]
        public float BonoAdicional { get; set; }
        [JsonPropertyName("horastrabajadas")]
        public int HorasTrabajadas { get; set; }
        [JsonPropertyName("sueldo")]
        public float Sueldo { get; set; }
        [JsonPropertyName("pagobonos")]
        public float PagoBonos { get; set; }
        [JsonPropertyName("pagoentregas")]
        public float PagoEntregas { get; set; }
        [JsonPropertyName("retenciones")]
        public float Retenciones { get; set; }
        [JsonPropertyName("vales")]
        public float Vales { get; set; }
        [JsonPropertyName("total")]
        public float Total { get; set; }
        [JsonPropertyName("totalconimpuestos")]
        public float TotalConImpuestos { get; set; }
    }
}

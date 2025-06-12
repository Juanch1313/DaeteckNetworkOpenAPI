using System.Text.Json.Serialization;

namespace DaeteckNetworkOpenAPI.Models
{
    public class ONUSignals
    {
        [JsonPropertyName("unique_external_id")]
        public string Unique_External_Id { get; set; } = string.Empty;
        [JsonPropertyName("signal")]
        public string Signal { get; set; } = string.Empty;
        [JsonPropertyName("signal_1310")]
        public string Signal_1310 { get; set; } = string.Empty;
        [JsonPropertyName("signal_1490")]
        public string Signal_1490 { get; set; } = string.Empty;
    }
}

using System.Text.Json.Serialization;

namespace DaeteckNetworkOpenAPI.Models
{
    public class ONUDetails
    {
        [JsonPropertyName("unique_external_id")]
        public string Unique_External_Id { get; set; } = string.Empty;
        [JsonPropertyName("board")]
        public string Board { get; set; } = string.Empty;
        [JsonPropertyName("port")]
        public string Port { get; set; } = string.Empty;
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        [JsonPropertyName("signal")]
        public string Signal { get; set; } = string.Empty;
        [JsonPropertyName("signal_1310")]
        public string Signal_1310 { get; set; } = string.Empty;
        [JsonPropertyName("signal_1490")]
        public string Signal_1490 { get; set; } = string.Empty;
    }
}

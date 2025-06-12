using System.Text.Json.Serialization;

namespace DaeteckNetworkOpenAPI.Models
{
    public class Zone
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}

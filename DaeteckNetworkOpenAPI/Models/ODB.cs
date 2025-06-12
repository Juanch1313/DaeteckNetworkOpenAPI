using System.Text.Json.Serialization;

namespace DaeteckNetworkOpenAPI.Models
{
    public class ODB
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("nr_of_ports")]
        public string Nr_Of_Ports { get; set; } = string.Empty;
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; } = string.Empty;

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; } = string.Empty;
        [JsonPropertyName("zone_id")]
        public string Zone_Id { get; set; } = string.Empty;
    }
}

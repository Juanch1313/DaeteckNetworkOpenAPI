using Newtonsoft.Json;

namespace DaeteckNetworkAPI.Models
{
    public class Client
    {
        [JsonProperty (".id")]
        public string Id { get; set; } = string.Empty;
        [JsonProperty ("name")]
        public string Name { get; set; } = string.Empty;
        [JsonProperty ("max-limit")]
        public string MaxLimit { get; set; } = string.Empty;
        [JsonProperty ("target")]
        public string Target { get; set; } = string.Empty;


    }
}

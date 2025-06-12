using DaeteckNetworkOpenAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace DaeteckNetworkOpenAPI.Services.ZoneService
{
    public class ZoneServices : IZoneServices
    {
        private readonly string _Ip;
        private readonly string _Token;
        private readonly HttpClient _httpClient;

        public ZoneServices(IConfiguration configuration)
        {
            _Ip = configuration["SmartOltCredentials:Ip"] ?? string.Empty;
            _Token = configuration["SmartOltCredentials:Token"] ?? string.Empty;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri($"{_Ip}")
            };
        }

        public async Task<List<Zone>> GetAllZonesAsync()
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("X-Token", _Token);
                var response = await _httpClient.GetAsync("system/get_zones");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    JObject keyValuePairs = JObject.Parse(content);
                    var zones = keyValuePairs["response"]?.ToObject<List<Zone>>() ?? new List<Zone>();
                    return zones;
                }
                else
                {
                    throw new Exception($"Error fetching zones: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching zones: {ex.Message}", ex);
            }
        }
        public async Task<Zone> GetZoneByIdAsync(string zoneId)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-Token", _Token);
            var response = await _httpClient.GetAsync("system/get_zones");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                JObject keyValuePairs = JObject.Parse(content);
                var zones = keyValuePairs["response"]?.ToObject<List<Zone>>() ?? new List<Zone>();
                return zones.FirstOrDefault(z => z.Id == zoneId || z.Name == zoneId);
            }
            else
            {
                throw new Exception($"Error fetching zones: {response.ReasonPhrase}");
            }
        }
        public async Task<Zone> CreateZoneAsync(Zone zone)
        {
            //Implementacion momentanea, para que funcione el servicio de crear zonas
            //AUN NO SE SABE SI FUNCIONA CORRECTAMENTE
            //NO SE SABE SI TRABAJA BIEN TODAVIA HASTA QUE SE APRUEBE QUE SE PUEDE JUGAR CON EL API
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-Token", _Token);
            var content = new StringContent(JsonConvert.SerializeObject(zone), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("system/create_zone", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                JObject keyValuePairs = JObject.Parse(responseContent);
                var createdZone = keyValuePairs["response"]?.ToObject<Zone>();
                return createdZone;
            }
            else
            {
                throw new Exception($"Error creating zone: {response.ReasonPhrase}");
            }

        }
    }
}

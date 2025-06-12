using DaeteckNetworkOpenAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace DaeteckNetworkOpenAPI.Services.ODBService
{
    public class ODBServices : IODBServices
    {
        private readonly string _Ip;
        private readonly string _Token;
        private readonly HttpClient _httpClient;

        public ODBServices(IConfiguration configuration)
        {
            _Ip = configuration["SmartOltCredentials:Ip"] ?? string.Empty;
            _Token = configuration["SmartOltCredentials:Token"] ?? string.Empty;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri($"{_Ip}")
            };
        }

        public async Task<List<ODB>> GetAllODBsAsync(string idZone)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("X-Token", _Token);
                var response = await _httpClient.GetAsync($"system/get_odbs/{idZone}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    JObject keyValuePairs = JObject.Parse(content);
                    var odbList = keyValuePairs["response"]?.ToObject<List<ODB>>() ?? new List<ODB>();
                    return odbList;
                }
                else
                {
                    throw new Exception($"Error fetching ODBs: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching ODBs: {ex.Message}", ex);
            }
        }
        public async Task<ODB> GetODBByNameAsync(string zoneId,string id)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("X-Token", _Token);
                var response = await _httpClient.GetAsync($"system/get_odbs/{zoneId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    JObject keyValuePairs = JObject.Parse(content);
                    var odbList = keyValuePairs["response"]?.ToObject<List<ODB>>() ?? new List<ODB>();
                    return odbList.FirstOrDefault(o => o.Id == id || o.Name == id);
                }
                else
                {
                    throw new Exception($"Error fetching ODBs: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching ODBs: {ex.Message}", ex);
            }
        }

        public Task<ODB> CreateODBAsync(ODB odb)
        {
            //The current implementation assumes that the ODB object is already populated with the necessary data.
            //TRY NOT USE IT ITS ONLY A PLACEHOLDER FOR FUTURE IMPLEMENTATION
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("X-Token", _Token);
                var content = new StringContent(JsonConvert.SerializeObject(odb), Encoding.UTF8, "application/json");
                var response = _httpClient.PostAsync($"system/create_odb", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    JObject keyValuePairs = JObject.Parse(responseContent);
                    return Task.FromResult(keyValuePairs["response"]?.ToObject<ODB>());
                }
                else
                {
                    throw new Exception($"Error creating ODB: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating ODB: {ex.Message}", ex);
            }
        }

    }
}

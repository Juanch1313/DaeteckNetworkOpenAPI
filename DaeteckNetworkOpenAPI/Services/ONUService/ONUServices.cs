using DaeteckNetworkOpenAPI.Models;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using tik4net;

namespace DaeteckNetworkOpenAPI.Services.ONUService
{
    public class ONUServices : IONUServices
    {
        private readonly string _Ip;
        private readonly string _Token;
        private readonly HttpClient _httpClient;


        public ONUServices(IConfiguration configuration)
        {
            _Ip = configuration["SmartOltCredentials:Ip"] ?? string.Empty;
            _Token = configuration["SmartOltCredentials:Token"] ?? string.Empty;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri($"{_Ip}")
            };
        }

        public async Task<List<ONUDetails>> GetAllONUsAsync()
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("X-Token", _Token);
                var response = await _httpClient.GetAsync("onu/get_all_onus_details?olt_id=1&board=&port=&zone=&odb=");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    JObject keyValuePairs = JObject.Parse(content);
                    var onuList = keyValuePairs["onus"]?.ToObject<List<ONUDetails>>() ?? new List<ONUDetails>();
                    return onuList;
                }
                else
                {
                    throw new Exception($"Error fetching ONUs: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching ONUs: {ex.Message}", ex);
            }
        }

        public async Task<List<ONUSignals>> GetAllONUSignalsAsync()
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-Token", _Token);
            var response = await _httpClient.GetAsync("onu/get_onus_signals?olt_id=&board=&port=&zone=");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                JObject keyValuePairs = JObject.Parse(content);
                var onuSignalsList = keyValuePairs["response"]?.ToObject<List<ONUSignals>>() ?? new List<ONUSignals>();
                return onuSignalsList;
            }
            else
            {
                throw new Exception($"Error fetching ONU signals: {response.ReasonPhrase}");
            }
        }

        public async Task<ONUDetails?> GetONUByNameOrId(string id)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("X-Token", _Token);
                var response = await _httpClient.GetAsync("onu/get_all_onus_details?olt_id=1&board=&port=&zone=&odb=");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    JObject keyValuePairs = JObject.Parse(content);
                    var onuList = keyValuePairs["onus"]?.ToObject<List<ONUDetails>>() ?? new List<ONUDetails>();
                    var onu = onuList.First(o => o.Unique_External_Id == id || o.Name == id);
                    return onu;
                }
                else
                {
                    throw new Exception($"Error fetching ONUs: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching ONUs: {ex.Message}", ex);
            }
        }

        public async Task<ONUSignals?> GetONUSignalsById(string id)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("X-Token", _Token);
                var response = await _httpClient.GetAsync("onu/get_all_onus_details?olt_id=1&board=&port=&zone=&odb=");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    JObject keyValuePairs = JObject.Parse(content);
                    var onuList = keyValuePairs["onus"]?.ToObject<List<ONUSignals>>() ?? new List<ONUSignals>();
                    var onu = onuList.First(o => o.Unique_External_Id == id);
                    return onu;
                }
                else
                {
                    throw new Exception($"Error fetching ONUs: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching ONUs: {ex.Message}", ex);
            }
        }

        public Task<ONUDetails?> AuthorizeONU(ONUDetails onu)
        {
            throw new NotImplementedException();
        }
    }
}

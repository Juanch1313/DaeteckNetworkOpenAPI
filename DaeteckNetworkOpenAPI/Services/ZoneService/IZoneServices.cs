using DaeteckNetworkOpenAPI.Models;

namespace DaeteckNetworkOpenAPI.Services.ZoneService
{
    public interface IZoneServices
    {
        Task<List<Zone>> GetAllZonesAsync();
        Task<Zone> GetZoneByIdAsync(string zoneId);
        Task<Zone> CreateZoneAsync(Zone zone);
    }
}

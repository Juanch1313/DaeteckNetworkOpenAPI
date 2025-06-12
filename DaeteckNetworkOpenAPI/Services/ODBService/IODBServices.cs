using DaeteckNetworkOpenAPI.Models;

namespace DaeteckNetworkOpenAPI.Services.ODBService
{
    public interface IODBServices
    {
        Task<List<ODB>> GetAllODBsAsync(string id);
        Task<ODB> GetODBByNameAsync(string zoneId,string id);
        Task<ODB> CreateODBAsync(ODB odb);

    }
}

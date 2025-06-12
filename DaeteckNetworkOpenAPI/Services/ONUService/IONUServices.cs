using DaeteckNetworkOpenAPI.Models;
using System.Threading.Tasks;

namespace DaeteckNetworkOpenAPI.Services.ONUService
{
    public interface IONUServices
    {
        Task<List<ONUDetails>> GetAllONUsAsync();
        Task<List<ONUSignals>> GetAllONUSignalsAsync();
        Task<ONUDetails?> GetONUByNameOrId(string id);
        Task<ONUSignals?> GetONUSignalsById(string id);
        Task<ONUDetails?> AuthorizeONU(ONUDetails onu);
    }
}

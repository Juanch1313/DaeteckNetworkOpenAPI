using DaeteckNetworkAPI.Models;

namespace DaeteckNetworkAPI.Services
{
    public interface IClientService
    {
        List<Client> GetAllClientsAsync();
        Client? GetClientByIdAsync(string id);
        Client? CreateClientAsync(Client client);
        Client? UpdateClientAsync(string id, Client client);
        Client? DeleteClientAsync(string id);
    }
}

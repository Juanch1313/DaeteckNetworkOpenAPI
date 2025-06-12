using DaeteckNetworkAPI.Models;

namespace DaeteckNetworkOpenAPI.Services.ClientService
{
    public interface IClientServices
    {
        List<Client> GetAllClientsAsync();
        Client? GetClientByIdAsync(string id);
        Client? CreateClientAsync(Client client);
        Client? UpdateClientAsync(string id, Client client);
        Client? DeleteClientAsync(string id);
    }
}

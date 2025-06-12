using DaeteckNetworkAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using tik4net;
using tik4net.Objects;
using tik4net.Objects.Queue;

namespace DaeteckNetworkOpenAPI.Services.ClientService
{
    public class ClientServices : IClientServices
    {
        private readonly string Ip;
        private readonly string UsernameRead;
        private readonly string UsernameWrite;
        private readonly string Password;

        public ClientServices(IConfiguration configuration)
        {
            Ip = configuration["Credentials:Ip"] ?? string.Empty;
            UsernameRead = configuration["Credentials:UsernameRead"] ?? string.Empty;
            UsernameWrite = configuration["Credentials:UsernameWrite"] ?? string.Empty;
            Password = configuration["Credentials:Password"] ?? string.Empty;
        }
        public List<Client> GetAllClientsAsync()
        {
            using (var connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
            {
                connection.Open(Ip, UsernameRead, Password);

                var queues = connection.LoadAll<QueueSimple>();

                var clients = new List<Client>();
                foreach (var q in queues)
                {
                    var client = new Client
                    {
                        Id = q.Id,
                        Name = q.Name,
                        MaxLimit = q.MaxLimit,
                        Target = q.Target
                    };
                    clients.Add(client);
                }
                return clients;
            }
        }

        public Client? GetClientByIdAsync(string id)
        {
            using (var connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
            {
                connection.Open(Ip, UsernameRead, Password);

                var queues = connection.LoadAll<QueueSimple>();

                var clients = new List<Client>();
                foreach (var q in queues)
                {
                    var client = new Client
                    {
                        Id = q.Id,
                        Name = q.Name,
                        MaxLimit = q.MaxLimit,
                        Target = q.Target
                    };
                    clients.Add(client);
                }
                return clients.FirstOrDefault(c => c.Name == id);
            }
        }
        public Client? CreateClientAsync(Client client)
        {
            using (var connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
            {
                if (string.IsNullOrEmpty(client.Name) || string.IsNullOrEmpty(client.MaxLimit) || string.IsNullOrEmpty(client.Target))
                {
                    throw new ArgumentException("Client properties cannot be null or empty.");
                }
                connection.Open(Ip, UsernameWrite, Password);

                var queue = new QueueSimple
                {
                    Name = client.Name,
                    MaxLimit = client.MaxLimit,
                    Target = client.Target
                };

                connection.Save(queue);
                client.Id = queue.Id; // Assign the generated ID back to the client object
                return client;
            }
        }

        public Client? DeleteClientAsync(string id)
        {
            using (var connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
            {
                connection.Open(Ip, UsernameWrite, Password);

                var queueToDelete = connection.LoadAll<QueueSimple>()
                    .First(q => q.Name == id);

                if (queueToDelete == null)
                {
                    return null;
                }

                connection.Delete(queueToDelete);
                return new Client
                {
                    Id = queueToDelete.Id,
                    Name = queueToDelete.Name,
                    MaxLimit = queueToDelete.MaxLimit,
                    Target = queueToDelete.Target
                };

            }
        }

        public Client? UpdateClientAsync(string id, Client client)
        {
            throw new NotImplementedException();
        }
    }
}

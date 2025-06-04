using DaeteckNetworkAPI.Models;
using DaeteckNetworkAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaeteckNetworkAPI.Controllers
{   

    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        [HttpGet]
        public ActionResult<List<Client>> GetAllClients()
        {
            try
            {
                var clients = _clientService.GetAllClientsAsync();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error fetching clients: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Client> GetClientById(string id)
        {
            var client = _clientService.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound($"Client with ID {id} not found.");
            }
            return client;
        }

        [HttpPost]
        public ActionResult<Client> CreateClient(Client client)
        {
            if (client == null)
            {
                return BadRequest("Client data is null.");
            }

            var createdClient = _clientService.CreateClientAsync(client);
            if (createdClient == null)
                {
                return BadRequest("Error creating client. Please check the provided data.");
            }
            return CreatedAtAction(nameof(GetClientById), new { id = createdClient.Id }, createdClient);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient(string id)
        {
            var deleted = _clientService.DeleteClientAsync(id);
            if (deleted is null)
            {
                return NotFound($"Client with ID {id} not found.");
            }
            return NoContent();
        }
    }
}

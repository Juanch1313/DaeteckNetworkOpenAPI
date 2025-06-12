using DaeteckNetworkOpenAPI.Models;
using DaeteckNetworkOpenAPI.Services.ClientService;
using DaeteckNetworkOpenAPI.Services.ZoneService;
using Microsoft.AspNetCore.Mvc;

namespace DaeteckNetworkOpenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneController : Controller
    {
        private readonly IZoneServices _zoneServices;
        public ZoneController(IZoneServices zoneServices)
        {
            _zoneServices = zoneServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<Zone>>> GetAllZones()
        {
            try
            {
                var zones = await _zoneServices.GetAllZonesAsync();
                return Ok(zones);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error fetching zones: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Zone>> GetZoneById(string id)
        {
            var zone = _zoneServices.GetZoneByIdAsync(id);
            if (zone == null)
            {
                return NotFound($"Zone with ID {id} not found.");
            }
            return await zone;
        }
    }
}

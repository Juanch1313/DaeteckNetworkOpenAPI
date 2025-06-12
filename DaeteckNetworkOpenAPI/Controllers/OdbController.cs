using DaeteckNetworkOpenAPI.Models;
using DaeteckNetworkOpenAPI.Services.ODBService;
using Microsoft.AspNetCore.Mvc;

namespace DaeteckNetworkOpenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdbController : Controller
    {
        private readonly IODBServices _odbServices;

        public OdbController(IODBServices odbServices)
        {
            _odbServices = odbServices;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ODB>>> GetAllOdbs()
        {
            try
            {
                //Change "33" to the desired zone ID or make it dynamic based on your requirements
                var odbs = await _odbServices.GetAllODBsAsync("33");
                return Ok(odbs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error fetching ODBs: {ex.Message}");
            }
        }

        [HttpGet("search/{id}")]
        public async Task<ActionResult<ODB>> GetOdbById(string zoneId, string id)
        {
            try
            {
                var odb = await _odbServices.GetODBByNameAsync(zoneId, id);
                if (odb == null)
                {
                    return NotFound($"ODB with ID {id} not found.");
                }
                return Ok(odb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error fetching ODB: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ODB>> CreateOdb([FromBody] ODB odb)
        {
            if (odb == null)
            {
                return BadRequest("ODB data is null.");
            }
            try
            {
                var createdOdb = await _odbServices.CreateODBAsync(odb);
                return CreatedAtAction(nameof(GetOdbById), new { zoneId = odb.Zone_Id, id = createdOdb.Id }, createdOdb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating ODB: {ex.Message}");
            }
        }
    }
}

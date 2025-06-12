using DaeteckNetworkOpenAPI.Models;
using DaeteckNetworkOpenAPI.Services.ONUService;
using Microsoft.AspNetCore.Mvc;

namespace DaeteckNetworkOpenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnuController : Controller
    {
        private readonly IONUServices _onuService;

        public OnuController(IONUServices onuService)
        {
            _onuService = onuService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ONUDetails>>> GetAllOnus()
        {
            try
            {
                var onus = await _onuService.GetAllONUsAsync();
                return Ok(onus);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error fetching ONUs: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ONUDetails?>> GetOnuByNameOrId(string id)
        {
            try
            {
                var onu = await _onuService.GetONUByNameOrId(id);
                if (onu == null)
                {
                    return NotFound($"ONU with ID or Name '{id}' not found.");
                }
                return Ok(onu);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error fetching ONU: {ex.Message}");
            }
        }

        [HttpGet("signals")]
        public async Task<ActionResult<List<ONUSignals>>> GetAllOnuSignals()
        {
            try
            {
                var onuSignals = await _onuService.GetAllONUSignalsAsync();
                return Ok(onuSignals);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error fetching ONU signals: {ex.Message}");
            }
        }

        [HttpGet("signals/{id}")]
        public async Task<ActionResult<ONUSignals?>> GetOnuSignalsById(string id)
        {
            try
            {
                var onuSignals = await _onuService.GetONUSignalsById(id);
                if (onuSignals == null)
                {
                    return NotFound($"ONU signals with ID '{id}' not found.");
                }
                return Ok(onuSignals);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error fetching ONU signals: {ex.Message}");
            }
        }
    }
}

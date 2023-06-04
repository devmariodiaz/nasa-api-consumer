using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NasaAPIConsumer.Services;

namespace NasaAPIConsumer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NasaAPIController : ControllerBase
    {
        private readonly INasaAPIService _nasaAPIService;
        public NasaAPIController(INasaAPIService nasaAPIService)
        {
            _nasaAPIService = nasaAPIService;
        }

        [HttpGet]
        public async Task<IActionResult> GetData(int id)
        {
            try
            {
                await _nasaAPIService.Get(1);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "");
            }
        }
    }
}

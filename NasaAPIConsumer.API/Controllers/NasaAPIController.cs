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
        [Route("/asteroids")]
        public async Task<IActionResult> GetData([FromQuery]int days)
        {
            try
            {
                if (days == null)
                    return BadRequest("days parameter should be provided");

                var response = await _nasaAPIService.Get(days);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

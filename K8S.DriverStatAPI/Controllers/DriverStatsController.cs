using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace K8S.DriverStatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverStatsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DriverStatsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new { success = true, message = "Connection established... 🔥🔥🔥" });
        }


        [HttpGet("GetByWorldChampionships")]
        public async Task<IActionResult> GetByWorldChampionships()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"{_configuration.GetConnectionString("DriverAPI")}/GetTopDriversByWorldChampionships");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    return Ok(jsonResponse);
                }

                return BadRequest("Http test connection failed on DriverStatAPI");
            }
        }
    }
}

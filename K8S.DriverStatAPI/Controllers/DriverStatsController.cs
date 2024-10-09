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

        [HttpGet("GetByRaceWins")]
        public async Task<IActionResult> GetByRaceWins()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"{_configuration.GetConnectionString("DriverAPI")}/GetTopDriversByRaceWins");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    return Ok(jsonResponse);
                }

                return BadRequest("Http test connection failed on DriverStatAPI");
            }
        }

        [HttpGet("GetByFastestLap")]
        public async Task<IActionResult> GetByFastestLap()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"{_configuration.GetConnectionString("DriverAPI")}/GetTopDriversByFastestLap");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    return Ok(jsonResponse);
                }

                return BadRequest("Http test connection failed on DriverStatAPI");
            }
        }

        [HttpGet("GetByPolePosition")]
        public async Task<IActionResult> GetByPolePosition()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"{_configuration.GetConnectionString("DriverAPI")}/GetTopDriversByPolePosition");
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

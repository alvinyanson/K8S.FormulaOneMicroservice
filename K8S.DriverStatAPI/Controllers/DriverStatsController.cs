using K8S.DriverStatAPI.DTOs.Responses;
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


        [HttpGet("GetConsistencyRating/{driverId}")]
        public async Task<IActionResult> GetConsistencyRating(string driverId)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"{_configuration.GetConnectionString("DriverAchievementAPI")}/{driverId}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    var parsedResponse = JsonSerializer.Deserialize<DriverAchievementResponse>(jsonResponse);

                    var consistencyRating = (parsedResponse.Wins / parsedResponse.PolePosition) * 100;

                    return Ok(consistencyRating);
                }

                return BadRequest("Failed on DriverStatAPI.GetConsistencyRating");
            }
        }
    }
}

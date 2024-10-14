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
            return Ok(new { success = true, message = "Connection established... 🔥 🔥 🔥" });
        }

        [HttpGet("TestV1")]
        public IActionResult TestV1()
        {
            return Ok(new { success = true, message = "TestV1... 🔥 🔥 🔥" });
        }


        [HttpGet("GetConsistencyRating/{driverId}")]
        public async Task<IActionResult> GetConsistencyRating(string driverId)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"{_configuration.GetConnectionString("DriverAchievementAPI")}/{driverId}");
                if (response.IsSuccessStatusCode)
                {
                    var response1 = await response.Content.ReadAsStringAsync();

                    var response2 = JsonSerializer.Deserialize<DriverAchievementResponse>(response1);

                    var consistencyRating = (response2.Wins / response2.PolePosition) * 100;

                    return Ok(consistencyRating);
                }

                return BadRequest("Failed on DriverStatAPI.GetConsistencyRating");
            }
        }
    }
}

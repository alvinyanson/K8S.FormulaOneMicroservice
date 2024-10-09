
using K8S.DriverAPI.Data.Repositories.Interfaces;
using K8S.DriverAPI.DTOs.Requests;
using K8S.DriverAPI.DTOs.Responses;
using K8S.DriverAPI.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace K8S.DriverAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AchievementController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("TestConnection")]
        public IActionResult TestConnection() {
            return Ok("Connection is OK");
        }


        [HttpGet("{driverId:Guid}")]
        public async Task<IActionResult> GetDriverAchievements(Guid driverId)
        {
            var driverAchievements = await _unitOfWork.Achievements.GetDriverAchievementAsync(driverId);

            if(driverAchievements == null)
                return NotFound("Achievements not found.");

            var result = driverAchievements.Adapt<DriverAchievementResponse>();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddDriverAchievement([FromBody] CreateDriverAchievementRequest driverAchievement)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = driverAchievement.Adapt<Achievement>();

            await _unitOfWork.Achievements.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetDriverAchievements), new { driverId = result.DriverId }, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDriverAchievement([FromBody] UpdateDriverAchievementRequest driverAchievement)
        {
            if (!ModelState.IsValid) 
                return BadRequest();

            var result = driverAchievement.Adapt<Achievement>();

            await _unitOfWork.Achievements.Update(result);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}


using K8S.DriverAchievementAPI.Data.Repositories.Interfaces;
using K8S.DriverAchievementAPI.DTOs.Requests;
using K8S.DriverAchievementAPI.DTOs.Responses;
using K8S.DriverAchievementAPI.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace K8S.DriverAchievementAPI.Controllers
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
        public async Task<IActionResult> AddDriverAchievement([FromBody] CreateDriverAchievementRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = request.Adapt<Achievement>();

            await _unitOfWork.Achievements.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetDriverAchievements), new { driverId = result.DriverId }, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDriverAchievement([FromBody] UpdateDriverAchievementRequest request)
        {
            if (!ModelState.IsValid) 
                return BadRequest();

            var result = request.Adapt<Achievement>();

            await _unitOfWork.Achievements.Update(result);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}

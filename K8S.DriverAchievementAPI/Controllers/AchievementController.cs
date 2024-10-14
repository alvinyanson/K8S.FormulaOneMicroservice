
using K8S.Contracts;
using K8S.DriverAchievementAPI.Data.Repositories.Interfaces;
using K8S.DriverAchievementAPI.DTOs.Requests;
using K8S.DriverAchievementAPI.DTOs.Responses;
using K8S.DriverAchievementAPI.Models;
using Mapster;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace K8S.DriverAchievementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint;

        public AchievementController(IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint) 
        {
            _unitOfWork = unitOfWork;
            _publishEndpoint = publishEndpoint;
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

            _publishEndpoint.Publish<AchievementCreated>(new
            {
                RaceWins = result.RaceWins,
                PolePosition = result.PolePosition,
                FastestLap = result.FastestLap,
                WorldChampionship = result.WorldChampionship,
                DriverId = result.DriverId
            });

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

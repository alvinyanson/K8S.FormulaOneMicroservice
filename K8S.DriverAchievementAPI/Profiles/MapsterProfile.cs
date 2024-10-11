using K8S.DriverAchievementAPI.DTOs.Requests;
using K8S.DriverAchievementAPI.DTOs.Responses;
using K8S.DriverAchievementAPI.Models;
using Mapster;

namespace K8S.DriverAchievementAPI.Profiles
{
    public class MapsterProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // request to domain
            config.NewConfig<CreateDriverAchievementRequest, Achievement>()
                .Map(dest => dest.RaceWins, src => src.Wins);


            // domain to response
            // source to destination
            config.NewConfig<Achievement, DriverAchievementResponse>()
                .Map(dest => dest.Wins, src => src.RaceWins);

        }
    }
}

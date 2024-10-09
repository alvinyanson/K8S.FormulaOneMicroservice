using K8S.DriverAPI.DTOs.Requests;
using K8S.DriverAPI.DTOs.Responses;
using K8S.DriverAPI.Models;
using Mapster;

namespace K8S.DriverAPI.Profiles
{
    public class MapsterProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // request to domain
            config.NewConfig<CreateDriverRequest, Driver>()
                .Map(dest => dest.Status, src => 1)
                .Map(dest => dest.AddedDate, src => DateTime.UtcNow)
                .Map(dest => dest.UpdatedDate, src => DateTime.UtcNow);


            config.NewConfig<UpdateDriverRequest, Driver>()
                .Map(dest => dest.UpdatedDate, src => DateTime.UtcNow);



            // domain to response
            // source to destination
            config.NewConfig<Driver, GetDriverResponse>()
                .Map(dest => dest.DriverId, src => src.Id)
                .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");


            config.NewConfig<Driver, TopDriverByWorldChampionshipResponse>()
                .Map(dest => dest.DriverId, src => src.Id)
                .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}")
                .Map(dest => dest.WorldChampionships, src => src.Achievements.WorldChampionship);

            config.NewConfig<Driver, TopDriverByFastestLap>()
                .Map(dest => dest.DriverId, src => src.Id)
                .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}")
                .Map(dest => dest.FastestLap, src => src.Achievements.FastestLap);

            config.NewConfig<Driver, TopDriverByRaceWins>()
                .Map(dest => dest.DriverId, src => src.Id)
                .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}")
                .Map(dest => dest.RaceWins, src => src.Achievements.RaceWins);

            config.NewConfig<Driver, TopDriverByPolePosition>()
                .Map(dest => dest.DriverId, src => src.Id)
                .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}")
                .Map(dest => dest.PolePosition, src => src.Achievements.PolePosition);


            config.NewConfig<Achievement, DriverAchievementResponse>()
                .Map(dest => dest.Wins, src => src.RaceWins);
        }
    }
}

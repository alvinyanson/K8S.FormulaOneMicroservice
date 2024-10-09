using K8S.DriverAPI.Models;

namespace K8S.DriverAPI.Data.Repositories.Interfaces
{
    public interface IDriverRepository : IGenericRepository<Driver> 
    {
        Task<IEnumerable<Driver>> GetTopDriversByWorldChampionships();

        Task<IEnumerable<Driver>> GetTopDriversByRaceWins();
        
        Task<IEnumerable<Driver>> GetTopDriversByPolePosition();

        Task<IEnumerable<Driver>> GetTopDriversByFastestLap();


    }
}

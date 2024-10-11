

using K8S.DriverAchievementAPI.Models;

namespace K8S.DriverAchievementAPI.Data.Repositories.Interfaces
{
    public interface IAchievementRepository : IGenericRepository<Achievement>
    {
        Task<Achievement?> GetDriverAchievementAsync(Guid driverId);
    }
}

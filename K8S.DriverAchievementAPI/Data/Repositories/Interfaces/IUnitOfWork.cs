
namespace K8S.DriverAchievementAPI.Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IAchievementRepository Achievements { get; }

        Task<bool> CompleteAsync();
    }
}

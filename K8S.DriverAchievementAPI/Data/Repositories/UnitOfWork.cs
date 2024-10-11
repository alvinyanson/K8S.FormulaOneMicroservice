
using CQRSMediatr.DataService.Repositories;
using K8S.DriverAchievementAPI.Data.Repositories.Interfaces;
using K8S.DriverAchievementAPI.Models;
using Microsoft.Extensions.Logging;

namespace K8S.DriverAchievementAPI.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        public IAchievementRepository Achievements { get; }

        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;

            var logger = loggerFactory.CreateLogger("logs");

            Achievements = new AchievementsRepository(logger, context);
        }

        public async Task<bool> CompleteAsync()
        {
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

using K8S.DriverAchievementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace K8S.DriverAchievementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // define the db entities
        public virtual DbSet<Achievement> Achievements { get; set; }
    }
}

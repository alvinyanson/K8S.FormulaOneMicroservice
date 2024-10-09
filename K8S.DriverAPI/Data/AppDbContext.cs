using K8S.DriverAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace K8S.DriverAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // define the db entities
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Achievement> Achievements { get; set; }
    }
}

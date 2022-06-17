using CPSDevExerciseWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace CPSDevExerciseWeb.Database
{
    public class AppDatabaseContext : DbContext
    {
        public AppDatabaseContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(DatabaseSettings.ConnectionString);
        }

        public DbSet<Order>? Orders { get; set; }
    }
}

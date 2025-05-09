using Microsoft.EntityFrameworkCore;
using ZooSimulator.Models;

namespace ZooSimulator.DataAccess
{
    public class ZooContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ZooDb");
        }

        public DbSet<Animal> Animals { get; set; }

        public DbSet<Enclosure> Enclosures { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace NetworkConsistency.DAL.NetworkComposition.Repositories.Postgresql.ValueObjects
{
    internal class PostgreDbContext : DbContext
    {
        private readonly string _connectionString;
        public DbSet<PostgreStoredSensor> Sensors { get; set; }
        public DbSet<PostgreStoredSection> Sections { get; set; }

        public PostgreDbContext(PostgresqlProperties properties) => _connectionString = properties.ConnectionString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }
}
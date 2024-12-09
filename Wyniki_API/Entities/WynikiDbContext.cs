using Microsoft.EntityFrameworkCore;

namespace Wyniki_API.Entities
{
    public class WynikiDbContext : DbContext
    {
        private string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=WynikiDb;Trusted_Connection=True;";

        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany()
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany()
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}

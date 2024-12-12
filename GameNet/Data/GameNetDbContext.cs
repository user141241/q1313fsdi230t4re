using Microsoft.EntityFrameworkCore;
using GameNet.Models.Platform;

namespace GameNet.Data
{
    public class GameNetDbContext : DbContext
    {
        public GameNetDbContext(DbContextOptions<GameNetDbContext> options)
            : base(options)
        {
        }

        public DbSet<Lobby> Lobbies { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lobby>().ToTable("Lobby");
            modelBuilder.Entity<Player>().ToTable("Player");
        }
    }
}
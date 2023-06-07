using BowlingGame.Infrastructure.MSSql.Models;
using Microsoft.EntityFrameworkCore;

namespace BowlingGame.Infrastructure.MSSql
{
    public class BowlingGameContext : DbContext
    {
        public BowlingGameContext(DbContextOptions<BowlingGameContext> options)
           : base(options)
        {

        }

        public DbSet<DBFrame> Frames { get; set; }
        public DbSet<DBGame> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DBFrame>().ToTable("Frame");
            modelBuilder.Entity<DBFrame>().HasKey(x =>x.Id);
            
            modelBuilder.Entity<DBGame>().ToTable("Game");
            modelBuilder.Entity<DBGame>().HasKey(x => x.Id);
            
            modelBuilder.Entity<DBGame>().HasMany(x => x.Frames)
                .WithOne(x => x.Game)
                .HasForeignKey(x=>x.GameId)
                .IsRequired();
        }

    }
}

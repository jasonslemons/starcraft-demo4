using Microsoft.EntityFrameworkCore;
using StarcraftDemo4.Models;

namespace StarcraftDemo4.Data
{
    public class StarcraftDbContext : DbContext
    {
        public DbSet<GameEntity> Games { get; set; }
        public DbSet<GameStepEntity> GameSteps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=StarcraftDemo4;Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameEntity>(entity =>
            {
                entity.HasKey(e => e.GameId);
                entity.Property(e => e.StartTime).IsRequired();
                entity.Property(e => e.TotalGameTime).IsRequired();
            });

            modelBuilder.Entity<GameStepEntity>(entity =>
            {
                entity.HasKey(e => e.StepId);
                entity.Property(e => e.MoveType).HasMaxLength(50);
                entity.Property(e => e.MoveDescription).HasMaxLength(200);
                entity.Property(e => e.ObjectBuilt).HasMaxLength(100);
                entity.Property(e => e.StepTimestamp).IsRequired();
                
                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GameSteps)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
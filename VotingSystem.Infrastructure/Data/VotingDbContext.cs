using Microsoft.EntityFrameworkCore;
using VotingSystem.Domain.Entities;

namespace VotingSystem.Infrastructure.Data
{
    public class VotingDbContext : DbContext
    {
        public VotingDbContext(DbContextOptions<VotingDbContext> options) : base(options)
        {
        }

        public DbSet<Election> Elections { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Voter> Voters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Election設定
            modelBuilder.Entity<Election>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Candidates).HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
            });

            // Vote設定
            modelBuilder.Entity<Vote>(entity =>
            {
                entity.HasKey(v => v.Id);
                entity.Property(v => v.SelectedCandidate).IsRequired().HasMaxLength(100);
            });

            // Voter設定
            modelBuilder.Entity<Voter>(entity =>
            {
                entity.HasKey(v => v.Id);
                entity.Property(v => v.Identifier).IsRequired().HasMaxLength(100);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

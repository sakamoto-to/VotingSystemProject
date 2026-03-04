using Microsoft.EntityFrameworkCore;
using VotingSystem.Domain.Entities;
using VotingSystem.Domain.Simulation.Entities;

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

        // Simulation Entities
        public DbSet<VoterPersona> VoterPersonas { get; set; }
        public DbSet<TrendEvent> TrendEvents { get; set; }
        public DbSet<CandidateManifesto> CandidateManifestos { get; set; }

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

            // シードデータ: 5人のペルソナの初期設定
            modelBuilder.Entity<VoterPersona>().HasData(
                new VoterPersona 
                { 
                    Id = 1, Name = "ソウタ（20代・低所得フリーター）", Description = "「経済」重視。高齢者向け福祉や増税を嫌う。",
                    EconomyWeight = 1.5, EducationWeight = 0.5, WelfareWeight = -1.0, SecurityWeight = 0.2 
                },
                new VoterPersona 
                { 
                    Id = 2, Name = "ミキ（30代・中間層会社員）", Description = "「子育て」最重視。現役世代への負担増を嫌う。",
                    EconomyWeight = 0.8, EducationWeight = 2.0, WelfareWeight = 0.2, SecurityWeight = 0.5 
                },
                new VoterPersona 
                { 
                    Id = 3, Name = "ケンジ（40代・富裕層IT経営者）", Description = "「経済」重視。ばらまき福祉を極端に嫌う。",
                    EconomyWeight = 2.0, EducationWeight = 0.5, WelfareWeight = -1.5, SecurityWeight = 0.5 
                },
                new VoterPersona 
                { 
                    Id = 4, Name = "トメ（70代・年金生活者）", Description = "「福祉」「治安」重視。変化や年金カットを極端に嫌う。",
                    EconomyWeight = -0.5, EducationWeight = -0.5, WelfareWeight = 2.0, SecurityWeight = 1.5 
                },
                new VoterPersona 
                { 
                    Id = 5, Name = "マコト（50代・地方農家）", Description = "「治安（防災）」「経済（地方創生）」重視。都市部偏重を嫌う。",
                    EconomyWeight = 1.0, EducationWeight = 0.2, WelfareWeight = 0.8, SecurityWeight = 1.5 
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}

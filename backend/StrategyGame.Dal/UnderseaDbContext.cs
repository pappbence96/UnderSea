using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StrategyGame.Dal.Seed;
using StrategyGame.Model.Entities;

namespace StrategyGame.Dal
{
    public class UnderseaDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public UnderseaDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<Combat> Combats { get; set; }
        public DbSet<CombatUnitConnector> CombatUnitConnectors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryBuildingConnector> CountryBuildingConnectors { get; set; }
        public DbSet<CountryResearchConnector> CountryResearchConnectors { get; set; }
        public DbSet<CountryUnitConnector> CountryUnitConnectors { get; set; }
        public DbSet<Research> Researches { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<ScoreboardEntry> ScoreboardEntries { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=UnderseaDB;Trusted_Connection=True;MultipleActiveResultSets=True;Integrated Security=true");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Building>()
                .HasMany(b => b.Countries)
                .WithOne(c => c.Building)
                .HasForeignKey(c => c.BuildingId);

            builder.Entity<Research>()
                .HasMany(r => r.Countries)
                .WithOne(c => c.Research)
                .HasForeignKey(c => c.ResearchId);

            builder.Entity<Unit>()
                .HasMany(u => u.Countries)
                .WithOne(c => c.Unit)
                .HasForeignKey(c => c.UnitId);

            builder.Entity<Combat>()
                .HasMany(c => c.Units)
                .WithOne(c => c.Combat)
                .HasForeignKey(c => c.CombatId);

            builder.Entity<Country>()
                .HasMany(c => c.Buildings)
                .WithOne(c => c.Country)
                .HasForeignKey(c => c.CountryId);
            builder.Entity<Country>()
                .HasMany(c => c.Researches)
                .WithOne(c => c.Country)
                .HasForeignKey(c => c.CountryId);
            builder.Entity<Country>()
                .HasMany(c => c.Units)
                .WithOne(c => c.Country)
                .HasForeignKey(c => c.CountryId);
            builder.Entity<Country>()
                .HasMany(c => c.OutgoingAttacks)
                .WithOne(c => c.Attacker)
                .HasForeignKey(c => c.AttackerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Country>()
                .HasMany(c => c.IncomingAttacks)
                .WithOne(c => c.Defender)
                .HasForeignKey(c => c.DefenderId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Country>()
                .HasOne(c => c.User)
                .WithOne(u => u.Country)
                .HasForeignKey<Country>(c => c.UserId);
            builder.Entity<Country>()
                .HasMany(c => c.ScoreboardEntries)
                .WithOne(e => e.Country)
                .HasForeignKey(c => c.CountryId);

            builder.Entity<Round>()
                .HasMany(r => r.StartedBuilds)
                .WithOne(c => c.BuildStartedRound)
                .HasForeignKey(c => c.BuildStartedRoundId);
            builder.Entity<Round>()
                .HasMany(r => r.StartedResearches)
                .WithOne(c => c.ResearchStartedRound)
                .HasForeignKey(c => c.ResearchStartedRoundId);
            builder.Entity<Round>()
                .HasMany(r => r.ActiveCombats)
                .WithOne(c => c.Round)
                .HasForeignKey(c => c.RoundId);
            builder.Entity<Round>()
                .HasMany(r => r.ScoreboardEntries)
                .WithOne(e => e.Round)
                .HasForeignKey(e => e.RoundId);

            builder.Seed();
        }
    }
}

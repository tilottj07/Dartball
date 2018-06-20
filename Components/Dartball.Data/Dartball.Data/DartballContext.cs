using Dartball.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;

namespace Dartball.Data
{
    public class DartballContext : DbContext
    {
        //public DartballContext(DbContextOptions<DartballContext> options) : base(options)
        //{

        //}

        public DbSet<Game> Games { get; set; }
        public DbSet<GameInning> GameInnings { get; set; }
        public DbSet<GameInningTeam> GameInningTeams { get; set; }
        public DbSet<GameInningTeamBatter> GameInningTeamBatters { get; set; }
        public DbSet<GameTeam> GameTeams { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerTeam> PlayerTeams { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamPlayerLineup> TeamPlayerLineups { get; set; }
        public object FirstOrDefault { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            path = Path.Combine(path, "DartballDataBase.db3");

            optionsBuilder.UseSqlite($"Data Source={path}");
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries()
             .Where(e => e.State == EntityState.Added ||
                         e.State == EntityState.Modified))
            {
                entry.Property("ChangeDate").CurrentValue = DateTime.Now;
            }
            return base.SaveChanges();
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region set disinct keys

            modelBuilder.Entity<Game>().HasKey(x => x.GameId);
            modelBuilder.Entity<GameInning>().HasKey(x => x.GameInningId);
            modelBuilder.Entity<GameInningTeam>().HasKey(x => x.GameInningTeamId);
            modelBuilder.Entity<GameInningTeamBatter>().HasKey(x => x.GameInningTeamBatterId);
            modelBuilder.Entity<GameTeam>().HasKey(x => new { x.GameId, x.TeamId });
            modelBuilder.Entity<League>().HasKey(x => x.LeagueId);
            modelBuilder.Entity<Player>().HasKey(x => x.PlayerId);
            modelBuilder.Entity<PlayerTeam>().HasKey(x => new { x.PlayerId, x.TeamId });
            modelBuilder.Entity<Team>().HasKey(x => x.TeamId);
            modelBuilder.Entity<TeamPlayerLineup>().HasKey(x => new { x.TeamId, x.PlayerId });

            #endregion


            #region set indexes

            modelBuilder.Entity<GameInning>().HasIndex(x => new { x.GameId, x.InningNumber });
            modelBuilder.Entity<GameInningTeam>().HasIndex(x => new { x.GameInningId, x.GameTeamId });

            #endregion

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.Name).Ignore("IsDirty");
            }
        }

    }
}

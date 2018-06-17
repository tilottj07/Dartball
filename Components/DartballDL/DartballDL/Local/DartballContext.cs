using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Dartball.DataLayer.Local.Data;
using Microsoft.EntityFrameworkCore;

namespace Dartball.DataLayer.Local
{
    public class DartballContext : DbContext
    {
        public DartballContext(DbContextOptions<DartballContext> options) : base(options)
        {

        }

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




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            path = Path.Combine(path, "DartballDataBase.db3");

            optionsBuilder.UseSqlite($"Filename={path}");
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
            modelBuilder.Entity<Game>().HasKey(x => x.GameAlternateKey);
            modelBuilder.Entity<GameInning>().HasKey(x => new { x.GameAlternateKey, x.InningNumber });
            modelBuilder.Entity<GameInningTeam>().HasKey(x => new { x.GameInningAlternateKey, x.GameTeamAlternateKey });
            modelBuilder.Entity<GameInningTeamBatter>().HasKey(x => new { x.GameInningTeamAlternateKey, x.Sequence });
            modelBuilder.Entity<GameTeam>().HasKey(x => new { x.GameAlternateKey, x.TeamAlternateKey });
            modelBuilder.Entity<League>().HasKey(x => x.LeagueAlternateKey);
            modelBuilder.Entity<Player>().HasKey(x => x.PlayerAlternateKey);
            modelBuilder.Entity<PlayerTeam>().HasKey(x => new { x.PlayerAlternateKey, x.TeamAlternateKey });
            modelBuilder.Entity<Team>().HasKey(x => x.TeamAlternateKey);
            modelBuilder.Entity<TeamPlayerLineup>().HasKey(x => new { x.TeamAlternateKey, x.PlayerAlternateKey });

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.Name).Ignore("IsDirty");
            }
        }

    }
}

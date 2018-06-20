using Dartball.DataLayer.Local.Shared;
using System;
using System.Collections.Generic;

namespace Dartball.DataLayer.Local.Data
{
    public class Team : ChangeTracker
    {

        public int TeamId { get; set; }
        public string TeamId { get; set; }
        public string LeagueId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int? Handicap { get; set; }
        public int ShouldSync { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public League League { get; set; }
        public List<PlayerTeam> PlayerTeams { get; set; }
        public List<TeamPlayerLineup> TeamPlayerLineups { get; set; }


    }
}

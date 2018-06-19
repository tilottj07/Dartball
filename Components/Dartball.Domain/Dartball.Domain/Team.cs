using System;
using System.Collections.Generic;

namespace Dartball.Domain
{
    public class Team : ChangeTracker
    {

        public int TeamId { get; set; }
        public string TeamAlternateKey { get; set; }
        public string LeagueAlternateKey { get; set; }
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

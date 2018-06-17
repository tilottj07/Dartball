using Dartball.DataLayer.Local.Shared;
using System;
using System.Collections.Generic;

namespace Dartball.DataLayer.Local.Data
{
    public class Player : ChangeTracker
    {
        public int PlayerId { get; set; }
        public string PlayerAlternateKey { get; set; }
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int ShouldSync { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public List<PlayerTeam> PlayerTeams { get; set; }
        public List<TeamPlayerLineup> TeamPlayerLineups { get; set; }
    }
}

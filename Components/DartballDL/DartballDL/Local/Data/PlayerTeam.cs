using Dartball.DataLayer.Local.Shared;
using System;

namespace Dartball.DataLayer.Local.Data
{
    public class PlayerTeam : ChangeTracker
    {
        public int PlayerTeamId { get; set; }
        public string PlayerTeamAlternateKey { get; set; }
        public string PlayerAlternateKey { get; set; }
        public string TeamAlternateKey { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public Player Player { get; set; }
        public Team Team { get; set; }

    }
}

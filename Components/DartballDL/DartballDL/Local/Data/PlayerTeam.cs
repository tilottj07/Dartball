using Dartball.DataLayer.Local.Shared;
using System;

namespace Dartball.DataLayer.Local.Data
{
    public class PlayerTeam : ChangeTracker
    {
        public int PlayerTeamId { get; set; }
        public string PlayerTeamId { get; set; }
        public string PlayerId { get; set; }
        public string TeamId { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public Player Player { get; set; }
        public Team Team { get; set; }

    }
}

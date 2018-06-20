using Dartball.DataLayer.Local.Shared;
using System;

namespace Dartball.DataLayer.Local.Data
{
    public class TeamPlayerLineup : ChangeTracker
    {

        public int TeamPlayerLineupId { get; set; }
        public string TeamPlayerLineupId { get; set; }
        public string TeamId { get; set; }
        public string PlayerId { get; set; }
        public int BattingOrder { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public Team Team { get; set; }
        public Player Player { get; set; }

    }
}

using System;

namespace Dartball.Domain
{
    public class TeamPlayerLineup : ChangeTracker
    {

        public int TeamPlayerLineupId { get; set; }
        public string TeamPlayerLineupAlternateKey { get; set; }
        public string TeamAlternateKey { get; set; }
        public string PlayerAlternateKey { get; set; }
        public int BattingOrder { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public Team Team { get; set; }
        public Player Player { get; set; }

    }
}

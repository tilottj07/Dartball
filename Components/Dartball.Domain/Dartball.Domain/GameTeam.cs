using System;

namespace Dartball.Domain
{
    public class GameTeam : ChangeTracker
    {

        public int GameTeamId { get; set; }
        public string GameTeamAlternateKey { get; set; }
        public string GameAlternateKey { get; set; }
        public string TeamAlternateKey { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public Game Game { get; set; }
        public Team Team { get; set; }

    }
}

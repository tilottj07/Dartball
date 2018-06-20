using System;

namespace Dartball.Domain
{
    public class GameTeam : ChangeTracker
    {

        public string GameTeamId { get; set; }
        public string GameId { get; set; }
        public string TeamId { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public Game Game { get; set; }
        public Team Team { get; set; }

    }
}

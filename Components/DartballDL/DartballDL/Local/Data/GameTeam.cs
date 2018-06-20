using Dartball.DataLayer.Local.Shared;
using System;

namespace Dartball.DataLayer.Local.Data
{
    public class GameTeam : ChangeTracker
    {

        public int GameTeamId { get; set; }
        public string GameTeamId { get; set; }
        public string GameId { get; set; }
        public string TeamId { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public Game Game { get; set; }
        public Team Team { get; set; }

    }
}

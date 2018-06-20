using Dartball.DataLayer.Local.Shared;
using System;
using System.Collections.Generic;

namespace Dartball.DataLayer.Local.Data
{
    public class Game : ChangeTracker
    {

        public int GameId { get; set; }
        public string GameId { get; set; }
        public string LeagueId { get; set; }
        public DateTime GameDate { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public League League { get; set; }
        public List<GameInning> GameInnings { get; set; }

    }
}

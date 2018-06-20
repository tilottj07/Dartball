using System;
using System.Collections.Generic;

namespace Dartball.Domain
{
    public class Game : ChangeTracker
    {

        public string GameId { get; set; }
        public string LeagueId { get; set; }
        public DateTime GameDate { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public League League { get; set; }
        public List<GameInning> GameInnings { get; set; }

    }
}

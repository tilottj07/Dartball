using System;
using System.Collections.Generic;

namespace Dartball.Domain
{
    public class Game : ChangeTracker
    {

        public int GameId { get; set; }
        public string GameAlternateKey { get; set; }
        public string LeagueAlternateKey { get; set; }
        public DateTime GameDate { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public League League { get; set; }
        public List<GameInning> GameInnings { get; set; }

    }
}

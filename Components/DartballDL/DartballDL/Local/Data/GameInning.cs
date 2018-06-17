using Dartball.DataLayer.Local.Shared;
using System;
using System.Collections.Generic;

namespace Dartball.DataLayer.Local.Data
{
    public class GameInning : ChangeTracker
    {

        public int GameInningId { get; set; }
        public string GameInningAlternateKey { get; set; }
        public string GameAlternateKey { get; set; }
        public int InningNumber { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public Game Game { get; set; }
        public List<GameInningTeam> GameInningTeams { get; set; }

    }
}

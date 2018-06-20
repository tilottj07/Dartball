using System;
using System.Collections.Generic;

namespace Dartball.Domain
{
    public class GameInning : ChangeTracker
    {

        public string GameInningId { get; set; }
        public string GameId { get; set; }
        public int InningNumber { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public Game Game { get; set; }
        public List<GameInningTeam> GameInningTeams { get; set; }

    }
}

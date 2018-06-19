using System;
using System.Collections.Generic;

namespace Dartball.Domain
{
    public class GameInningTeam : ChangeTracker
    {

        public int GameInningTeamId { get; set; }
        public string GameInningTeamAlternateKey { get; set; }
        public string GameInningAlternateKey { get; set; }
        public string GameTeamAlternateKey { get; set; }
        public int Score { get; set; }
        public int Outs { get; set; }
        public int IsRunnerOnFirst { get; set; }
        public int IsRunnerOnSecond { get; set; }
        public int IsRunnerOnThird { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public GameInning GameInning { get; set; }
        public GameTeam GameTeam { get; set; }
        public List<GameInningTeamBatter> GameInningTeamBatters { get; set; }

    }
}

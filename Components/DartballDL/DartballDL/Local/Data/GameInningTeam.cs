using Dartball.DataLayer.Local.Shared;
using System;
using System.Collections.Generic;

namespace Dartball.DataLayer.Local.Data
{
    public class GameInningTeam : ChangeTracker
    {

        public int GameInningTeamId { get; set; }
        public string GameInningTeamId { get; set; }
        public string GameInningId { get; set; }
        public string GameTeamId { get; set; }
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

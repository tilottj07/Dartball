using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.DataLayer.Device.Dto
{
    public class GameInningTeamDto
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

    }
}

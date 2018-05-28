using Dartball.BusinessLayer.Game.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Dto
{
    public class GameInningTeamDto : IGameInningTeam
    {

        public int GameInningTeamId { get; set; }
        public Guid GameInningTeamAlternateKey { get; set; }
        public Guid GameInningAlternateKey { get; set; }
        public Guid GameTeamAlternateKey { get; set; }
        public int Score { get; set; }
        public int Outs { get; set; }
        public bool IsRunnerOnFirst { get; set; }
        public bool IsRunnerOnSecond { get; set; }
        public bool IsRunnerOnThird { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

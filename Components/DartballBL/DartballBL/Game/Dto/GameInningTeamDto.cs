using Dartball.BusinessLayer.Game.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Dto
{
    public class GameInningTeamDto : IGameInningTeam
    {

        public Guid GameInningTeamId { get; set; }
        public Guid GameInningId { get; set; }
        public Guid GameTeamId { get; set; }
        public int Score { get; set; }
        public int Outs { get; set; }
        public bool IsRunnerOnFirst { get; set; }
        public bool IsRunnerOnSecond { get; set; }
        public bool IsRunnerOnThird { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

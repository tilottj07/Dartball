using Dartball.BusinessLayer.Game.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Dto
{
    public class GameInningTeamBatterDto : IGameInningTeamBatter
    {

        public int GameInningTeamBatterId { get; set; }
        public Guid GameInningTeamBatterAlternateKey { get; set; }
        public Guid GameInningTeamAlternateKey { get; set; }
        public Guid PlayerAlternateKey { get; set; }
        public int Sequence { get; set; }
        public int EventType { get; set; }
        public int? TargetEventType { get; set; }
        public int RBIs { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

using Dartball.BusinessLayer.Game.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Dto
{
    public class GameInningTeamBatterDto : IGameInningTeamBatter
    {

        public Guid GameInningTeamBatterId { get; set; }
        public Guid GameInningTeamId { get; set; }
        public Guid PlayerId { get; set; }
        public int Sequence { get; set; }
        public int EventType { get; set; }
        public int? TargetEventType { get; set; }
        public int RBIs { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

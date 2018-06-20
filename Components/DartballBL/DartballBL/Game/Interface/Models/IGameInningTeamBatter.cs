using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Interface.Models
{
    public interface IGameInningTeamBatter
    {

        Guid GameInningTeamBatterId { get; }
        Guid GameInningTeamId { get; }
        Guid PlayerId { get; }
        int Sequence { get; }
        int EventType { get; }
        int? TargetEventType { get; }
        int RBIs { get; }
        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }

    }
}

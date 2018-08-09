using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Interface.Models
{
    public interface IGameTeam
    {

        Guid GameTeamId { get; }
        Guid GameId { get; }
        Guid TeamId { get; }
        int TeamBattingSequence { get; }
        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }

    }
}

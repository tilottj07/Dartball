using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Interface.Models
{
    public interface IGameInningTeam
    {

        Guid GameInningTeamId { get; }
        Guid GameInningId { get; }
        Guid GameTeamId { get; }
        int Score { get; }
        int Outs { get; }
        bool IsRunnerOnFirst { get; }
        bool IsRunnerOnSecond { get; }
        bool IsRunnerOnThird { get; }
        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }

    }
}

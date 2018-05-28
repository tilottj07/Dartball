using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Interface.Models
{
    public interface IGameInningTeam
    {

        int GameInningTeamId { get; }
        Guid GameInningTeamAlternateKey { get; }
        Guid GameInningAlternateKey { get; }
        Guid GameTeamAlternateKey { get; }
        int Score { get; }
        int Outs { get; }
        bool IsRunnerOnFirst { get; }
        bool IsRunnerOnSecond { get; }
        bool IsRunnerOnThird { get; }
        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }

    }
}

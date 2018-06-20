using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Interface
{
    public interface IGameInningTeamBatterService
    {

        IGameInningTeamBatter GetGameInningTeamBatter(Guid gameInningTeamId, int sequence);
        List<IGameInningTeamBatter> GetGameInningTeamBatters(Guid gameInningTeamId);
        List<IGameInningTeamBatter> GetGamePlayerAtBats(Guid gameId, Guid playerId);

        ChangeResult AddNew(IGameInningTeamBatter gameInningTeamBatter);
        ChangeResult AddNew(List<IGameInningTeamBatter> gameInningTeamBatters);

        ChangeResult Update(IGameInningTeamBatter gameInningTeamBatter);
        ChangeResult Update(List<IGameInningTeamBatter> gameInningTeamBatters);

        ChangeResult Remove(Guid gameInningTeamId, int sequence);

    }
}

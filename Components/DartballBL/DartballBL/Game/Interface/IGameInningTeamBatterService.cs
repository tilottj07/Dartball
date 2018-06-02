using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Interface
{
    public interface IGameInningTeamBatterService
    {

        IGameInningTeamBatter GetGameInningTeamBatter(Guid gameInningTeamAlternateKey, int sequence);
        List<IGameInningTeamBatter> GetGameInningTeamBatters(Guid gameInningTeamAlternateKey);
        List<IGameInningTeamBatter> GetGamePlayerAtBats(Guid gameAlternateKey, Guid playerAlteranteKey);

        ChangeResult AddNew(IGameInningTeamBatter gameInningTeamBatter);
        ChangeResult AddNew(List<IGameInningTeamBatter> gameInningTeamBatters);

        ChangeResult Update(IGameInningTeamBatter gameInningTeamBatter);
        ChangeResult Update(List<IGameInningTeamBatter> gameInningTeamBatters);

        ChangeResult Remove(Guid gameInningTeamAlternateKey, int sequence);

    }
}

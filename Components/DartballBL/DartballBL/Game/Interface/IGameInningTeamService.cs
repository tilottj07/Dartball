using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Interface
{
    public interface IGameInningTeamService
    {

        IGameInningTeam GetGameInningTeam(Guid gameInningTeamId);
        IGameInningTeam GetGameInningTeam(Guid gameTeamId, Guid gameInningId);
        List<IGameInningTeam> GetInningTeams(Guid gameInningId);
        List<IGameInningTeam> GetTeamInnings(Guid gameTeamId);

        ChangeResult AddNew(IGameInningTeam gameInningTeam);
        ChangeResult AddNew(List<IGameInningTeam> gameInningTeams);

        ChangeResult Update(IGameInningTeam gameInningTeam);
        ChangeResult Update(List<IGameInningTeam> gameInningTeams);

        ChangeResult Remove(Guid gameInningId, Guid gameTeamId);

    }
}

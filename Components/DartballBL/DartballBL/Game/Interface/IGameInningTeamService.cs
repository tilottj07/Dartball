using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Interface
{
    public interface IGameInningTeamService
    {
        IGameInningTeam GetGameInningTeam(Guid gameTeamAlternateKey, Guid gameInningAlternateKey);
        List<IGameInningTeam> GetInningTeams(Guid gameInningAlternateKey);
        List<IGameInningTeam> GetTeamInnings(Guid gameTeamAlternateKey);

        ChangeResult AddNew(IGameInningTeam gameInningTeam);
        ChangeResult AddNew(List<IGameInningTeam> gameInningTeams);

        ChangeResult Update(IGameInningTeam gameInningTeam);
        ChangeResult Update(List<IGameInningTeam> gameInningTeams);

        ChangeResult Remove(Guid gameInningAlternateKey, Guid gameTeamAlternateKey);

    }
}

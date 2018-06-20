using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Interface
{
    public interface IGameTeamService
    {

        IGameTeam GetGameTeam(Guid gameTeamId);
        IGameTeam GetGameTeam(Guid gameId, Guid teamId);
        List<IGameTeam> GetGameTeams(Guid gameId);
        List<IGameTeam> GetAllGames();

        ChangeResult AddNew(List<IGameTeam> gameTeams);
        ChangeResult AddNew(IGameTeam gameTeam);

        ChangeResult Update(IGameTeam gameTeam);
        ChangeResult Update(List<IGameTeam> gameTeams);

        ChangeResult Remove(Guid gameId, Guid teamId);


    }
}

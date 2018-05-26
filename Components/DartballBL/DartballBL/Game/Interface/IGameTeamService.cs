using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Interface
{
    public interface IGameTeamService
    {

        IGameTeam GetGameTeam(Guid gameAlternateKey, Guid teamAlternateKey);
        List<IGameTeam> GetGameTeams(Guid gameAlternateKey);
        List<IGameTeam> GetAllGames();

        ChangeResult AddNew(List<IGameTeam> gameTeams);
        ChangeResult AddNew(IGameTeam gameTeam);

        ChangeResult Update(IGameTeam gameTeam);
        ChangeResult Update(List<IGameTeam> gameTeams);

        ChangeResult Remove(Guid gameAlternateKey, Guid teamAlternateKey);


    }
}

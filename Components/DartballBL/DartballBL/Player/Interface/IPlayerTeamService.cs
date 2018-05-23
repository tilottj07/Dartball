using Dartball.BusinessLayer.Player.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Player.Interface
{
    public interface IPlayerTeamService
    {
        List<IPlayerTeam> GetTeamPlayers(Guid teamAlternateKey);
        IPlayerTeam GetPlayerTeam(Guid teamAlternateKey, Guid playerAlternateKey);

        ChangeResult AddNew(IPlayerTeam playerTeam);
        ChangeResult AddNew(List<IPlayerTeam> playerTeams);

        ChangeResult Update(IPlayerTeam playerTeam);
        ChangeResult Update(List<IPlayerTeam> playerTeams);

        ChangeResult Remove(Guid playerAlternateKey, Guid teamAlternateKey);
    }
}

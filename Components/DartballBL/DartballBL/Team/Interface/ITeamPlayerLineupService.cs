using Dartball.BusinessLayer.Player.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using Dartball.BusinessLayer.Team.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Team.Interface
{
    public interface ITeamPlayerLineupService
    {
        ITeamPlayerLineup GetTeamPlayerLineupItem(Guid teamId, Guid playerId);
        List<ITeamPlayerLineup> GetTeamLineup(Guid teamId);

        List<IPlayer> GetTeamSortedBattingOrderPlayers(Guid teamId);


        ChangeResult SetLineup(List<ITeamPlayerLineup> teamPlayerLineups);
        ChangeResult Save(ITeamPlayerLineup teamPlayerLineup);

        ChangeResult AddNew(ITeamPlayerLineup teamPlayerLineup);
        ChangeResult AddNew(List<ITeamPlayerLineup> teamPlayerLineups);

        ChangeResult Update(ITeamPlayerLineup teamPlayerLineup);
        ChangeResult Update(List<ITeamPlayerLineup> teamPlayerLineups);

        ChangeResult Remove(Guid teamId, Guid playerId);

    }
}

using Dartball.BusinessLayer.Shared.Models;
using Dartball.BusinessLayer.Team.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Team.Interface
{
    public interface ITeamPlayerLineupService
    {
        ITeamPlayerLineup GetTeamPlayerLineupItem(Guid teamAlternateKey, Guid playerAlternateKey);
        List<ITeamPlayerLineup> GetTeamLineup(Guid teamAlternateKey);

        ChangeResult AddNew(ITeamPlayerLineup teamPlayerLineup);
        ChangeResult AddNew(List<ITeamPlayerLineup> teamPlayerLineups);

        ChangeResult Update(ITeamPlayerLineup teamPlayerLineup);
        ChangeResult Update(List<ITeamPlayerLineup> teamPlayerLineups);

        ChangeResult Remove(Guid teamAlternateKey, Guid playerAlternateKey);

    }
}

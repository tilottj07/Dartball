using Dartball.BusinessLayer.Shared.Models;
using Dartball.BusinessLayer.Team.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Team.Interface
{
    public interface ITeamService
    {

        ITeam GetTeam(Guid teamAlternateKey);
        List<ITeam> GetTeams(Guid leagueAlternateKey);
        List<ITeam> GetTeams();

        ChangeResult AddNew(ITeam team);
        ChangeResult AddNew(List<ITeam> teams);

        ChangeResult Update(ITeam team);
        ChangeResult Update(List<ITeam> teams);

        ChangeResult Remove(Guid teamAlternateKey);

    }
}

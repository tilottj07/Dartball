using Dartball.BusinessLayer.League.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.League.Interface
{
    public interface ILeagueService
    {

        ILeague GetLeague(Guid leagueAlternateKey);
        List<ILeague> GetLeagues();

        ChangeResult Save(ILeague league);
        ChangeResult Save(List<ILeague> leagues);

        ChangeResult RemoveLeague(Guid leagueAlternateKey);

    }
}

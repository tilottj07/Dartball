﻿using Dartball.BusinessLayer.League.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.League.Interface
{
    public interface ILeagueService
    {

        ILeague GetLeague(Guid leagueId);
        List<ILeague> GetLeagues();
        ILeague GetGenericLeague();

        ChangeResult AddNew(ILeague league);
        ChangeResult AddNew(List<ILeague> leagues);

        ChangeResult Update(ILeague league);
        ChangeResult Update(List<ILeague> leagues);

        ChangeResult RemoveLeague(Guid leagueId);

    }
}

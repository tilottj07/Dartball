﻿using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Interface
{
    public interface IGameService
    {

        IGame GetGame(Guid gameId);
        List<IGame> GetLeagueGames(Guid leagueId);
        List<IGame> GetAllGames();
        List<Tuple<Guid, int>> GetGameTeamScores(Guid gameId);

        ChangeResult Save(IGame game);

        ChangeResult AddNew(IGame game);
        ChangeResult AddNew(List<IGame> games);

        ChangeResult Update(IGame game);
        ChangeResult Update(List<IGame> games);

        ChangeResult Remove(Guid gameId);

    }
}

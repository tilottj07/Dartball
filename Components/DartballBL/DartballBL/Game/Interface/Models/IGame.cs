using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Interface.Models
{
    public interface IGame
    {
        int GameId { get; }
        Guid GameAlternateKey { get; }
        Guid LeagueAlternateKey { get; }
        DateTime GameDate { get; }
        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }

    }
}

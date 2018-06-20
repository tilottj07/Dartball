using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Interface.Models
{
    public interface IGame
    {
        Guid GameId { get; }
        Guid LeagueId { get; }
        DateTime GameDate { get; }
        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }

    }
}

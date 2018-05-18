using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.League.Interface.Models
{
    public interface ILeague
    {
        int LeagueId { get; }
        Guid LeagueAlternateKey { get; }
        string Name { get; }
        string Password { get; }

    }
}

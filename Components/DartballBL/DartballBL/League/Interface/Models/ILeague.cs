using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.League.Interface.Models
{
    public interface ILeague
    {
        int LeagueId { get; }
        string Name { get; }
        string Password { get; }

    }
}

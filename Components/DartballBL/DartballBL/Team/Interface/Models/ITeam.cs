using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Team.Interface.Models
{
    public interface ITeam
    {
        int TeamId { get; }
        Guid TeamAlternateKey { get; }
        Guid LeagueAlternateKey { get; }
        string Name { get; }
        string Password { get; }
        int? Handicap { get; }
        int ShouldSync { get; }
        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }

    }
}

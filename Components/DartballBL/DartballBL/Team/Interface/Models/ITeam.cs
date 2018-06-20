using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Team.Interface.Models
{
    public interface ITeam
    {
        Guid TeamId { get; }
        Guid LeagueId { get; }
        string Name { get; }
        string Password { get; }
        int? Handicap { get; }
        bool ShouldSync { get; }
        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }

    }
}

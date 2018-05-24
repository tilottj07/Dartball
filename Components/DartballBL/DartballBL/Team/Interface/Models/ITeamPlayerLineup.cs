using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Team.Interface.Models
{
    public interface ITeamPlayerLineup
    {
        int TeamPlayerLineupId { get; }
        Guid TeamPlayerLineupAlternateKey { get; }
        Guid TeamAlternateKey { get; }
        Guid PlayerAlternateKey { get; }
        int BattingOrder { get; }
        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }
    }
}

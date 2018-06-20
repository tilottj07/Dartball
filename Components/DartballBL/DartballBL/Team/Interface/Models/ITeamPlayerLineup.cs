using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Team.Interface.Models
{
    public interface ITeamPlayerLineup
    {
        Guid TeamPlayerLineupId { get; }
        Guid TeamId { get; }
        Guid PlayerId { get; }
        int BattingOrder { get; }
        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }
    }
}

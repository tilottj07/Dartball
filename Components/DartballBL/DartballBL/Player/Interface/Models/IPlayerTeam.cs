using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Player.Interface.Models
{
    public interface IPlayerTeam
    {

        Guid PlayerTeamId { get; }
        Guid PlayerId { get; }
        Guid TeamId { get; }
        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }

    }
}

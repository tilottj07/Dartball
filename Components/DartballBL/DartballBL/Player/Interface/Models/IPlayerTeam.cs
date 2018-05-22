using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Player.Interface.Models
{
    public interface IPlayerTeam
    {

        int PlayerTeamId { get; }
        Guid PlayerTeamAlternateKey { get; }
        Guid PlayerAlternateKey { get; }
        Guid TeamAlternateKey { get; }
        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Interface.Models
{
    public interface IGameTeam
    {

        int GameTeamId { get; }
        Guid GameTeamAlternateKey { get; }
        Guid GameAlternateKey { get; }
        Guid TeamAlternateKey { get; }
        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }

    }
}

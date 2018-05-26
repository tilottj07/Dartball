using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Interface.Models
{
    public interface IGameInning
    {

        int GameInningId { get; }
        Guid GameInningAlternateKey { get; }
        Guid GameAlternateKey { get; }
        int InningNumber { get; }
        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }

    }
}

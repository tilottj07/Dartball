using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Interface.Models
{
    public interface IGameInning
    {

        Guid GameInningId { get; }
        Guid GameId { get; }
        int InningNumber { get; }
        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }

    }
}

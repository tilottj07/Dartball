using System;
using System.Collections.Generic;
using System.Text;
using static Dartball.BusinessLayer.Game.Implementation.GameEventService;

namespace Dartball.BusinessLayer.Game.Interface.Models
{
    public interface IGameEvent
    {

        EventType Event { get; }
        string Description { get; }

    }
}

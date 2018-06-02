using Dartball.BusinessLayer.Game.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static Dartball.BusinessLayer.Game.Implementation.GameEventService;

namespace Dartball.BusinessLayer.Game.Interface
{
    public interface IGameEventService
    {

        IGameEvent GetGameEvent(EventType eventType);
        List<IGameEvent> GetAllGameEvents();

    }
}

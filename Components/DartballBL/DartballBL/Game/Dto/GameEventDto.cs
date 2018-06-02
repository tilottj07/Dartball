using Dartball.BusinessLayer.Game.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static Dartball.BusinessLayer.Game.Implementation.GameEventService;

namespace Dartball.BusinessLayer.Game.Dto
{
    public class GameEventDto : IGameEvent
    {

        public EventType Event { get; set; }
        public string Description { get; set; }


    }
}

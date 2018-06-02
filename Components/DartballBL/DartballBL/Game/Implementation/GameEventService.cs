using Dartball.BusinessLayer.Game.Dto;
using Dartball.BusinessLayer.Game.Interface;
using Dartball.BusinessLayer.Game.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Implementation
{
    public class GameEventService : IGameEventService
    {
        public GameEventService()
        {

        }



        public IGameEvent GetGameEvent(EventType eventType)
        {
            return new GameEventDto()
            {
                Event = eventType,
                Description = GetEventTypeDescription(eventType)
            };
        }

        public List<IGameEvent> GetAllGameEvents()
        {
            List<IGameEvent> events = new List<IGameEvent>();
            foreach(var item in Enum.GetValues(typeof(EventType)))
            {
                events.Add(GetGameEvent((EventType)item));
            }
            return events;
        }



        public enum EventType
        {
            Out = 0,
            Single = 1,
            Double = 2,
            Triple = 3,
            HomeRun = 4,
            Walk = 5,
            DoublePlay = 6,
            SacraficeHit = 7,
            TwoBaseSingle = 8
        }


        public string GetEventTypeDescription(EventType eventType)
        {
            switch (eventType)
            {
                case EventType.Out:
                    return "Out";
                case EventType.Single:
                    return "Single";
                case EventType.Double:
                    return "Double";
                case EventType.Triple:
                    return "Triple";
                case EventType.HomeRun:
                    return "Home Run";
                case EventType.Walk:
                    return "Walk";
                case EventType.DoublePlay:
                    return "Double Play";
                case EventType.SacraficeHit:
                    return "Sacrafice Hit";
                case EventType.TwoBaseSingle:
                    return "Two Base Single";

                default:
                    throw new Exception($"Event Type: {eventType} is not mapped to a description");
            }
        }


    }
}

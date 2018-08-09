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
            Unknown = 0,
            Out = 1,
            Single = 2,
            Double = 3,
            Triple = 4,
            HomeRun = 5,
            Walk = 6,
            DoublePlay = 7,
            SacraficeHit = 8,
            TwoBaseSingle = 9
        }


        public string GetEventTypeDescription(EventType eventType)
        {
            switch (eventType)
            {
                case EventType.Unknown:
                    return "Unknown";
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

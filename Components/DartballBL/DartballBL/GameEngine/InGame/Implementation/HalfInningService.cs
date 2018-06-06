using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.GameEngine.Event.Dto;
using Dartball.BusinessLayer.GameEngine.Event.Implementation;
using Dartball.BusinessLayer.GameEngine.Event.Interface;
using Dartball.BusinessLayer.GameEngine.Event.Interface.Models;
using Dartball.BusinessLayer.GameEngine.InGame.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Dartball.BusinessLayer.Game.Implementation.GameEventService;

namespace Dartball.BusinessLayer.GameEngine.InGame.Implementation
{
    public class HalfInningService : IHalfInningService
    {
        private IGameEventSingleService Single;
        private IGameEventDoubleService Double;
        private IGameEventTripleService Triple;
        private IGameEventHomeRunService HomeRun;
        private IGameEventOutService Out;
        private IGameEventDoublePlayService DoublePlay;
        private IGameEventSacraficeHitService SacraficeHit;
        private IGameEventTwoBaseSingleService TwoBaseSingle;

        public HalfInningService()
        {
            Single = new GameEventSingleService();
            Double = new GameEventDoubleService();
            Triple = new GameEventTripleService();
            HomeRun = new GameEventHomeRunService();
            Out = new GameEventOutService();
            DoublePlay = new GameEventDoublePlayService();
            SacraficeHit = new GameEventSacraficeHitService();
            TwoBaseSingle = new GameEventTwoBaseSingleService();
        }


        public IHalfInningActions GetHalfInningActions(List<IGameInningTeamBatter> gameInningTeamBatters)
        {
            IHalfInningActions dto = new HalfInningActionsDto();

            foreach(var batter in gameInningTeamBatters.OrderBy(x => x.Sequence))
            {
                switch((EventType)batter.EventType)
                {
                    case EventType.Single:
                        dto = Single.FillSingleActions(dto);
                        break;
                    case EventType.Double:
                        dto = Double.FillDoubleActions(dto);
                        break;
                    case EventType.Triple:
                        dto = Triple.FillTripleActions(dto);
                        break;
                    case EventType.HomeRun:
                        dto = HomeRun.FillHomeRunActions(dto);
                        break;
                    case EventType.Out:
                        dto = Out.FillOutActions(dto);
                        break;
                    case EventType.DoublePlay:
                        dto = DoublePlay.FillDoublePlayActions(dto);
                        break;
                    case EventType.SacraficeHit:
                        dto = SacraficeHit.FillSacraficeHitActions(dto);
                        break;
                    case EventType.TwoBaseSingle:
                        dto = TwoBaseSingle.FillTwoBaseSingleActions(dto);
                        break;

                    default:
                        throw new Exception($"Event Type: {(EventType)batter.EventType} is not mapped.");
                }
            }

            return dto;
        }


    }
}

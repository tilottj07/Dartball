using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.GameEngine.Event.Dto;
using Dartball.BusinessLayer.GameEngine.Event.Interface;
using Dartball.BusinessLayer.GameEngine.Event.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using static Dartball.BusinessLayer.Game.Implementation.GameEventService;

namespace Dartball.BusinessLayer.GameEngine.Event.Implementation
{
    public class GameEventOutService : IGameEventOutService
    {
        private IMapper Mapper;

        public GameEventOutService()
        {
            var mapConfig = new MapperConfiguration(c =>
               {
                   c.CreateMap<IHalfInningActions, HalfInningActionsDto>();
               });

            Mapper = mapConfig.CreateMapper();
        }


        public IHalfInningActions FillOutActions(IHalfInningActions actions, List<IGameInningTeamBatter> gameInningTeamBatters)
        {
            HalfInningActionsDto dto = Mapper.Map<HalfInningActionsDto>(actions);

            dto = PopulateTotalOuts(dto, gameInningTeamBatters);
            dto = PopulateShouldAdvanceToNextHalfInning(dto);

            return dto;
        }



        private HalfInningActionsDto PopulateTotalOuts(HalfInningActionsDto dto, List<IGameInningTeamBatter> gameInningTeamBatters)
        {
            dto.TotalOuts = gameInningTeamBatters.Where(x => (EventType)x.EventType == EventType.Out).Count();
            return dto;
        }

        private HalfInningActionsDto PopulateShouldAdvanceToNextHalfInning(HalfInningActionsDto dto)
        {
            dto.AdvanceToNextHalfInning = dto.TotalOuts >= 3;
            return dto;
        }


    }
}

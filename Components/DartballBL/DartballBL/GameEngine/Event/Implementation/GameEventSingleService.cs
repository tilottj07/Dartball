using Dartball.BusinessLayer.GameEngine.Event.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Dartball.BusinessLayer.GameEngine.Event.Interface.Models;
using Dartball.BusinessLayer.GameEngine.Event.Dto;
using Dartball.BusinessLayer.Game.Interface.Models;

namespace Dartball.BusinessLayer.GameEngine.Event.Implementation
{
    public class GameEventSingleService : IGameEventSingleService
    {
        private IMapper Mapper;

        public GameEventSingleService()
        {
            var mapConfig = new MapperConfiguration(c => c.CreateMap<IHalfInningActions, HalfInningActionsDto>());
            Mapper = mapConfig.CreateMapper();
        }



        public IHalfInningActions FillSingleActions(IHalfInningActions actions)
        {
            HalfInningActionsDto dto = Mapper.Map<HalfInningActionsDto>(actions);
            if (dto.IsRunnerOnThird)
            {
                dto.IsRunnerOnThird = false;
                dto.TotalRuns++;
            }
            if (dto.IsRunnerOnSecond)
            {
                dto.IsRunnerOnSecond = false;
                dto.IsRunnerOnThird = true;
            }
            if (dto.IsRunnerOnFirst)
            {
                dto.IsRunnerOnSecond = true;
                dto.IsRunnerOnFirst = false;
            }
            dto.IsRunnerOnFirst = true;

            return dto;
        }


    }
}

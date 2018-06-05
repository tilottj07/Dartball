using AutoMapper;
using Dartball.BusinessLayer.GameEngine.Event.Dto;
using Dartball.BusinessLayer.GameEngine.Event.Interface;
using Dartball.BusinessLayer.GameEngine.Event.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.GameEngine.Event.Implementation
{
    public class GameEventWalkService : IGameEventWalkService
    {
        private IMapper Mapper;

        public GameEventWalkService()
        {
            var mapConfig = new MapperConfiguration(c => c.CreateMap<IHalfInningActions, HalfInningActionsDto>());
            Mapper = mapConfig.CreateMapper();
        }



        public IHalfInningActions FillWalkActions(IHalfInningActions actions)
        {
            HalfInningActionsDto dto = Mapper.Map<HalfInningActionsDto>(actions);

            if (AreBasesLoaded(dto) == true) dto.TotalRuns++;
            else
            {
                if (dto.IsRunnerOnFirst && dto.IsRunnerOnSecond)
                {
                    dto.IsRunnerOnFirst = false;
                    dto.IsRunnerOnThird = true;
                }
                else if (dto.IsRunnerOnFirst)
                {
                    dto.IsRunnerOnFirst = false;
                    dto.IsRunnerOnSecond = true;
                }
            }
            dto.IsRunnerOnFirst = true;

            return dto;
        }


        private bool AreBasesLoaded(HalfInningActionsDto dto)
        {
            return dto.IsRunnerOnFirst && dto.IsRunnerOnSecond && dto.IsRunnerOnThird;
        }


    }
}

﻿using AutoMapper;
using Dartball.BusinessLayer.GameEngine.Event.Dto;
using Dartball.BusinessLayer.GameEngine.Event.Interface;
using Dartball.BusinessLayer.GameEngine.Event.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.GameEngine.Event.Implementation
{
    public class GameEventDoubleService : IGameEventDoubleService
    {
        private IMapper Mapper;

        public GameEventDoubleService()
        {
            var mapConfig = new MapperConfiguration(c => c.CreateMap<IHalfInningActions, HalfInningActionsDto>());
            Mapper = mapConfig.CreateMapper();
        }



        public IHalfInningActions FillDoubleActions(IHalfInningActions actions)
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
                dto.TotalRuns++;
            }
            if (dto.IsRunnerOnFirst)
            {
                dto.IsRunnerOnFirst = false;
                dto.IsRunnerOnThird = true;
            }
            dto.IsRunnerOnSecond = true;

            return dto;
        }


    }
}
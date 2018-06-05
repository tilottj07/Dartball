using AutoMapper;
using Dartball.BusinessLayer.GameEngine.Event.Dto;
using Dartball.BusinessLayer.GameEngine.Event.Interface;
using Dartball.BusinessLayer.GameEngine.Event.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.GameEngine.Event.Implementation
{
    public class GameEventSacraficeHitService : IGameEventSacraficeHitService
    {
        private IMapper Mapper;

        public GameEventSacraficeHitService()
        {
            var mapConfig = new MapperConfiguration(c => c.CreateMap<IHalfInningActions, HalfInningActionsDto>());
            Mapper = mapConfig.CreateMapper();
        }



        public IHalfInningActions FillSacraficeHitActions(IHalfInningActions actions)
        {
            HalfInningActionsDto dto = Mapper.Map<HalfInningActionsDto>(actions);
            dto.TotalOuts++;

            if (dto.TotalOuts >= 3) dto.AdvanceToNextHalfInning = true;
            else
            {
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
                    dto.IsRunnerOnFirst = false;
                    dto.IsRunnerOnSecond = true;
                }
            }
            
            return dto;
        }


    }
}

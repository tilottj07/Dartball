using AutoMapper;
using Dartball.BusinessLayer.GameEngine.Event.Dto;
using Dartball.BusinessLayer.GameEngine.Event.Interface;
using Dartball.BusinessLayer.GameEngine.Event.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.GameEngine.Event.Implementation
{
    public class GameEventDoublePlayService : IGameEventDoublePlayService
    {
        private IMapper Mapper;

        public GameEventDoublePlayService()
        {
            var mapConfig = new MapperConfiguration(c => c.CreateMap<IHalfInningActions, HalfInningActionsDto>());
            Mapper = mapConfig.CreateMapper();
        }



        public IHalfInningActions FillDoublePlayActions(IHalfInningActions actions)
        {
            HalfInningActionsDto dto = Mapper.Map<HalfInningActionsDto>(actions);

            if (HasBaseRunner(dto) == true) dto.TotalOuts += 2;
            else dto.TotalOuts++;

            if (dto.TotalOuts >= 3) dto.AdvanceToNextHalfInning = true;
            else
            {
                if (dto.IsRunnerOnFirst) dto.IsRunnerOnFirst = false;
                else if (dto.IsRunnerOnSecond) dto.IsRunnerOnSecond = false;
                else if (dto.IsRunnerOnThird) dto.IsRunnerOnThird = false;
            }

            return dto;
        }


        private bool HasBaseRunner(HalfInningActionsDto dto)
        {
            bool hasBaseRunner = false;

            if (dto.IsRunnerOnFirst) hasBaseRunner = true;
            if (dto.IsRunnerOnSecond) hasBaseRunner = true;
            if (dto.IsRunnerOnThird) hasBaseRunner = true;

            return hasBaseRunner;
        }

    }
}

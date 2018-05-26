using Dartball.BusinessLayer.Game.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Dartball.BusinessLayer.Game.Dto;
using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;

namespace Dartball.BusinessLayer.Game.Implementation
{
    public class GameInningService : IGameInningService
    {
        private IMapper Mapper;
        private DataLayer.Device.Repository.GameInningRepository Repository;

        public GameInningService()
        {
            var mapConfig = new MapperConfiguration(c => c.CreateMap<DataLayer.Device.Dto.GameInningDto, GameInningDto>());
            Mapper = mapConfig.CreateMapper();

            Repository = new DataLayer.Device.Repository.GameInningRepository();
        }


        public IGameInning GetGameInning(Guid gameAlternateKey, int inningNumber)
        {
            IGameInning gameInning = null;

            var dl = Repository.LoadByCompositeKey(gameAlternateKey, inningNumber);
            if (dl != null) gameInning = Mapper.Map<GameInningDto>(dl);

            return gameInning;
        }

        public List<IGameInning> GetGameInnings(Guid gameAlternateKey)
        {
            List<IGameInning> gameInnings = new List<IGameInning>();
            foreach(var item in Repository.LoadByGameAlternateKey(gameAlternateKey))
            {
                if (!item.DeleteDate.HasValue) gameInnings.Add(Mapper.Map<GameInningDto>(item));
            }
            return gameInnings;
        }

        public List<IGameInning> GetAllGameInnings()
        {
            List<IGameInning> gameInnings = new List<IGameInning>();
            foreach (var item in Repository.LoadAll())
            {
                if (!item.DeleteDate.HasValue) gameInnings.Add(Mapper.Map<GameInningDto>(item));
            }
            return gameInnings;
        }


        public ChangeResult AddNew(IGameInning gameInning)
        {
            return AddNew(new List<IGameInning> { gameInning });
        }
        public ChangeResult AddNew(List<IGameInning> gameInnings)
        {
            var result = Validate(gameInnings, isAddNew: true);
            if (result.IsSuccess)
            {
                foreach(var item in gameInnings)
                {
                    DataLayer.Device.Dto.GameInningDto dto = new DataLayer.Device.Dto.GameInningDto()
                    {
                        GameInningAlternateKey = item.GameInningAlternateKey == Guid.Empty ? Guid.NewGuid().ToString() : item.GameInningAlternateKey.ToString(),
                        GameAlternateKey = item.GameAlternateKey.ToString(),
                        InningNumber = item.InningNumber,
                        DeleteDate = item.DeleteDate
                    };
                    Repository.AddNew(dto);
                }
            }
            return result;
        }

        public ChangeResult Update(IGameInning gameInning)
        {
            return Update(new List<IGameInning> { gameInning });
        }
        public ChangeResult Update(List<IGameInning> gameInnings)
        {
            var result = Validate(gameInnings, isAddNew: false);
            if (result.IsSuccess)
            {
                foreach (var item in gameInnings)
                {
                    DataLayer.Device.Dto.GameInningDto dto = new DataLayer.Device.Dto.GameInningDto()
                    {
                        GameInningAlternateKey = item.GameInningAlternateKey.ToString(),
                        GameAlternateKey = item.GameAlternateKey.ToString(),
                        InningNumber = item.InningNumber,
                        DeleteDate = item.DeleteDate
                    };
                    Repository.Update(dto);
                }
            }
            return result;
        }




        private ChangeResult Validate(List<IGameInning> gameInnings, bool isAddNew = false)
        {
            ChangeResult result = new ChangeResult();
            foreach(var item in gameInnings)
            {
                if (!result.IsSuccess) break;

                if (item.GameAlternateKey == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid Game.");
                }

                if (item.InningNumber < 1)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add($"Invalid Inning: {item.InningNumber}.");
                }

                if (!isAddNew)
                {
                    if (item.GameInningAlternateKey == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid Game Inning Id.");
                    }
                }
            }
            return result;
        }




        public ChangeResult Remove(Guid gameAlternateKey, int inningNumber)
        {
            ChangeResult result = new ChangeResult();
            Repository.Delete(gameAlternateKey, inningNumber);
            return result;
        }

    }
}

using Dartball.BusinessLayer.Game.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Dartball.BusinessLayer.Game.Dto;
using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using System.Linq;

namespace Dartball.BusinessLayer.Game.Implementation
{
    public class GameInningService : IGameInningService
    {
        private IMapper Mapper;

        public GameInningService()
        {
            var mapConfig = new MapperConfiguration(c => c.CreateMap<Domain.GameInning, GameInningDto>());
            Mapper = mapConfig.CreateMapper();
        }


        public IGameInning GetGameInning(Guid gameInningId)
        {
            IGameInning gameInning = null;

            using (var context = new Data.DartballContext())
            {
                var item = context.GameInnings.FirstOrDefault(x => x.GameInningId == gameInningId.ToString());
                if (item != null) gameInning = Mapper.Map<GameInningDto>(item);
            }

            return gameInning;
        }

        public IGameInning GetGameInning(Guid gameId, int inningNumber)
        {
            IGameInning gameInning = null;

            using (var context = new Data.DartballContext())
            {
                var item = context.GameInnings.FirstOrDefault(x => x.GameId == gameId.ToString() && x.InningNumber == inningNumber);
                if (item != null) gameInning = Mapper.Map<GameInningDto>(item);
            }

            return gameInning;
        }

        public List<IGameInning> GetGameInnings(Guid gameId)
        {
            List<IGameInning> gameInnings = new List<IGameInning>();
            using (var context = new Data.DartballContext())
            {
                var items = context.GameInnings.Where(x => x.GameId == gameId.ToString() && !x.DeleteDate.HasValue).ToList();
                foreach (var item in items) gameInnings.Add(Mapper.Map<GameInningDto>(item));
            }
            return gameInnings;
        }

        public List<IGameInning> GetAllGameInnings()
        {
            List<IGameInning> gameInnings = new List<IGameInning>();
            using (var context = new Data.DartballContext())
            {
                var items = context.GameInnings.ToList().Where(x => !x.DeleteDate.HasValue);
                foreach (var item in items) gameInnings.Add(Mapper.Map<GameInningDto>(item));
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
                using (var context = new Data.DartballContext())
                {
                    foreach (var item in gameInnings)
                    {
                        context.GameInnings.Add(new Domain.GameInning()
                        {
                            GameInningId = item.GameInningId == Guid.Empty ? Guid.NewGuid().ToString() : item.GameInningId.ToString(),
                            GameId = item.GameId.ToString(),
                            InningNumber = item.InningNumber,
                            DeleteDate = item.DeleteDate
                        });
                    }
                    context.SaveChanges();
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
                using (var context = new Data.DartballContext())
                {
                    foreach (var item in gameInnings)
                    {
                        context.GameInnings.Update(new Domain.GameInning()
                        {
                            GameInningId = item.GameInningId.ToString(),
                            GameId = item.GameId.ToString(),
                            InningNumber = item.InningNumber,
                            DeleteDate = item.DeleteDate
                        });
                    }
                    context.SaveChanges();
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

                if (item.GameId == Guid.Empty)
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
                    if (item.GameInningId == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid Game Inning Id.");
                    }
                }
            }
            return result;
        }




        public ChangeResult Remove(Guid gameId, int inningNumber)
        {
            ChangeResult result = new ChangeResult();
            using (var context = new Data.DartballContext())
            {
                var item = context.GameInnings.FirstOrDefault(x => x.GameId == gameId.ToString() && x.InningNumber == inningNumber);
                if (item != null)
                {
                    context.GameInnings.Remove(item);
                    context.SaveChanges();
                }
            }
            return result;
        }

    }
}

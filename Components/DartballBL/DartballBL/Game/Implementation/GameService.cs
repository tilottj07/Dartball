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
    public class GameService : IGameService
    {
        private IMapper Mapper;

        public GameService()
        {
            var mapConfig = new MapperConfiguration(c => c.CreateMap<Domain.Game, GameDto>());
            Mapper = mapConfig.CreateMapper();
        }



        public IGame GetGame(Guid gameId)
        {
            IGame game = null;

            using (var context = new Data.DartballContext())
            {
                var item = context.Games.FirstOrDefault(x => x.GameId == gameId.ToString());
                if (item != null) game = Mapper.Map<GameDto>(item);
            }

            return game;
        }

        public List<IGame> GetLeagueGames(Guid leagueId)
        {
            List<IGame> games = new List<IGame>();

            using (var context = new Data.DartballContext())
            {
                var items = context.Games.Where(x => x.LeagueId == leagueId.ToString() && !x.DeleteDate.HasValue).ToList();
                foreach (var item in items) games.Add(Mapper.Map<GameDto>(item));
            }

            return games;
        }

        public List<IGame> GetAllGames()
        {
            List<IGame> games = new List<IGame>();

            using (var context = new Data.DartballContext())
            {
                var items = context.Games.Where(x => !x.DeleteDate.HasValue).ToList();
                foreach (var item in items) games.Add(Mapper.Map<GameDto>(item));
            }

            return games;
        }



        public ChangeResult AddNew(IGame game)
        {
            return AddNew(new List<IGame> { game });
        }
        public ChangeResult AddNew(List<IGame> games)
        {
            var result = Validate(games, isAddNew: true);
            if (result.IsSuccess)
            {
                using (var context = new Data.DartballContext())
                {
                    foreach (var item in games)
                    {
                        context.Games.Add(new Domain.Game()
                        {
                            GameId = item.GameId == Guid.Empty ? Guid.NewGuid().ToString() : item.GameId.ToString(),
                            LeagueId = item.LeagueId.ToString(),
                            GameDate = item.GameDate,
                            DeleteDate = item.DeleteDate
                        });
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }


        public ChangeResult Update(IGame game)
        {
            return Update(new List<IGame> { game });
        }
        public ChangeResult Update(List<IGame> games)
        {
            var result = Validate(games, isAddNew: false);
            if (result.IsSuccess)
            {
                using (var context = new Data.DartballContext())
                {
                    foreach (var item in games)
                    {
                        context.Games.Update(new Domain.Game()
                        {
                            GameId = item.GameId == Guid.Empty ? Guid.NewGuid().ToString() : item.GameId.ToString(),
                            LeagueId = item.LeagueId.ToString(),
                            GameDate = item.GameDate,
                            DeleteDate = item.DeleteDate
                        });
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }




        private ChangeResult Validate(List<IGame> games, bool isAddNew = false)
        {
            ChangeResult result = new ChangeResult();

            foreach(var item in games)
            {
                if (!result.IsSuccess) break;

                if (item.LeagueId == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid League.");
                }

                if (item.GameDate > DateTime.Today.AddDays(1))
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Game cannot take place in the future.");
                }

                if (isAddNew == false)
                {
                    if (item.GameId == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid Game.");
                    }
                }
            }

            return result;
        }



        public ChangeResult Remove(Guid gameId)
        {
            ChangeResult result = new ChangeResult();
            using (var context = new Data.DartballContext())
            {
                var item = context.Games.FirstOrDefault(x => x.GameId == gameId.ToString());
                if (item != null)
                {
                    context.Games.Remove(item);
                    context.SaveChanges();
                }
            }
            return result;
        }


        

    }
}

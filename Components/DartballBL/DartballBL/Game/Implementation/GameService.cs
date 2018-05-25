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
    public class GameService : IGameService
    {
        private IMapper Mapper;
        private DataLayer.Device.Repository.GameRepository Repository;

        public GameService()
        {
            var mapConfig = new MapperConfiguration(c => c.CreateMap<DataLayer.Device.Dto.GameDto, GameDto>());
            Mapper = mapConfig.CreateMapper();

            Repository = new DataLayer.Device.Repository.GameRepository();
        }



        public IGame GetGame(Guid gameAlternateKey)
        {
            IGame game = null;

            var dl = Repository.LoadByKey(gameAlternateKey);
            if (dl != null) game = Mapper.Map<GameDto>(dl);

            return game;
        }

        public List<IGame> GetLeagueGames(Guid leagueAlternateKey)
        {
            List<IGame> games = new List<IGame>();

            foreach(var item in Repository.LoadByLeagueAlternateKey(leagueAlternateKey))
            {
                if (!item.DeleteDate.HasValue) games.Add(Mapper.Map<GameDto>(item));
            }

            return games;
        }

        public List<IGame> GetAllGames()
        {
            List<IGame> games = new List<IGame>();

            foreach (var item in Repository.LoadAll())
            {
                if (!item.DeleteDate.HasValue) games.Add(Mapper.Map<GameDto>(item));
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
                foreach(var item in games)
                {
                    DataLayer.Device.Dto.GameDto dto = new DataLayer.Device.Dto.GameDto()
                    {
                        GameAlternateKey = item.GameAlternateKey == Guid.Empty ? Guid.NewGuid().ToString() : item.GameAlternateKey.ToString(),
                        LeagueAlternateKey = item.LeagueAlternateKey.ToString(),
                        GameDate = item.GameDate,
                        DeleteDate = item.DeleteDate
                    };
                    Repository.AddNew(dto);
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
                foreach (var item in games)
                {
                    DataLayer.Device.Dto.GameDto dto = new DataLayer.Device.Dto.GameDto()
                    {
                        GameAlternateKey = item.GameAlternateKey == Guid.Empty ? Guid.NewGuid().ToString() : item.GameAlternateKey.ToString(),
                        LeagueAlternateKey = item.LeagueAlternateKey.ToString(),
                        GameDate = item.GameDate,
                        DeleteDate = item.DeleteDate
                    };
                    Repository.Update(dto);
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

                if (item.LeagueAlternateKey == Guid.Empty)
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
                    if (item.GameAlternateKey == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid Game.");
                    }
                }
            }

            return result;
        }



        public ChangeResult Remove(Guid gameAlternateKey)
        {
            ChangeResult result = new ChangeResult();
            Repository.Delete(gameAlternateKey);
            return result;
        }


        

    }
}

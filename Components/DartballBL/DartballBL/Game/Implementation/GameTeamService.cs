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
    public class GameTeamService : IGameTeamService
    {
        private IMapper Mapper;
        private DataLayer.Device.Repository.GameTeamRepository Repository;

        public GameTeamService()
        {
            var mapConfig = new MapperConfiguration(c => c.CreateMap<DataLayer.Device.Dto.GameTeamDto, GameTeamDto>());
            Mapper = mapConfig.CreateMapper();

            Repository = new DataLayer.Device.Repository.GameTeamRepository();
        }



        public IGameTeam GetGameTeam(Guid gameAlternateKey, Guid teamAlternateKey)
        {
            IGameTeam gameTeam = null;

            var dl = Repository.LoadByCompositeKey(gameAlternateKey, teamAlternateKey);
            if (dl != null) gameTeam = Mapper.Map<GameTeamDto>(dl);

            return gameTeam;
        }

        public List<IGameTeam> GetGameTeams(Guid gameAlternateKey)
        {
            List<IGameTeam> gameTeams = new List<IGameTeam>();

            foreach(var item in Repository.LoadByGameAlternateKey(gameAlternateKey))
            {
                if (!item.DeleteDate.HasValue) gameTeams.Add(Mapper.Map<GameTeamDto>(item));
            }

            return gameTeams;
        }

        public List<IGameTeam> GetAllGames()
        {
            List<IGameTeam> gameTeams = new List<IGameTeam>();

            foreach(var item in Repository.LoadAll())
            {
                if (!item.DeleteDate.HasValue) gameTeams.Add(Mapper.Map<GameTeamDto>(item));
            }

            return gameTeams;
        }



        public ChangeResult AddNew(IGameTeam gameTeam)
        {
            return AddNew(new List<IGameTeam> { gameTeam });
        }
        public ChangeResult AddNew(List<IGameTeam> gameTeams)
        {
            var result = Validate(gameTeams, isAddNew: true);
            if (result.IsSuccess)
            {
                foreach(var item in gameTeams)
                {
                    DataLayer.Device.Dto.GameTeamDto dto = new DataLayer.Device.Dto.GameTeamDto()
                    {
                        GameTeamAlternateKey = item.GameTeamAlternateKey == Guid.Empty ? Guid.NewGuid().ToString() : item.GameTeamAlternateKey.ToString(),
                        GameAlternateKey = item.GameAlternateKey.ToString(),
                        TeamAlternateKey = item.TeamAlternateKey.ToString(),
                        DeleteDate = item.DeleteDate
                    };
                    Repository.AddNew(dto);
                }
            }
            return result;
        }

        public ChangeResult Update(IGameTeam gameTeam)
        {
            return Update(new List<IGameTeam> { gameTeam });
        }
        public ChangeResult Update(List<IGameTeam> gameTeams)
        {
            var result = Validate(gameTeams, isAddNew: false);
            if (result.IsSuccess)
            {
                foreach (var item in gameTeams)
                {
                    DataLayer.Device.Dto.GameTeamDto dto = new DataLayer.Device.Dto.GameTeamDto()
                    {
                        GameTeamAlternateKey = item.GameTeamAlternateKey.ToString(),
                        GameAlternateKey = item.GameAlternateKey.ToString(),
                        TeamAlternateKey = item.TeamAlternateKey.ToString(),
                        DeleteDate = item.DeleteDate
                    };
                    Repository.Update(dto);
                }
            }
            return result;
        }



        private ChangeResult Validate(List<IGameTeam> gameTeams, bool isAddNew = false)
        {
            ChangeResult result = new ChangeResult();

            foreach (var item in gameTeams)
            {
                if (!result.IsSuccess) break;

                if (item.GameAlternateKey == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid Game.");
                }

                if (item.TeamAlternateKey == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid Team.");
                }

                if (isAddNew == false)
                {
                    if (item.GameTeamAlternateKey == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid Game Team Id.");
                    }
                }
            }

            return result;
        }



        public ChangeResult Remove(Guid gameAlternateKey, Guid teamAlternateKey)
        {
            ChangeResult result = new ChangeResult();
            Repository.Delete(gameAlternateKey, teamAlternateKey);
            return result;
        }

    }
}

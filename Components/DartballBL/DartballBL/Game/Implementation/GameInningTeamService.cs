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
    public class GameInningTeamService : IGameInningTeamService
    {
        private IMapper Mapper;
        private DataLayer.Device.Repository.GameInningTeamRepository Repository;

        public GameInningTeamService()
        {
            var mapConfig = new MapperConfiguration(c => c.CreateMap<DataLayer.Device.Dto.GameInningTeamDto, GameInningTeamDto>());
            Mapper = mapConfig.CreateMapper();

            Repository = new DataLayer.Device.Repository.GameInningTeamRepository();
        }

        public IGameInningTeam GetGameInningTeam(Guid gameTeamAlternateKey, Guid gameInningAlternateKey)
        {
            IGameInningTeam gameInningTeam = null;

            var dl = Repository.LoadByCompositeKey(gameTeamAlternateKey, gameInningAlternateKey);
            if (dl != null) gameInningTeam = Mapper.Map<GameInningTeamDto>(dl);

            return gameInningTeam;
        }

        public List<IGameInningTeam> GetInningTeams(Guid gameInningAlternateKey)
        {
            List<IGameInningTeam> gameInningTeams = new List<IGameInningTeam>();
            foreach(var item in Repository.LoadByGameInningAlternateKey(gameInningAlternateKey))
            {
                if (!item.DeleteDate.HasValue) gameInningTeams.Add(Mapper.Map<GameInningTeamDto>(item));
            }

            return gameInningTeams;
        }

        public List<IGameInningTeam> GetTeamInnings(Guid gameTeamAlternateKey)
        {
            List<IGameInningTeam> gameInningTeams = new List<IGameInningTeam>();
            foreach (var item in Repository.LoadByGameTeamAlternateKey(gameTeamAlternateKey))
            {
                if (!item.DeleteDate.HasValue) gameInningTeams.Add(Mapper.Map<GameInningTeamDto>(item));
            }

            return gameInningTeams;
        }



        public ChangeResult AddNew(IGameInningTeam gameInningTeam)
        {
            return AddNew(new List<IGameInningTeam> { gameInningTeam });
        }
        public ChangeResult AddNew(List<IGameInningTeam> gameInningTeams)
        {
            var result = Validate(gameInningTeams, isAddNew: true);
            if (result.IsSuccess)
            {
                foreach(var item in gameInningTeams)
                {
                    DataLayer.Device.Dto.GameInningTeamDto dto = new DataLayer.Device.Dto.GameInningTeamDto()
                    {
                        GameInningTeamAlternateKey = item.GameInningTeamAlternateKey == Guid.Empty ? Guid.NewGuid().ToString() : item.GameInningTeamAlternateKey.ToString(),
                        GameInningAlternateKey = item.GameInningAlternateKey.ToString(),
                        GameTeamAlternateKey = item.GameTeamAlternateKey.ToString(),
                        Score = item.Score,
                        Outs = item.Outs,
                        IsRunnerOnFirst = item.IsRunnerOnFirst ? 1 : 0,
                        IsRunnerOnSecond = item.IsRunnerOnSecond ? 1 : 0,
                        IsRunnerOnThird = item.IsRunnerOnThird ? 1 : 0,
                        DeleteDate = item.DeleteDate
                    };
                    Repository.AddNew(dto);
                }
            }
            return result;
        }

        public ChangeResult Update(IGameInningTeam gameInningTeam)
        {
            return Update(new List<IGameInningTeam> { gameInningTeam });
        }
        public ChangeResult Update(List<IGameInningTeam> gameInningTeams)
        {
            var result = Validate(gameInningTeams, isAddNew: false);
            if (result.IsSuccess)
            {
                foreach (var item in gameInningTeams)
                {
                    DataLayer.Device.Dto.GameInningTeamDto dto = new DataLayer.Device.Dto.GameInningTeamDto()
                    {
                        GameInningTeamAlternateKey = item.GameInningTeamAlternateKey.ToString(),
                        GameInningAlternateKey = item.GameInningAlternateKey.ToString(),
                        GameTeamAlternateKey = item.GameTeamAlternateKey.ToString(),
                        Score = item.Score,
                        Outs = item.Outs,
                        IsRunnerOnFirst = item.IsRunnerOnFirst ? 1 : 0,
                        IsRunnerOnSecond = item.IsRunnerOnSecond ? 1 : 0,
                        IsRunnerOnThird = item.IsRunnerOnThird ? 1 : 0,
                        DeleteDate = item.DeleteDate
                    };
                    Repository.Update(dto);
                }
            }
            return result;
        }



        private ChangeResult Validate(List<IGameInningTeam> gameInningTeams, bool isAddNew = false)
        {
            ChangeResult result = new ChangeResult();
            foreach(var item in gameInningTeams)
            {
                if (!result.IsSuccess) break;

                if (item.GameInningAlternateKey == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid Inning Id.");
                }
                if (item.GameTeamAlternateKey == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid Game Team Id.");
                }

                if (item.Outs < 0 || item.Outs > 3)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add($"Invalid Number of Outs: {item.Outs}");
                }

                if (item.Score < 0)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid Score, cannot be less than 0.");
                }

                if (isAddNew == false)
                {
                    if (item.GameInningTeamAlternateKey == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid Game Inning Team Id.");
                    }
                }

            }
            return result;
        }



        public ChangeResult Remove(Guid gameInningAlternateKey, Guid gameTeamAlternateKey)
        {
            ChangeResult result = new ChangeResult();
            Repository.Delete(gameTeamAlternateKey, gameInningAlternateKey);
            return result;
        }

    }
}

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
    public class GameInningTeamService : IGameInningTeamService
    {
        private IMapper Mapper;

        public GameInningTeamService()
        {
            var mapConfig = new MapperConfiguration(c => c.CreateMap<Domain.GameInningTeam, GameInningTeamDto>());
            Mapper = mapConfig.CreateMapper();
        }

        public IGameInningTeam GetGameInningTeam(Guid gameInningTeamId)
        {
            IGameInningTeam gameInningTeam = null;

            using (var context = new Data.DartballContext())
            {
                var item = context.GameInningTeams.FirstOrDefault(x => x.GameInningTeamId == gameInningTeamId.ToString());
                if (item != null) gameInningTeam = Mapper.Map<GameInningTeamDto>(item);
            }

            return gameInningTeam;
        }

        public IGameInningTeam GetGameInningTeam(Guid gameTeamId, Guid gameInningId)
        {
            IGameInningTeam gameInningTeam = null;

            using (var context = new Data.DartballContext())
            {
                var item = context.GameInningTeams.FirstOrDefault(x => x.GameTeamId == gameTeamId.ToString() && x.GameInningId == gameInningId.ToString());
                if (item != null) gameInningTeam = Mapper.Map<GameInningTeamDto>(item);
            }

            return gameInningTeam;
        }

        public List<IGameInningTeam> GetInningTeams(Guid gameInningId)
        {
            List<IGameInningTeam> gameInningTeams = new List<IGameInningTeam>();
            using (var context = new Data.DartballContext())
            {
                var items = context.GameInningTeams.Where(x => x.GameInningId == gameInningId.ToString() && !x.DeleteDate.HasValue).ToList();
                foreach (var item in items) gameInningTeams.Add(Mapper.Map<GameInningTeamDto>(item));
            }

            return gameInningTeams;
        }

        public List<IGameInningTeam> GetTeamInnings(Guid gameTeamId)
        {
            List<IGameInningTeam> gameInningTeams = new List<IGameInningTeam>();
            using (var context = new Data.DartballContext())
            {
                var items = context.GameInningTeams.Where(x => x.GameTeamId == gameTeamId.ToString() && !x.DeleteDate.HasValue).ToList();
                foreach (var item in items) gameInningTeams.Add(Mapper.Map<GameInningTeamDto>(item));
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
                using (var context = new Data.DartballContext())
                {
                    foreach (var item in gameInningTeams)
                    {
                        context.GameInningTeams.Add(new Domain.GameInningTeam()
                        {
                            GameInningTeamId = item.GameInningTeamId == Guid.Empty ? Guid.NewGuid().ToString() : item.GameInningTeamId.ToString(),
                            GameInningId = item.GameInningId.ToString(),
                            GameTeamId = item.GameTeamId.ToString(),
                            Score = item.Score,
                            Outs = item.Outs,
                            IsRunnerOnFirst = item.IsRunnerOnFirst ? 1 : 0,
                            IsRunnerOnSecond = item.IsRunnerOnSecond ? 1 : 0,
                            IsRunnerOnThird = item.IsRunnerOnThird ? 1 : 0,
                            DeleteDate = item.DeleteDate
                        });
                    }
                    context.SaveChanges();
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
                using (var context = new Data.DartballContext())
                {
                    foreach (var item in gameInningTeams)
                    {
                        context.GameInningTeams.Update(new Domain.GameInningTeam()
                        {
                            GameInningTeamId = item.GameInningTeamId.ToString(),
                            GameInningId = item.GameInningId.ToString(),
                            GameTeamId = item.GameTeamId.ToString(),
                            Score = item.Score,
                            Outs = item.Outs,
                            IsRunnerOnFirst = item.IsRunnerOnFirst ? 1 : 0,
                            IsRunnerOnSecond = item.IsRunnerOnSecond ? 1 : 0,
                            IsRunnerOnThird = item.IsRunnerOnThird ? 1 : 0,
                            DeleteDate = item.DeleteDate
                        });
                    }
                    context.SaveChanges();
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

                if (item.GameInningId == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid Inning Id.");
                }
                if (item.GameTeamId == Guid.Empty)
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
                    if (item.GameInningTeamId == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid Game Inning Team Id.");
                    }
                }

            }
            return result;
        }



        public ChangeResult Remove(Guid gameInningId, Guid gameTeamId)
        {
            ChangeResult result = new ChangeResult();
            using (var context = new Data.DartballContext())
            {
                var item = context.GameInningTeams.FirstOrDefault(x => x.GameInningId == gameInningId.ToString() && x.GameTeamId == gameTeamId.ToString());
                if (item != null)
                {
                    context.GameInningTeams.Remove(item);
                    context.SaveChanges();
                }
            }
            return result;
        }

    }
}

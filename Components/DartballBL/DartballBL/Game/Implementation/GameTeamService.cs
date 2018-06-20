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
    public class GameTeamService : IGameTeamService
    {
        private IMapper Mapper;

        public GameTeamService()
        {
            var mapConfig = new MapperConfiguration(c => c.CreateMap<Domain.GameTeam, GameTeamDto>());
            Mapper = mapConfig.CreateMapper();
        }



        public IGameTeam GetGameTeam(Guid gameId, Guid teamId)
        {
            IGameTeam gameTeam = null;

            using (var context = new Data.DartballContext())
            {
                var item = context.GameTeams.FirstOrDefault(x => x.GameId == gameId.ToString() && x.TeamId == teamId.ToString());
                if (item != null) gameTeam = Mapper.Map<GameTeamDto>(item);
            }

                return gameTeam;
        }

        public List<IGameTeam> GetGameTeams(Guid gameId)
        {
            List<IGameTeam> gameTeams = new List<IGameTeam>();

            using (var context = new Data.DartballContext())
            {
                var items = context.GameTeams.Where(x => x.GameId == gameId.ToString() && !x.DeleteDate.HasValue).ToList();
                foreach (var item in items) gameTeams.Add(Mapper.Map<GameTeamDto>(item));
            }
            return gameTeams;
        }

        public List<IGameTeam> GetAllGames()
        {
            List<IGameTeam> gameTeams = new List<IGameTeam>();

            using (var context = new Data.DartballContext())
            {
                var items = context.Games.Where(x => !x.DeleteDate.HasValue).ToList();
                foreach (var item in items) gameTeams.Add(Mapper.Map<GameTeamDto>(item));
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
                using (var context = new Data.DartballContext())
                {
                    foreach (var item in gameTeams)
                    {
                        context.GameTeams.Add(new Domain.GameTeam()
                        {
                            GameTeamId = item.GameTeamId == Guid.Empty ? Guid.NewGuid().ToString() : item.GameTeamId.ToString(),
                            GameId = item.GameId.ToString(),
                            TeamId = item.TeamId.ToString(),
                            DeleteDate = item.DeleteDate
                        });
                    }
                    context.SaveChanges();
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
                using (var context = new Data.DartballContext())
                {
                    foreach (var item in gameTeams)
                    {
                        context.GameTeams.Update(new Domain.GameTeam()
                        {
                            GameTeamId = item.GameTeamId.ToString(),
                            GameId = item.GameId.ToString(),
                            TeamId = item.TeamId.ToString(),
                            DeleteDate = item.DeleteDate
                        });
                    }
                    context.SaveChanges();
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

                if (item.GameId == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid Game.");
                }

                if (item.TeamId == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid Team.");
                }

                if (isAddNew == false)
                {
                    if (item.GameTeamId == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid Game Team Id.");
                    }
                }
            }

            return result;
        }



        public ChangeResult Remove(Guid gameId, Guid teamId)
        {
            ChangeResult result = new ChangeResult();
            using (var context = new Data.DartballContext())
            {
                var item = context.GameTeams.FirstOrDefault(x => x.GameId == gameId.ToString() && x.TeamId == teamId.ToString());
                if (item != null)
                {
                    context.GameTeams.Remove(item);
                    context.SaveChanges();
                }
            }
            return result;
        }

    }
}

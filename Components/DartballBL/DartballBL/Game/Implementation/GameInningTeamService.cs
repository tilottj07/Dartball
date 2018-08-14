using Dartball.BusinessLayer.Game.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Dartball.BusinessLayer.Game.Dto;
using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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


        public List<IGameInningTeam> GetTeamInningsForEntireGame(Guid gameId)
        {
            List<IGameInningTeam> list = new List<IGameInningTeam>();

            using (var context = new Data.DartballContext())
            {
                var items = (from g in context.Games
                             join gi in context.GameInnings on g.GameId equals gi.GameId
                             join git in context.GameInningTeams on gi.GameInningId equals git.GameInningId
                             where g.GameId == gameId.ToString()
                             && !g.DeleteDate.HasValue
                             && !gi.DeleteDate.HasValue
                             && !git.DeleteDate.HasValue
                             select git).ToList();

                foreach (var item in items)
                    list.Add(Mapper.Map<GameInningTeamDto>(item));
            }

            return list;
        }


        public IGameInningTeam GetCurrentGameInningTeam(Guid gameId) {
            IGameInningTeam gameInningTeam = null;
            using(var context = new Data.DartballContext()) {
                var items = (from g in context.Games
                            join gi in context.GameInnings on g.GameId equals gi.GameId
                            join gt in context.GameTeams on g.GameId equals gt.GameId
                            join git in context.GameInningTeams on gi.GameInningId equals git.GameInningId
                            where g.GameId == gameId.ToString()
                             && !g.DeleteDate.HasValue
                             && !gi.DeleteDate.HasValue
                             && !gt.DeleteDate.HasValue
                             && !git.DeleteDate.HasValue
                            orderby gi.InningNumber descending 
                            orderby gt.TeamBattingSequence descending
                            select git).Take(1);

                if (items != null && items.Count() > 0) gameInningTeam = Mapper.Map<GameInningTeamDto>(items.FirstOrDefault());
            }

            return gameInningTeam;
        }


        public Guid? GetNextAtBatTeamId(Guid gameId)
        {
            Guid? nextAtBatTeamId = null;

            using (var context = new Data.DartballContext())
            {
                var gameTeams = context.GameTeams.Where(x => x.GameId == gameId.ToString() && !x.DeleteDate.HasValue)
                                           .OrderBy(x => x.TeamBattingSequence).ToList();

                var currentInning = context.GameInnings
                                           .Where(x => x.GameId == gameId.ToString() && !x.DeleteDate.HasValue)
                                           .OrderByDescending(x => x.InningNumber).FirstOrDefault();
                int maxInning = 1;
                if (currentInning != null) maxInning = currentInning.InningNumber;

                var inningTeams = context.GameInningTeams
                                          .Where(x => x.GameInning.GameId == gameId.ToString() && !x.DeleteDate.HasValue && !x.GameInning.DeleteDate.HasValue)
                                          .Include(x => x.GameInning).ToList();
                
                var currentInningTeams = inningTeams
                    .Where(x => x.GameInning.InningNumber == maxInning).ToList();

                Guid? lastGameTeamId = null;
                List<string> currentInningGameTeamIds = currentInningTeams.Select(x => x.GameTeamId).ToList();
                var reverseOrderGameTeams = gameTeams.OrderByDescending(x => x.TeamBattingSequence).ToList();

                foreach(var gt in reverseOrderGameTeams) {
                    if (currentInningGameTeamIds.Contains(gt.GameTeamId)) {
                        lastGameTeamId = Guid.Parse(gt.GameTeamId);
                        break;
                    }
                }

                Guid? lastTeamId = null;
                if (lastGameTeamId.HasValue) {
                    var gt = gameTeams.FirstOrDefault(x => x.GameTeamId == lastGameTeamId.Value.ToString());
                    if (gt != null) lastTeamId = Guid.Parse(gt.TeamId);
                }

                if (!lastTeamId.HasValue)
                {
                    //get the first team in the sequence 
                    var gameTeam = gameTeams.FirstOrDefault();

                    if (gameTeam != null)
                        nextAtBatTeamId = Guid.Parse(gameTeam.TeamId);
                }
                else
                {
                    //get the next team in the sequence
                    var lastGameTeam = gameTeams.FirstOrDefault(x => x.TeamId == lastTeamId.Value.ToString());
                    if (lastGameTeam != null)
                    {
                        int gtIndex = gameTeams.IndexOf(lastGameTeam);
                        if (gtIndex < (gameTeams.Count - 1))
                        {
                            nextAtBatTeamId = Guid.Parse(gameTeams[gtIndex + 1].TeamId);
                        }
                        else
                        {
                            nextAtBatTeamId = Guid.Parse(gameTeams.FirstOrDefault().TeamId);
                        }
                    }
                }

            }
            return nextAtBatTeamId;
        }



        public ChangeResult Save(IGameInningTeam gameInningTeam) {
            bool isAdd = GetGameInningTeam(gameInningTeam.GameTeamId, gameInningTeam.GameInningId) == null;

            if (isAdd) return AddNew(gameInningTeam);
            return Update(gameInningTeam);
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

                if (isAddNew == false) {
                    if (item.GameInningTeamId == Guid.Empty) {
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

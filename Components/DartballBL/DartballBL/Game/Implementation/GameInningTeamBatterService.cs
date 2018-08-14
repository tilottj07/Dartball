using Dartball.BusinessLayer.Game.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Dartball.BusinessLayer.Game.Dto;
using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Dartball.BusinessLayer.Game.Implementation
{
    public class GameInningTeamBatterService : IGameInningTeamBatterService
    {
        private IMapper Mapper;
        private IGameEventService EventService;

        public GameInningTeamBatterService()
        {
            var mapConfig = new MapperConfiguration(c => c.CreateMap<Domain.GameInningTeamBatter, GameInningTeamBatterDto>());
            Mapper = mapConfig.CreateMapper();

            EventService = new GameEventService();
        }



        public IGameInningTeamBatter GetGameInningTeamBatter(Guid gameInningTeamId, int sequence)
        {
            IGameInningTeamBatter gameInningTeamBatter = null;

            using (var context = new Data.DartballContext())
            {
                var item = context.GameInningTeamBatters.FirstOrDefault(x => x.GameInningTeamId == gameInningTeamId.ToString() && x.Sequence == sequence);
                if (item != null) gameInningTeamBatter = Mapper.Map<GameInningTeamBatterDto>(item);
            }

            return gameInningTeamBatter;
        }

        public List<IGameInningTeamBatter> GetGameInningTeamBatters(Guid gameInningTeamId)
        {
            List<IGameInningTeamBatter> gameInningTeamBatters = new List<IGameInningTeamBatter>();

            using (var context = new Data.DartballContext())
            {
                var items = context.GameInningTeamBatters.Where(x => x.GameInningTeamId == gameInningTeamId.ToString() 
                    && !x.DeleteDate.HasValue).OrderBy(x => x.Sequence).ToList();

                foreach (var item in items) gameInningTeamBatters.Add(Mapper.Map<GameInningTeamBatterDto>(item));
            }

            return gameInningTeamBatters;
        }

        public List<IGameInningTeamBatter> GetGamePlayerAtBats(Guid gameId, Guid playerId)
        {
            List<IGameInningTeamBatter> gameInningTeamBatters = new List<IGameInningTeamBatter>();

            using (var context = new Data.DartballContext())
            {
                var items = context.GameInningTeamBatters.Where(x => x.GameInningTeam.GameInning.GameId == gameId.ToString() &&
                    x.PlayerId == playerId.ToString() && !x.DeleteDate.HasValue).ToList();

                foreach (var item in items) gameInningTeamBatters.Add(Mapper.Map<GameInningTeamBatterDto>(item));
            }
            return gameInningTeamBatters;
        }


        public IGameInningTeamBatter GetCurrentGameInningTeamBatter(Guid gameId)
        {
            IGameInningTeamBatter gameInningTeamBatter = null;
            using (var context = new Data.DartballContext())
            {
                var currentInning = context.GameInnings
                                           .Where(x => x.GameId == gameId.ToString() && !x.DeleteDate.HasValue)
                                           .OrderByDescending(x => x.InningNumber).FirstOrDefault();
                if (currentInning != null)
                {
                    var gameTeamsInReverseSequence = context.GameTeams
                                           .Where(x => x.GameId == gameId.ToString() && !x.DeleteDate.HasValue)
                                           .OrderByDescending(x => x.TeamBattingSequence).ToList();

                    var inningTeamsBatters = context.GameInningTeamBatters
                                                    .Where(x => x.GameInningTeam.GameInningId == currentInning.GameInningId && !x.DeleteDate.HasValue && !x.DeleteDate.HasValue)
                                                    .Include(x => x.GameInningTeam)
                                                    .OrderByDescending(x => x.Sequence).ToList();

                    foreach (var gt in gameTeamsInReverseSequence)
                    {
                        var teamBatters = inningTeamsBatters.Where(x => x.GameInningTeam.GameTeamId == gt.GameTeamId).ToList();
                        if (teamBatters.Count > 0)
                        {
                            gameInningTeamBatter = Mapper.Map<GameInningTeamBatterDto>(teamBatters.FirstOrDefault());
                            break;
                        }
                    }
                }
            }

            return gameInningTeamBatter;
        }


        public Guid? GetNextGameBatterPlayerId(Guid gameId, Guid teamId)
        {
            Guid? nextAtBatPlayerId = null;

            using (var context = new Data.DartballContext())
            {
                Guid? lastAtBatPlayerId = null;
                var lastTeamBatter = (from gt in context.GameTeams
                                      join tpl in context.TeamPlayerLineups on gt.TeamId equals tpl.TeamId
                                      join gi in context.GameInnings on gt.GameId equals gi.GameId
                                      join git in context.GameInningTeams on gi.GameInningId equals git.GameInningId
                                      join gitb in context.GameInningTeamBatters on git.GameInningTeamId equals gitb.GameInningTeamId
                                      where gt.GameId == gameId.ToString()
                                      && gt.TeamId == teamId.ToString()
                                      && !gt.DeleteDate.HasValue
                                      && !tpl.DeleteDate.HasValue
                                      && !gi.DeleteDate.HasValue
                                      && !git.DeleteDate.HasValue
                                      && !gitb.DeleteDate.HasValue
                                      orderby gi.InningNumber descending
                                      orderby gt.TeamBattingSequence descending
                                      orderby gitb.Sequence descending
                                      select new { LastAtBatPlayerId = gitb.PlayerId }).Take(1);

                if (lastTeamBatter != null && lastTeamBatter.Count() > 0)
                    lastAtBatPlayerId = Guid.Parse(lastTeamBatter.FirstOrDefault().LastAtBatPlayerId);

                if (!lastAtBatPlayerId.HasValue)
                {
                    //next up is the first player in the team's lineup
                    var firstBatterInLineup = context.TeamPlayerLineups
                                                     .Where(x => x.TeamId == teamId.ToString() && !x.DeleteDate.HasValue)
                                                     .OrderBy(x => x.BattingOrder).FirstOrDefault();

                    if (firstBatterInLineup != null) nextAtBatPlayerId = Guid.Parse(firstBatterInLineup.PlayerId);
                }
                else
                {
                    //find the next player in the team's lineup
                    var orderedLineup = context.TeamPlayerLineups
                                               .Where(x => x.TeamId == teamId.ToString() && !x.DeleteDate.HasValue)
                                               .OrderBy(x => x.BattingOrder).ToList();

                    var lastBatter = orderedLineup.FirstOrDefault(x => x.PlayerId == lastAtBatPlayerId.Value.ToString());
                    if (lastBatter != null) {
                        int batterIndex = orderedLineup.IndexOf(lastBatter);
                        if (batterIndex < (orderedLineup.Count - 1)) {
                            nextAtBatPlayerId = Guid.Parse(orderedLineup[batterIndex + 1].PlayerId);
                        }
                        else {
                            nextAtBatPlayerId = Guid.Parse(orderedLineup.FirstOrDefault().PlayerId);
                        }
                    }
                }

            }

            return nextAtBatPlayerId;
        }





        public ChangeResult Save(IGameInningTeamBatter gameInningTeamBatter)
        {
            bool isAdd = GetGameInningTeamBatter(gameInningTeamBatter.GameInningTeamId, gameInningTeamBatter.Sequence) == null;

            if (isAdd) return AddNew(gameInningTeamBatter);
            return Update(gameInningTeamBatter);
        }


        public ChangeResult AddNew(IGameInningTeamBatter gameInningTeamBatter)
        {
            return AddNew(new List<IGameInningTeamBatter> { gameInningTeamBatter });
        }
        public ChangeResult AddNew(List<IGameInningTeamBatter> gameInningTeamBatters)
        {
            var result = Validate(gameInningTeamBatters, isAddNew: true);
            if (result.IsSuccess)
            {
                using (var context = new Data.DartballContext())
                {
                    foreach (var item in gameInningTeamBatters)
                    {
                        context.GameInningTeamBatters.Add(new Domain.GameInningTeamBatter()
                        {
                            GameInningTeamBatterId = item.GameInningTeamBatterId == Guid.Empty ? Guid.NewGuid().ToString() : item.GameInningTeamBatterId.ToString(),
                            GameInningTeamId = item.GameInningTeamId.ToString(),
                            PlayerId = item.PlayerId.ToString(),
                            Sequence = item.Sequence,
                            EventType = item.EventType,
                            TargetEventType = item.TargetEventType,
                            RBIs = item.RBIs,
                            DeleteDate = item.DeleteDate
                        });
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }

        public ChangeResult Update(IGameInningTeamBatter gameInningTeamBatter)
        {
            return Update(new List<IGameInningTeamBatter> { gameInningTeamBatter });
        }
        public ChangeResult Update(List<IGameInningTeamBatter> gameInningTeamBatters)
        {
            var result = Validate(gameInningTeamBatters, isAddNew: false);
            if (result.IsSuccess)
            {
                using (var context = new Data.DartballContext())
                {
                    foreach (var item in gameInningTeamBatters)
                    {
                        context.GameInningTeamBatters.Update(new Domain.GameInningTeamBatter()
                        {
                            GameInningTeamBatterId = item.GameInningTeamBatterId.ToString(),
                            GameInningTeamId = item.GameInningTeamId.ToString(),
                            PlayerId = item.PlayerId.ToString(),
                            Sequence = item.Sequence,
                            EventType = item.EventType,
                            TargetEventType = item.TargetEventType,
                            RBIs = item.RBIs,
                            DeleteDate = item.DeleteDate
                        });
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }



        private ChangeResult Validate(List<IGameInningTeamBatter> gameInningTeamBatters, bool isAddNew = false)
        {
            ChangeResult result = new ChangeResult();

            List<int> validEvents = new List<int>();
            foreach (var item in EventService.GetAllGameEvents()) validEvents.Add((int)item.Event);

            foreach (var item in gameInningTeamBatters)
            {
                if (!result.IsSuccess) break;

                if (item.GameInningTeamId == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid Game Inning Team Key");
                }

                if (item.PlayerId == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid Player");
                }

                if (item.Sequence < 0)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Sequence must be > 0");
                }

                if (item.RBIs < 0)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Cannot have RBIs < 0");
                }

                if (!validEvents.Contains(item.EventType))
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add($"Invalid Event Type: {item.EventType}");
                }

                if (item.TargetEventType.HasValue)
                {
                    if (!validEvents.Contains(item.TargetEventType.Value))
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add($"Invalid Target Event Type: {item.TargetEventType.Value}");
                    }
                }

                if (isAddNew == false) {
                    if (item.GameInningTeamBatterId == Guid.Empty) {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid Team Inning Batter Id.");
                    }
                }


            }

            return result;
        }



        public ChangeResult Remove(Guid gameInningTeamId, int sequence)
        {
            ChangeResult result = new ChangeResult();
            using (var context = new Data.DartballContext())
            {
                var item = context.GameInningTeamBatters.FirstOrDefault(x => x.GameInningTeamId == gameInningTeamId.ToString() && x.Sequence == sequence);
                if (item != null)
                {
                    context.GameInningTeamBatters.Remove(item);
                    context.SaveChanges();
                }
            }
            return result;
        }

    }
}

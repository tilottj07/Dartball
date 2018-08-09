using Dartball.BusinessLayer.Team.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Dartball.BusinessLayer.Team.Dto;
using Dartball.BusinessLayer.Team.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using System.Linq;
using Dartball.BusinessLayer.Player.Interface.Models;
using Microsoft.EntityFrameworkCore;
using Dartball.BusinessLayer.Player.Dto;

namespace Dartball.BusinessLayer.Team.Implementation
{
    public class TeamPlayerLineupService : ITeamPlayerLineupService
    {
        private IMapper Mapper;

        public TeamPlayerLineupService()
        {
            var mapConfig = new MapperConfiguration(c => c.CreateMap<Domain.TeamPlayerLineup, TeamPlayerLineupDto>());
            Mapper = mapConfig.CreateMapper();
        }


        public ITeamPlayerLineup GetTeamPlayerLineupItem(Guid teamId, Guid playerId)
        {
            ITeamPlayerLineup teamPlayerLineup = null;

            using (var context = new Data.DartballContext())
            {
                var item = context.TeamPlayerLineups.FirstOrDefault(x => x.TeamId == teamId.ToString() && x.PlayerId == playerId.ToString());
                if (item != null) teamPlayerLineup = Mapper.Map<TeamPlayerLineupDto>(item);
            }

            return teamPlayerLineup;
        }

        public List<ITeamPlayerLineup> GetTeamLineup(Guid teamId)
        {
            List<ITeamPlayerLineup> teamPlayerLineups = new List<ITeamPlayerLineup>();

            using (var context = new Data.DartballContext())
            {
                var items = context.TeamPlayerLineups.Where(x => x.TeamId == teamId.ToString() && !x.DeleteDate.HasValue).OrderBy(x => x.BattingOrder).ToList();
                foreach (var item in items) teamPlayerLineups.Add(Mapper.Map<TeamPlayerLineupDto>(item));
            }

            return teamPlayerLineups;
        }


        public List<IPlayer> GetTeamSortedBattingOrderPlayers(Guid teamId) {
            List<IPlayer> players = new List<IPlayer>();

            using(var context = new Data.DartballContext()) {
                var items = context.TeamPlayerLineups
                                   .Where(x => x.TeamId == teamId.ToString() && !x.DeleteDate.HasValue)
                                   .OrderBy(x => x.BattingOrder)
                                   .Include(y => y.Player).Select(x => x.Player).ToList();

                foreach (var item in items) players.Add(Mapper.Map<PlayerDto>(item));
            }

            return players;
        }



        public ChangeResult SetLineup(List<ITeamPlayerLineup> teamPlayerLineups) {

            List<ITeamPlayerLineup> adds = new List<ITeamPlayerLineup>();
            List<ITeamPlayerLineup> updates = new List<ITeamPlayerLineup>();

            if (teamPlayerLineups.Count > 0) {
                Guid teamId = teamPlayerLineups.FirstOrDefault().TeamId;
                var existingLineup = GetTeamLineup(teamId);

                //determine adds vs updates
                foreach(var item in teamPlayerLineups) {
                    var existing = existingLineup.FirstOrDefault(x => x.PlayerId == item.PlayerId);

                    if (existing == null) adds.Add(item);
                    else updates.Add(item);
                }

                //delete any players that have been removed from the lineup
                foreach(var item in existingLineup) {
                    if (adds.FirstOrDefault(x => x.PlayerId == item.PlayerId) == null 
                        && updates.FirstOrDefault(x => x.PlayerId == item.PlayerId) == null) {

                        Remove(item.TeamId, item.PlayerId);
                    }
                }
            }

            var result = AddNew(adds);
            if (result.IsSuccess) result = Update(updates);

            return result;
        }

        public ChangeResult Save(ITeamPlayerLineup teamPlayerLineup) {
           
            bool isAdd = GetTeamPlayerLineupItem(teamPlayerLineup.TeamId, teamPlayerLineup.PlayerId) == null;

            if (isAdd) return AddNew(teamPlayerLineup);
            else return Update(teamPlayerLineup);
        }


        public ChangeResult AddNew(ITeamPlayerLineup teamPlayerLineup)
        {
            return AddNew(new List<ITeamPlayerLineup> { teamPlayerLineup });
        }
        public ChangeResult AddNew(List<ITeamPlayerLineup> teamPlayerLineups)
        {
            var result = Validate(teamPlayerLineups, isAddNew: true);
            if (result.IsSuccess)
            {
                using (var context = new Data.DartballContext())
                {
                    foreach (var item in teamPlayerLineups)
                    {
                        context.TeamPlayerLineups.Add(new Domain.TeamPlayerLineup()
                        {
                            TeamPlayerLineupId = item.TeamPlayerLineupId == Guid.Empty ? Guid.NewGuid().ToString() : item.TeamPlayerLineupId.ToString(),
                            TeamId = item.TeamId.ToString(),
                            PlayerId = item.PlayerId.ToString(),
                            BattingOrder = item.BattingOrder,
                            DeleteDate = item.DeleteDate
                        });
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }

        public ChangeResult Update(ITeamPlayerLineup teamPlayerLineup)
        {
            return Update(new List<ITeamPlayerLineup> { teamPlayerLineup });
        }
        public ChangeResult Update(List<ITeamPlayerLineup> teamPlayerLineups)
        {
            var result = Validate(teamPlayerLineups, isAddNew: false);
            if (result.IsSuccess)
            {
                using (var context = new Data.DartballContext())
                {
                    foreach (var item in teamPlayerLineups)
                    {
                        context.TeamPlayerLineups.Update(new Domain.TeamPlayerLineup()
                        {
                            TeamId = item.TeamId.ToString(),
                            PlayerId = item.PlayerId.ToString(),
                            BattingOrder = item.BattingOrder,
                            DeleteDate = item.DeleteDate
                        });
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }


        private ChangeResult Validate(List<ITeamPlayerLineup> teamPlayerLineups, bool isAddNew = true)
        {
            ChangeResult result = new ChangeResult();

            foreach(var item in teamPlayerLineups)
            {
                if (!result.IsSuccess) break;

                if (item.TeamId == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid Team.");
                }
                else if (item.PlayerId == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid Player.");
                }

                if (isAddNew == false)
                {

                }
            }

            return result;
        }





        public ChangeResult Remove(Guid teamId, Guid playerId)
        {
            ChangeResult result = new ChangeResult();

            using (var context = new Data.DartballContext())
            {
                var item = context.TeamPlayerLineups.FirstOrDefault(x => x.TeamId == teamId.ToString() && x.PlayerId == playerId.ToString());
                if (item != null)
                {
                    context.TeamPlayerLineups.Remove(item);
                    context.SaveChanges();
                }
            }

            return result;
        }


    }
}

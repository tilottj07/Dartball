using Dartball.BusinessLayer.Team.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Dartball.BusinessLayer.Team.Dto;
using Dartball.BusinessLayer.Team.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using System.Linq;

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
                            TeamPlayerLineupId = item.TeamPlayerLineupId.ToString(),
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
                    if (item.TeamPlayerLineupId == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid Team Player Lineup Alternate Key.");
                    }
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

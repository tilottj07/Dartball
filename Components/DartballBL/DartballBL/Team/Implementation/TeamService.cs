using Dartball.BusinessLayer.Team.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Dartball.BusinessLayer.Team.Dto;
using Dartball.BusinessLayer.Team.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using Dartball.BusinessLayer.Shared;
using System.Linq;

namespace Dartball.BusinessLayer.Team.Implementation
{
    public class TeamService : ITeamService
    {
        private IMapper Mapper;

        public TeamService()
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Domain.Team, TeamDto>());
            Mapper = mapConfig.CreateMapper();
        }


        public ITeam GetTeam(Guid teamId)
        {
            ITeam team = null;

            using (var context = new Data.DartballContext())
            {
                var item = context.Teams.FirstOrDefault(x => x.TeamId == teamId.ToString());
                if (item != null) team = Mapper.Map<TeamDto>(item);
            }

            return team;
        }

        public List<ITeam> GetTeams(Guid leagueId)
        {
            List<ITeam> teams = new List<ITeam>();

            using (var context = new Data.DartballContext())
            {
                var items = context.Teams.Where(x => x.LeagueId == leagueId.ToString() && !x.DeleteDate.HasValue).ToList();
                foreach (var item in items) teams.Add(Mapper.Map<TeamDto>(item));
            }

            return teams;
        }

        public List<ITeam> GetTeams()
        {
            List<ITeam> teams = new List<ITeam>();

            using (var context = new Data.DartballContext())
            {
                var items = context.Teams.Where(x => !x.DeleteDate.HasValue).ToList();
                foreach (var item in items) teams.Add(Mapper.Map<TeamDto>(item));
            }

            return teams;
        }



        public ChangeResult AddNew(ITeam team)
        {
            return AddNew(new List<ITeam> { team });
        }
        public ChangeResult AddNew(List<ITeam> teams)
        {
            var result = Validate(teams);
            if (result.IsSuccess)
            {
                using (var context = new Data.DartballContext())
                {
                    foreach (var team in teams)
                    {
                        context.Teams.Add(new Domain.Team()
                        {
                            TeamId = team.TeamId == Guid.Empty ? Guid.NewGuid().ToString() : team.TeamId.ToString(),
                            LeagueId = team.LeagueId.ToString(),
                            Name = Helper.CleanString(team.Name),
                            Password = Helper.CleanString(team.Password), //TODO: add encryption
                            Handicap = team.Handicap,
                            ShouldSync = team.ShouldSync ? 1 : 0,
                            DeleteDate = team.DeleteDate
                        });
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }


        public ChangeResult Update(ITeam team)
        {
            return Update(new List<ITeam> { team });
        }
        public ChangeResult Update(List<ITeam> teams)
        {
            var result = Validate(teams);
            if (result.IsSuccess)
            {
                using (var context = new Data.DartballContext())
                {
                    foreach (var team in teams)
                    {
                        context.Teams.Update(new Domain.Team()
                        {
                            TeamId = team.TeamId.ToString(),
                            LeagueId = team.LeagueId.ToString(),
                            Name = Helper.CleanString(team.Name),
                            Password = Helper.CleanString(team.Password), //TODO: add encryption
                            Handicap = team.Handicap,
                            ShouldSync = team.ShouldSync ? 1 : 0,
                            DeleteDate = team.DeleteDate
                        });
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }


        private ChangeResult Validate(List<ITeam> teams)
        {
            ChangeResult result = new ChangeResult();

            foreach(var team in teams)
            {
                if (!result.IsSuccess) break;
                if (team.LeagueId == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid League.");
                }
                else if (string.IsNullOrWhiteSpace(team.Name))
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Team Name is Required.");
                }
                else if (team.Name.Trim().Length > 100)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Team Name cannot be longer than 100 characters.");
                }
            }

            return result;
        }



        public ChangeResult Remove(Guid teamId)
        {
            ChangeResult result = new ChangeResult();

            using (var context = new Data.DartballContext())
            {
                var item = context.Teams.FirstOrDefault(x => x.TeamId == teamId.ToString());
                if (item != null)
                {
                    context.Teams.Remove(item);
                    context.SaveChanges();
                }
            }

            return result;
        }


    }
}

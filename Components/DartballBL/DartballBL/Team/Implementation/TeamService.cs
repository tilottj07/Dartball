using Dartball.BusinessLayer.Team.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Dartball.BusinessLayer.Team.Dto;
using Dartball.BusinessLayer.Team.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;

namespace Dartball.BusinessLayer.Team.Implementation
{
    public class TeamService : ITeamService
    {
        private IMapper Mapper;
        private DataLayer.Device.Repository.TeamRepository TeamRepository;

        public TeamService()
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<DataLayer.Device.Dto.TeamDto, TeamDto>());
            Mapper = mapConfig.CreateMapper();

            TeamRepository = new DataLayer.Device.Repository.TeamRepository();
        }


        public ITeam GetTeam(Guid teamAlternateKey)
        {
            ITeam team = null;

            var dl = TeamRepository.LoadByKey(teamAlternateKey);
            if (dl != null) team = Mapper.Map<TeamDto>(dl);

            return team;
        }

        public List<ITeam> GetTeams(Guid leagueAlternateKey)
        {
            List<ITeam> teams = new List<ITeam>();
            foreach(var item in TeamRepository.LoadByLeagueKey(leagueAlternateKey))
            {
                if (!item.DeleteDate.HasValue) teams.Add(Mapper.Map<TeamDto>(item));
            }

            return teams;
        }

        public List<ITeam> GetTeams()
        {
            List<ITeam> teams = new List<ITeam>();
            foreach (var item in TeamRepository.LoadAll())
            {
                if (!item.DeleteDate.HasValue) teams.Add(Mapper.Map<TeamDto>(item));
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
                foreach(var team in teams)
                {
                    DataLayer.Device.Dto.TeamDto dto = new DataLayer.Device.Dto.TeamDto()
                    {
                        TeamAlternateKey = team.TeamAlternateKey == Guid.Empty ? Guid.NewGuid().ToString() : team.TeamAlternateKey.ToString(),
                        LeagueAlternateKey = team.LeagueAlternateKey.ToString(),
                        Name = team.Name,
                        Password = team.Password, //TODO: add encryption
                        Handicap = team.Handicap,
                        ShouldSync = team.ShouldSync,
                        DeleteDate = team.DeleteDate
                    };
                    TeamRepository.AddNew(dto);
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
                foreach (var team in teams)
                {
                    DataLayer.Device.Dto.TeamDto dto = new DataLayer.Device.Dto.TeamDto()
                    {
                        TeamAlternateKey = team.TeamAlternateKey.ToString(),
                        LeagueAlternateKey = team.LeagueAlternateKey.ToString(),
                        Name = team.Name,
                        Password = team.Password, //TODO: add encryption
                        Handicap = team.Handicap,
                        ShouldSync = team.ShouldSync,
                        DeleteDate = team.DeleteDate
                    };
                    TeamRepository.Update(dto);
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
                if (team.LeagueAlternateKey == Guid.Empty)
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


    }
}

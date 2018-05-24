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
    public class TeamPlayerLineupService : ITeamPlayerLineupService
    {
        private IMapper Mapper;
        private DataLayer.Device.Repository.TeamPlayerLineupRepository TeamPlayerLineupRepository;

        public TeamPlayerLineupService()
        {
            var mapConfig = new MapperConfiguration(c => c.CreateMap<DataLayer.Device.Dto.TeamPlayerLineupDto, TeamPlayerLineupDto>());
            Mapper = mapConfig.CreateMapper();

            TeamPlayerLineupRepository = new DataLayer.Device.Repository.TeamPlayerLineupRepository();
        }


        public ITeamPlayerLineup GetTeamPlayerLineupItem(Guid teamAlternateKey, Guid playerAlternateKey)
        {
            ITeamPlayerLineup teamPlayerLineup = null;

            var dl = TeamPlayerLineupRepository.LoadByCompositeKey(playerAlternateKey, teamAlternateKey);
            if (dl != null) teamPlayerLineup = Mapper.Map<TeamPlayerLineupDto>(dl);

            return teamPlayerLineup;
        }

        public List<ITeamPlayerLineup> GetTeamLineup(Guid teamAlternateKey)
        {
            List<ITeamPlayerLineup> teamPlayerLineups = new List<ITeamPlayerLineup>();

            foreach(var item in TeamPlayerLineupRepository.LoadByTeamAlternateKey(teamAlternateKey))
            {
                if (!item.DeleteDate.HasValue) teamPlayerLineups.Add(Mapper.Map<TeamPlayerLineupDto>(item));
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
                foreach(var item in teamPlayerLineups)
                {
                    DataLayer.Device.Dto.TeamPlayerLineupDto dto = new DataLayer.Device.Dto.TeamPlayerLineupDto()
                    {
                        TeamPlayerLineupAlternateKey = item.TeamPlayerLineupAlternateKey == Guid.Empty ? Guid.NewGuid().ToString() : item.TeamPlayerLineupAlternateKey.ToString(),
                        TeamAlternateKey = item.TeamAlternateKey.ToString(),
                        PlayerAlternateKey = item.PlayerAlternateKey.ToString(),
                        BattingOrder = item.BattingOrder,
                        DeleteDate = item.DeleteDate
                    };
                    TeamPlayerLineupRepository.AddNew(dto);
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
                foreach (var item in teamPlayerLineups)
                {
                    DataLayer.Device.Dto.TeamPlayerLineupDto dto = new DataLayer.Device.Dto.TeamPlayerLineupDto()
                    {
                        TeamPlayerLineupAlternateKey = item.TeamPlayerLineupAlternateKey.ToString(),
                        TeamAlternateKey = item.TeamAlternateKey.ToString(),
                        PlayerAlternateKey = item.PlayerAlternateKey.ToString(),
                        BattingOrder = item.BattingOrder,
                        DeleteDate = item.DeleteDate
                    };
                    TeamPlayerLineupRepository.Update(dto);
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

                if (item.TeamAlternateKey == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid Team.");
                }
                else if (item.PlayerAlternateKey == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid Player.");
                }

                if (isAddNew == false)
                {
                    if (item.TeamPlayerLineupAlternateKey == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid Team Player Lineup Alternate Key.");
                    }
                }
            }

            return result;
        }





        public ChangeResult Remove(Guid teamAlternateKey, Guid playerAlternateKey)
        {
            ChangeResult result = new ChangeResult();
            TeamPlayerLineupRepository.Delete(playerAlternateKey, teamAlternateKey);
            return result;
        }


    }
}

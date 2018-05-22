using Dartball.BusinessLayer.Player.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Dartball.BusinessLayer.Player.Dto;
using Dartball.BusinessLayer.Player.Interface.Models;

namespace Dartball.BusinessLayer.Player.Implementation
{
    public class PlayerTeamService : IPlayerTeamService
    {
        private IMapper Mapper;
        private DataLayer.Device.Repository.PlayerTeamRepository PlayerTeamRepository;

        public PlayerTeamService()
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<DataLayer.Device.Dto.PlayerTeamDto, PlayerTeamDto>());
            Mapper = mapConfig.CreateMapper();

            PlayerTeamRepository = new DataLayer.Device.Repository.PlayerTeamRepository();
        }

        public List<IPlayerTeam> GetTeamPlayers(Guid teamAlternateKey)
        {
            List<IPlayerTeam> playerTeams = new List<IPlayerTeam>();
            foreach(var item in PlayerTeamRepository.LoadByTeamAlternateKey(teamAlternateKey))
            {
                if (!item.DeleteDate.HasValue) playerTeams.Add(Mapper.Map<PlayerTeamDto>(item));
            }

            return playerTeams;
        }

    }
}

using Dartball.BusinessLayer.Player.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Dartball.BusinessLayer.Player.Dto;
using Dartball.BusinessLayer.Player.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;

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

        public IPlayerTeam GetPlayerTeam(Guid teamAlternateKey, Guid playerAlternateKey)
        {
            IPlayerTeam playerTeam = null;

            var dl = PlayerTeamRepository.LoadByCompositeKey(playerAlternateKey, teamAlternateKey);
            if (dl != null) playerTeam = Mapper.Map<PlayerTeamDto>(dl);

            return playerTeam;
        }





        public ChangeResult AddNew(IPlayerTeam playerTeam) 
        {
            return AddNew(new List<IPlayerTeam> { playerTeam });
        }
        public ChangeResult AddNew(List<IPlayerTeam> playerTeams)
        {
            var result = Validate(playerTeams);
            if (result.IsSuccess)
            {
                foreach(var item in playerTeams)
                {
                    DataLayer.Device.Dto.PlayerTeamDto dto = new DataLayer.Device.Dto.PlayerTeamDto()
                    {
                        PlayerTeamAlternateKey = item.PlayerTeamAlternateKey == Guid.Empty ? Guid.NewGuid().ToString() : item.PlayerTeamAlternateKey.ToString(),
                        PlayerAlternateKey = item.PlayerAlternateKey.ToString(),
                        TeamAlternateKey = item.TeamAlternateKey.ToString(),
                        DeleteDate = item.DeleteDate
                    };
                    PlayerTeamRepository.AddNew(dto);
                }
            }
            return result;
        }


        public ChangeResult Update(IPlayerTeam playerTeam)
        {
            return Update(new List<IPlayerTeam> { playerTeam });
        }
        public ChangeResult Update(List<IPlayerTeam> playerTeams)
        {
            var result = Validate(playerTeams);
            if (result.IsSuccess)
            {
                foreach (var item in playerTeams)
                {
                    DataLayer.Device.Dto.PlayerTeamDto dto = new DataLayer.Device.Dto.PlayerTeamDto()
                    {
                        PlayerTeamAlternateKey = item.PlayerTeamAlternateKey.ToString(),
                        PlayerAlternateKey = item.PlayerAlternateKey.ToString(),
                        TeamAlternateKey = item.TeamAlternateKey.ToString(),
                        DeleteDate = item.DeleteDate
                    };
                    PlayerTeamRepository.Update(dto);
                }
            }
            return result;
        }




        private ChangeResult Validate(List<IPlayerTeam> playerTeams)
        {
            ChangeResult result = new ChangeResult();

            foreach(var item in playerTeams)
            {
                if (!result.IsSuccess) break;
                if (item.PlayerAlternateKey == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Incorrect PlayerAlternateKey.");
                }
                if (item.TeamAlternateKey == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Incorrect TeamAlternateKey.");
                }
            }

            return result;
        }




        public ChangeResult Remove(Guid playerAlternateKey, Guid teamAlternateKey)
        {
            ChangeResult result = new ChangeResult();
            PlayerTeamRepository.Delete(playerAlternateKey, teamAlternateKey);

            return result;
        }


    }
}

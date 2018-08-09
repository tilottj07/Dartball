using Dartball.BusinessLayer.Player.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Dartball.BusinessLayer.Player.Dto;
using Dartball.BusinessLayer.Player.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Dartball.BusinessLayer.Player.Implementation
{
    public class PlayerTeamService : IPlayerTeamService
    {
        private IMapper Mapper;

        public PlayerTeamService()
        {
            var mapConfig = new MapperConfiguration(cfg => 
            { 
                cfg.CreateMap<Domain.PlayerTeam, PlayerTeamDto>();
                cfg.CreateMap<Domain.Player, PlayerDto>();
            });
            Mapper = mapConfig.CreateMapper();
        }

        public List<IPlayerTeam> GetTeamPlayers(Guid teamId)
        {
            List<IPlayerTeam> playerTeams = new List<IPlayerTeam>();

            using (var context = new Data.DartballContext())
            {
                var items = context.PlayerTeams.Where(x => x.TeamId == teamId.ToString() && !x.DeleteDate.HasValue).ToList();
                foreach (var item in items) playerTeams.Add(Mapper.Map<PlayerTeamDto>(item));
            }

            return playerTeams;
        }

        public IPlayerTeam GetPlayerTeam(Guid teamId, Guid playerId)
        {
            IPlayerTeam playerTeam = null;

            using (var context = new Data.DartballContext())
            {
                var item = context.PlayerTeams.FirstOrDefault(x => x.TeamId == teamId.ToString() && x.PlayerId == playerId.ToString());
                if (item != null) playerTeam = Mapper.Map<PlayerTeamDto>(item);
            }

            return playerTeam;
        }

        public List<IPlayerTeam> GetPlayerTeams(Guid playerId) {
            List<IPlayerTeam> playerTeams = new List<IPlayerTeam>();

            using(var context = new Data.DartballContext()) {
                var items = context.PlayerTeams.Where(x => x.PlayerId == playerId.ToString()).ToList();
                foreach (var item in items) playerTeams.Add(Mapper.Map<PlayerTeamDto>(item));
            }

            return playerTeams;
        }


        public List<IPlayer> GetTeamPlayerInformations(Guid teamId) {
            List<IPlayer> players = new List<IPlayer>();

            using (var context = new Data.DartballContext()) {
                var items = context.PlayerTeams
                                   .Where(x => x.TeamId == teamId.ToString() && !x.DeleteDate.HasValue)
                                   .Include(x => x.Player).Select(x => x.Player).ToList();

                foreach (var item in items)
                {
                    players.Add(Mapper.Map<PlayerDto>(item));
                }
            }

            return players;
        }




        public ChangeResult Save(IPlayerTeam playerTeam) {
            bool isAdd = false;

            if (GetPlayerTeam(playerTeam.TeamId, playerTeam.PlayerId) == null) isAdd = true;

            if (isAdd) return AddNew(playerTeam);
            else return Update(playerTeam);
        }


        public ChangeResult AddNew(IPlayerTeam playerTeam) 
        {
            return AddNew(new List<IPlayerTeam> { playerTeam });
        }
        public ChangeResult AddNew(List<IPlayerTeam> playerTeams)
        {
            var result = Validate(playerTeams, isAddNew: true);
            if (result.IsSuccess)
            {
                using (var context = new Data.DartballContext())
                {
                    foreach (var item in playerTeams)
                    {
                        context.PlayerTeams.Add(new Domain.PlayerTeam()
                        {
                            PlayerTeamId = item.PlayerTeamId == Guid.Empty ? Guid.NewGuid().ToString() : item.PlayerTeamId.ToString(),
                            PlayerId = item.PlayerId.ToString(),
                            TeamId = item.TeamId.ToString(),
                            DeleteDate = item.DeleteDate
                        });
                    }
                    context.SaveChanges();
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
            var result = Validate(playerTeams, isAddNew: false);
            if (result.IsSuccess)
            {
                using (var context = new Data.DartballContext())
                {
                    foreach (var item in playerTeams)
                    {
                        context.PlayerTeams.Update(new Domain.PlayerTeam()
                        {
                            PlayerTeamId = item.PlayerTeamId.ToString(),
                            PlayerId = item.PlayerId.ToString(),
                            TeamId = item.TeamId.ToString(),
                            DeleteDate = item.DeleteDate
                        });
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }




        private ChangeResult Validate(List<IPlayerTeam> playerTeams, bool isAddNew = true)
        {
            ChangeResult result = new ChangeResult();

            foreach(var item in playerTeams)
            {
                if (!result.IsSuccess) break;
                if (item.PlayerId == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Incorrect PlayerId.");
                }
                if (item.TeamId == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Incorrect TeamId.");
                }

                if (isAddNew == false)
                {
                    if (item.PlayerTeamId == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid Player Team Alternate Key.");
                    }
                }
            }

            return result;
        }




        public ChangeResult Remove(Guid playerId, Guid teamId)
        {
            ChangeResult result = new ChangeResult();

            using (var context = new Data.DartballContext())
            {
                var item = context.PlayerTeams.FirstOrDefault(x => x.PlayerId == playerId.ToString() && x.TeamId == teamId.ToString());
                if (item != null)
                {
                    context.PlayerTeams.Remove(item);
                    context.SaveChanges();
                }
            }

            return result;
        }


    }
}

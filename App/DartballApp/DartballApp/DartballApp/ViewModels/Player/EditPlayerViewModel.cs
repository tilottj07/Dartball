using System;
using System.Collections.Generic;
using System.Linq;
using Dartball.BusinessLayer.League.Implementation;
using Dartball.BusinessLayer.League.Interface;
using Dartball.BusinessLayer.Player.Dto;
using Dartball.BusinessLayer.Player.Implementation;
using Dartball.BusinessLayer.Player.Interface;
using Dartball.BusinessLayer.Shared.Models;
using Dartball.BusinessLayer.Team.Implementation;
using Dartball.BusinessLayer.Team.Interface;

namespace DartballApp.ViewModels.Player
{
    public class EditPlayerViewModel
    {
        IPlayerService Service;
        IPlayerTeamService PlayerTeam;

        public EditPlayerViewModel(Guid? playerId, Guid? leagueId)
        {
            Service = new PlayerService();
            PlayerTeam = new PlayerTeamService();
            PlayerId = playerId;
            LeagueId = leagueId;
        }


        public Guid? PlayerId { get; set; }
        public Guid? LeagueId { get; set; }
        public Guid? TeamId { get; set; }
        public int TeamPickerDefaultSelectedIndex { get; set; }

        public Models.Player Player { get; set; }
        public List<Models.Team> Teams { get; set; }


        public void FillPlayer()
        {
            if (PlayerId.HasValue) {
                Player = new Models.Player(Service.GetPlayer(PlayerId.Value));
            }
            else {
                Player = new Models.Player();
            }
        }

        public void FillTeams() {
            Teams = new List<Models.Team>();
            TeamPickerDefaultSelectedIndex = -1;

            //determine players team id available 
            if (PlayerId.HasValue)
            {
                var team = PlayerTeam.GetPlayerTeams(PlayerId.Value).FirstOrDefault();
                if (team != null) TeamId = team.TeamId;
            }

            if (!LeagueId.HasValue) {
                ILeagueService leagueService = new LeagueService();
                var league = leagueService.GetGenericLeague();
                if (league != null) LeagueId = league.LeagueId;
            }
            if (LeagueId.HasValue) {
                ITeamService teamService = new TeamService();

                int index = 0;
                foreach(var team in teamService.GetTeams(LeagueId.Value)) {
                    Teams.Add(new Models.Team(team));
                    if (team.TeamId == TeamId)
                    {
                        TeamPickerDefaultSelectedIndex = index;
                    }
                    index++;
                }
            }
        }



        public ChangeResult SavePlayer()
        {
            PlayerDto dto = new PlayerDto()
            {
                PlayerId = Player.PlayerId,
                EmailAddress = Player.EmailAddress,
                Name = Player.Name,
                UserName = Player.UserName,
                Password = Player.Password
            };
            return Service.Save(dto);
        }

        public ChangeResult SavePlayerTeam(Guid teamId) {
            PlayerTeamDto dto = new PlayerTeamDto()
            {
                PlayerId = Player.PlayerId,
                TeamId = teamId
            };
                           
            return PlayerTeam.Save(dto);
        }
    }
}

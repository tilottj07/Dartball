using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Dartball.BusinessLayer.Player.Dto;
using Dartball.BusinessLayer.Player.Implementation;
using Dartball.BusinessLayer.Player.Interface;
using Dartball.BusinessLayer.Shared.Models;
using Dartball.BusinessLayer.Team.Implementation;
using Dartball.BusinessLayer.Team.Interface;

namespace DartballApp.ViewModels
{
    public class EditTeamPlayersViewModel
    {
        IPlayerTeamService PlayerTeam;
        IPlayerService Player;
        ITeamService Team;

        public EditTeamPlayersViewModel(Guid teamId)
        {
            TeamId = teamId;

            PlayerTeam = new PlayerTeamService();
            Player = new PlayerService();
            Team = new TeamService();
        }

        public Guid TeamId { get; set; }
        public Guid LeagueId { get; set; }
        public string TeamName { get; set; }

        public ObservableCollection<Models.Player> TeamPlayers { get; set; }
        public ObservableCollection<Models.Player> AvailablePlayers { get; set; }


        public void FillPlayers() {
            TeamPlayers = new ObservableCollection<Models.Player>();
            AvailablePlayers = new ObservableCollection<Models.Player>();

            foreach(var player in PlayerTeam.GetTeamPlayerInformations(TeamId).OrderBy(y => y.Name)) {
                TeamPlayers.Add(new Models.Player(player));
            }

            List<Guid> playerIdsOnTeam = TeamPlayers.Select(y => y.PlayerId).ToList();
            foreach(var player in Player.GetPlayers().OrderBy(y => y.Name)) {
                if (!playerIdsOnTeam.Contains(player.PlayerId)) {
                    AvailablePlayers.Add(new Models.Player(player));
                }
            }

        }

       

        public void FillTeamInfo() {
            TeamName = string.Empty;

            var team = Team.GetTeam(TeamId);
            if (team != null)
            {
                TeamName = team.Name;
                LeagueId = team.LeagueId;
            }
        }


        public ChangeResult AddTeamPlayer(Guid playerId) {
            return PlayerTeam.Save(new PlayerTeamDto()
            {
                TeamId = TeamId,
                PlayerId = playerId
            });
        }

        public ChangeResult RemoveTeamPlayer(Guid playerId) {
            return PlayerTeam.Remove(playerId, TeamId);
        }
    }
}

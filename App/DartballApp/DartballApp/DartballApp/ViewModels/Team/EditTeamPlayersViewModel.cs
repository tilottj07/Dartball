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

namespace DartballApp.ViewModels.Team
{
    public class EditTeamPlayersViewModel
    {
        IPlayerTeamService PlayerTeam;
        ITeamService Team;
        ITeamPlayerLineupService TeamPlayerLineup;

        public EditTeamPlayersViewModel(Guid teamId)
        {
            TeamId = teamId;

            PlayerTeam = new PlayerTeamService();
            Team = new TeamService();
            TeamPlayerLineup = new TeamPlayerLineupService();
        }

        public Guid TeamId { get; set; }
        public Guid LeagueId { get; set; }
        public string TeamName { get; set; }

        public ObservableCollection<Models.Player> TeamPlayers { get; set; }
     


        public void FillPlayers() {
            TeamPlayers = new ObservableCollection<Models.Player>();

            foreach(var player in PlayerTeam.GetTeamPlayerInformations(TeamId).OrderBy(y => y.LastName).ThenBy(y => y.Name)) {
                TeamPlayers.Add(new Models.Player(player));
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


        public ChangeResult RemoveTeamPlayer(Guid playerId) {

            //remove player from lineup, then from the actual team
            var result = TeamPlayerLineup.Remove(TeamId, playerId);
            if (result.IsSuccess) {
                result = PlayerTeam.Remove(playerId, TeamId);
            }

            return result;
        }
    }
}

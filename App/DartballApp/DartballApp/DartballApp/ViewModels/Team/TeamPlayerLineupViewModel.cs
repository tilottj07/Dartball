using System;
using System.Collections.Generic;
using System.Linq;
using Dartball.BusinessLayer.Player.Implementation;
using Dartball.BusinessLayer.Player.Interface;
using Dartball.BusinessLayer.Shared.Models;
using Dartball.BusinessLayer.Team.Dto;
using Dartball.BusinessLayer.Team.Implementation;
using Dartball.BusinessLayer.Team.Interface;
using Dartball.BusinessLayer.Team.Interface.Models;

namespace DartballApp.ViewModels.Team
{
    public class TeamPlayerLineupViewModel
    {
        ITeamPlayerLineupService TeamPlayerLineup;
        IPlayerTeamService PlayerTeam;
        ITeamService Team;

        public TeamPlayerLineupViewModel(Guid teamId)
        {
            TeamPlayerLineup = new TeamPlayerLineupService();
            Team = new TeamService();
            PlayerTeam = new PlayerTeamService();
            TeamId = teamId;
            TeamName = string.Empty;
            HasChanges = true;
        }

        public Guid TeamId { get; set; }
        public string TeamName { get; set; }

        public List<Models.Player> Batters { get; set; }


        public bool HasChanges { get; set; }



        public void FillBatters() {
            Batters = new List<Models.Player>();
            foreach(var item in TeamPlayerLineup.GetTeamSortedBattingOrderPlayers(TeamId)) {
                Batters.Add(new Models.Player(item));
            }

            if (Batters.Count == 0) {
                //add all players on the team
                foreach(var item in PlayerTeam.GetTeamPlayerInformations(TeamId)) {
                    Batters.Add(new Models.Player(item));
                }
                SaveLineup();
            }

        }
        public void FillTeamInfo() {
            var t = Team.GetTeam(TeamId);
            if (t != null) TeamName = t.Name;
        }

       
        public ChangeResult SaveLineup() {
            List<ITeamPlayerLineup> teamPlayerLineups = new List<ITeamPlayerLineup>();

            int battingOrder = 0;
            foreach(var batter in Batters) {
                teamPlayerLineups.Add(new TeamPlayerLineupDto()
                {
                    TeamId = TeamId,
                    PlayerId = batter.PlayerId,
                    BattingOrder = battingOrder
                });

                battingOrder++;
            }

            return TeamPlayerLineup.SetLineup(teamPlayerLineups);
        }

        public void RemovePlayerFromLineup(Guid playerId) {
            var player = Batters.FirstOrDefault(y => y.PlayerId == playerId);
            if (player != null) {
                Batters.Remove(player);
            }
        }

        public void MovePlayerUpInLineup(Guid playerId) {
            var player = Batters.FirstOrDefault(y => y.PlayerId == playerId);
            if (player != null) {
                int playerIndex = Batters.IndexOf(player);
                if (playerIndex > 0) {
                    Batters.Remove(player);
                    Batters.Insert((playerIndex - 1), player);
                }
            }
        }

        public void MovePlayerDownInLineup(Guid playerId)
        {
            var player = Batters.FirstOrDefault(y => y.PlayerId == playerId);
            if (player != null)
            {
                int playerIndex = Batters.IndexOf(player);
                if (playerIndex < (Batters.Count - 1))
                {
                    Batters.Remove(player);
                    Batters.Insert((playerIndex + 1), player);
                }
            }
        }

    }
}

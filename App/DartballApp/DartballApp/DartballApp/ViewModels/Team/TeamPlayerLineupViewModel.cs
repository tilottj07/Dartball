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

        public TeamPlayerLineupViewModel(Guid teamId)
        {
            TeamPlayerLineup = new TeamPlayerLineupService();
            TeamId = teamId;
        }

        public Guid TeamId { get; set; }
        public List<Models.Player> Batters { get; set; }



        public void FillBatters() {
            Batters = new List<Models.Player>();
            foreach(var item in TeamPlayerLineup.GetTeamSortedBattingOrderPlayers(TeamId)) {
                Batters.Add(new Models.Player(item));
            }
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

       

    }
}

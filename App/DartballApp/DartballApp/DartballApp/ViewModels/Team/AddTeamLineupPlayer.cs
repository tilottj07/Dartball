using System;
using System.Collections.Generic;
using System.Linq;
using Dartball.BusinessLayer.Player.Implementation;
using Dartball.BusinessLayer.Player.Interface;
using Dartball.BusinessLayer.Shared.Models;
using Dartball.BusinessLayer.Team.Dto;
using Dartball.BusinessLayer.Team.Implementation;
using Dartball.BusinessLayer.Team.Interface;

namespace DartballApp.ViewModels.Team
{
    public class AddTeamLineupPlayer
    {
        IPlayerTeamService PlayerTeam;
        ITeamPlayerLineupService TeamPlayerLineup;

        public AddTeamLineupPlayer(Guid teamId)
        {
            TeamId = teamId;
            PlayerTeam = new PlayerTeamService();
            TeamPlayerLineup = new TeamPlayerLineupService();
        }


        public Guid TeamId { get; set; }
        public List<Models.Player> Players { get; set; }



        public void FillPlayersToAddToLineup() {
            Players = new List<Models.Player>();

            List<Guid> playerIdsInLineup = TeamPlayerLineup.GetTeamLineup(TeamId)
                                                           .Where(z => !z.DeleteDate.HasValue)
                                                           .Select(y => y.PlayerId).ToList();

            foreach(var player in PlayerTeam.GetTeamPlayerInformations(TeamId)) {
                if (!playerIdsInLineup.Contains(player.PlayerId)) {
                    Players.Add(new Models.Player(player));
                }
            }
        }

        public ChangeResult AddPlayerToLineup(Guid playerId) {
            TeamPlayerLineupDto dto = new TeamPlayerLineupDto()
            {
                TeamId = TeamId,
                PlayerId = playerId,
                BattingOrder = 999
            };

            return TeamPlayerLineup.Save(dto);
        }
    }
}

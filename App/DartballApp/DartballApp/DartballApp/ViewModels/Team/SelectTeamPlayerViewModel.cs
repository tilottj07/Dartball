using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Dartball.BusinessLayer.Player.Dto;
using Dartball.BusinessLayer.Player.Implementation;
using Dartball.BusinessLayer.Player.Interface;
using Dartball.BusinessLayer.Shared.Models;

namespace DartballApp.ViewModels.Team
{
    public class SelectTeamPlayerViewModel
    {
        IPlayerTeamService PlayerTeam;
        IPlayerService Player;

        public SelectTeamPlayerViewModel(Guid teamId)
        {
            PlayerTeam = new PlayerTeamService();
            Player = new PlayerService();
            TeamId = teamId;
        }

        public Guid TeamId { get; set; }
        public List<Models.Player> PlayersFiltered { get; set; }
        public List<Models.Player> AllPlayers { get; set; }
                          

        public void FillAvailablePlayers() {
            PlayersFiltered = new List<Models.Player>();
            AllPlayers = new List<Models.Player>();

            List<Guid> teamPlayerIds = PlayerTeam.GetTeamPlayers(TeamId).Select(y => y.PlayerId).ToList();

            foreach(var player in Player.GetPlayers().OrderBy(y => y.Name)) {
                if (!teamPlayerIds.Contains(player.PlayerId)) {
                    AllPlayers.Add(new Models.Player(player));
                }
            }

            PlayersFiltered.AddRange(AllPlayers);
        }


        public ChangeResult AddPlayerToTeam(Models.Player player) {
            return PlayerTeam.Save(new PlayerTeamDto()
            {
                TeamId = TeamId,
                PlayerId = player.PlayerId
            });
        }

    }
}

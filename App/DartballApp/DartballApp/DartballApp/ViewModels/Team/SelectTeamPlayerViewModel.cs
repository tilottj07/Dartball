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
        public ObservableCollection<Models.Player> Players { get; set; }


        public void FillAvailablePlayers() {
            Players = new ObservableCollection<Models.Player>();

            List<Guid> teamPlayerIds = PlayerTeam.GetTeamPlayers(TeamId).Select(y => y.PlayerId).ToList();

            foreach(var player in Player.GetPlayers().OrderBy(y => y.Name)) {
                if (!teamPlayerIds.Contains(player.PlayerId)) {
                    Players.Add(new Models.Player(player));
                }
            }
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

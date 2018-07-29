using System;
using Dartball.BusinessLayer.Game.Interface.Models;

namespace DartballApp.Models
{
    public class GameTeam
    {
        public GameTeam(Guid gameId, Guid teamId)
        {
            GameId = gameId;
            TeamId = teamId;
        }

        public GameTeam(IGameTeam gameTeam) {
            if (gameTeam != null) {
                GameTeamId = gameTeam.GameTeamId;
                GameId = gameTeam.GameId;
                TeamId = gameTeam.TeamId;
            }
        }

        public Guid GameTeamId { get; set; }
        public Guid GameId { get; set; }
        public Guid TeamId { get; set; }
    }
}

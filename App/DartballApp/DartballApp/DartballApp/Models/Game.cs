using System;
using Dartball.BusinessLayer.Game.Interface.Models;

namespace DartballApp.Models
{
    public class Game
    {
        public Game(Guid leagueId)
        {
            LeagueId = leagueId;
            GameDate = DateTime.UtcNow;
        }

        public Game(IGame game) {
            if (game != null) {
                GameId = game.GameId;
                LeagueId = game.LeagueId;
                GameDate = game.GameDate;
            }
        }


        public Guid GameId { get; set; }
        public Guid LeagueId { get; set; }
        public DateTime GameDate { get; set; }
    }
}

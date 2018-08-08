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
            HasData = true;
        }

        public Game(IGame game) {
            if (game != null) {
                HasData = true;
                GameId = game.GameId;
                LeagueId = game.LeagueId;
                GameDate = game.GameDate;
            }
        }

        public bool HasData { get; set; }

        public Guid GameId { get; set; }
        public Guid LeagueId { get; set; }
        public DateTime GameDate { get; set; }
    }
}

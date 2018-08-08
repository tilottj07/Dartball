using System;
using Dartball.BusinessLayer.Game.Interface.Models;

namespace DartballApp.Models
{
    public class GameInning
    {
        public GameInning(Guid gameId, int inningNumber)
        {
            GameId = gameId;
            InningNumber = inningNumber;
            HasData = true;
        }

        public GameInning(IGameInning gameInning) {
            if (gameInning != null) {
                HasData = true;
                GameInningId = gameInning.GameInningId;
                GameId = gameInning.GameId;
                InningNumber = gameInning.InningNumber;
            }
        }

        public bool HasData { get; set; }

        public Guid GameInningId { get; set; }
        public Guid GameId { get; set; }
        public int InningNumber { get; set; }
    }
}

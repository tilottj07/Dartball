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
        }

        public GameInning(IGameInning gameInning) {
            if (gameInning != null) {
                GameInningId = gameInning.GameInningId;
                GameId = gameInning.GameId;
                InningNumber = gameInning.InningNumber;
            }
        }


        public Guid GameInningId { get; set; }
        public Guid GameId { get; set; }
        public int InningNumber { get; set; }
    }
}

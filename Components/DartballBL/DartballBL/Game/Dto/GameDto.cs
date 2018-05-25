using Dartball.BusinessLayer.Game.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Dto
{
    public class GameDto : IGame
    {

        public int GameId { get; set; }
        public Guid GameAlternateKey { get; set; }
        public Guid LeagueAlternateKey { get; set; }
        public DateTime GameDate { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

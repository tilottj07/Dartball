using Dartball.BusinessLayer.Game.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Dto
{
    public class GameInningDto : IGameInning
    {

        public Guid GameInningId { get; set; }
        public Guid GameId { get; set; }
        public int InningNumber { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }


    }
}

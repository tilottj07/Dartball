using Dartball.BusinessLayer.Game.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Dto
{
    public class GameTeamDto : IGameTeam
    {

        public Guid GameTeamId { get; set; }
        public Guid GameId { get; set; }
        public Guid TeamId { get; set; }
        public int TeamBattingSequence { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

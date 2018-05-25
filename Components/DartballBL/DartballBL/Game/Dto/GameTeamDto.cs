using Dartball.BusinessLayer.Game.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Dto
{
    public class GameTeamDto : IGameTeam
    {

        public int GameTeamId { get; set; }
        public Guid GameTeamAlternateKey { get; set; }
        public Guid GameAlternateKey { get; set; }
        public Guid TeamAlternateKey { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

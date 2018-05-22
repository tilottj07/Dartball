using Dartball.BusinessLayer.Player.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Player.Dto
{
    public class PlayerTeamDto : IPlayerTeam
    {

        public int PlayerTeamId { get; set; }
        public Guid PlayerTeamAlternateKey { get; set; }
        public Guid PlayerAlternateKey { get; set; }
        public Guid TeamAlternateKey { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

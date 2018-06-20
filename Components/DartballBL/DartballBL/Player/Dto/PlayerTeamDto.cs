using Dartball.BusinessLayer.Player.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Player.Dto
{
    public class PlayerTeamDto : IPlayerTeam
    {

        public Guid PlayerTeamId { get; set; }
        public Guid PlayerId { get; set; }
        public Guid TeamId { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

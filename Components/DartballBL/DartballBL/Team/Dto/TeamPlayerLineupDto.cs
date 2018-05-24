using Dartball.BusinessLayer.Team.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Team.Dto
{
    public class TeamPlayerLineupDto : ITeamPlayerLineup
    {
        public int TeamPlayerLineupId { get; set; }
        public Guid TeamPlayerLineupAlternateKey { get; set; }
        public Guid TeamAlternateKey { get; set; }
        public Guid PlayerAlternateKey { get; set; }
        public int BattingOrder { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

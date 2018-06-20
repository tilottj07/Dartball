using Dartball.BusinessLayer.Team.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Team.Dto
{
    public class TeamPlayerLineupDto : ITeamPlayerLineup
    {
        public Guid TeamPlayerLineupId { get; set; }
        public Guid TeamId { get; set; }
        public Guid PlayerId { get; set; }
        public int BattingOrder { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

using Dartball.BusinessLayer.Team.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Team.Dto
{
    public class TeamDto : ITeam
    {

        public Guid TeamId { get; set; }
        public Guid LeagueId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int? Handicap { get; set; }
        public bool ShouldSync { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

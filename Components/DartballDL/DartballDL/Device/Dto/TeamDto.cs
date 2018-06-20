using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.DataLayer.Device.Dto
{
    public class TeamDto
    {

        public int TeamId { get; set; }
        public string TeamId { get; set; }
        public string LeagueId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int? Handicap { get; set; }
        public int ShouldSync { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

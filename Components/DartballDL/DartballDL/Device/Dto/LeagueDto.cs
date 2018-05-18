using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.DataLayer.Device.Dto
{
    public class LeagueDto
    {
        public int LeagueId { get; set; }
        public string LeagueAlternateKey { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.DataLayer.Device.Dto
{
    public class TeamPlayerLineupDto
    {
        public int TeamPlayerLineupId { get; set; }
        public string TeamPlayerLineupAlternateKey { get; set; }
        public string TeamAlternateKey { get; set; }
        public string PlayerAlternateKey { get; set; }
        public int BattingOrder { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

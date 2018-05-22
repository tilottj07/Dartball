using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.DataLayer.Device.Dto
{
    public class PlayerTeamDto
    {

        public int PlayerTeamId { get; set; }
        public string PlayerTeamAlternateKey { get; set; }
        public string PlayerAlternateKey { get; set; }
        public string TeamAlternateKey { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

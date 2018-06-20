using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.DataLayer.Device.Dto
{
    public class PlayerTeamDto
    {

        public int PlayerTeamId { get; set; }
        public string PlayerTeamId { get; set; }
        public string PlayerId { get; set; }
        public string TeamId { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

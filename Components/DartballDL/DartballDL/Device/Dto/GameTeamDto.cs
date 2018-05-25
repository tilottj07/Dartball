using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.DataLayer.Device.Dto
{
    public class GameTeamDto
    {
        public int GameTeamId { get; set; }
        public string GameTeamAlternateKey { get; set; }
        public string GameAlternateKey { get; set; }
        public string TeamAlternateKey { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

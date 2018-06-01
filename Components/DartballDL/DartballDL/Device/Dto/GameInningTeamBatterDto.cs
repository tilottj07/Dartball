using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.DataLayer.Device.Dto
{
    public class GameInningTeamBatterDto
    {
        public int GameInningTeamBatterId { get; set; }
        public string GameInningTeamBatterAlternateKey { get; set; }
        public string GameInningTeamAlternateKey { get; set; }
        public string PlayerAlternateKey { get; set; }
        public int Sequence { get; set; }
        public int EventType { get; set; }
        public int TargetEventType { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

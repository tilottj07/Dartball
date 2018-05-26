using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.DataLayer.Device.Dto
{
    public class GameInningDto
    {

        public int GameInningId { get; set; }
        public string GameInningAlternateKey { get; set; }
        public string GameAlternateKey { get; set; }
        public int InningNumber { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

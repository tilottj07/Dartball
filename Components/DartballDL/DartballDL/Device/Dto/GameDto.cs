using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.DataLayer.Device.Dto
{
    public class GameDto
    {

        public int GameId { get; set; }
        public string GameId { get; set; }
        public string LeagueId { get; set; }
        public DateTime GameDate { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

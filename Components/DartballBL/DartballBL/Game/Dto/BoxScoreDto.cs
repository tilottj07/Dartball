using System;
using System.Collections.Generic;
using Dartball.BusinessLayer.Game.Interface.Models;

namespace Dartball.BusinessLayer.Game.Dto
{
    public class BoxScoreDto : IBoxScore
    {
        public BoxScoreDto() {
            Innings = new List<IBoxScoreInning>();
            TeamNames = new List<string>();
        }
       
        public DateTime GameDate { get; set; }
        public List<IBoxScoreInning> Innings { get; set; }
        public List<string> TeamNames { get; set; }

    }
}

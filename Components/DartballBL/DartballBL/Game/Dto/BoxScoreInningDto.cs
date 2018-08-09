using System;
using System.Collections.Generic;
using Dartball.BusinessLayer.Game.Interface.Models;

namespace Dartball.BusinessLayer.Game.Dto
{
    public class BoxScoreInningDto : IBoxScoreInning
    {
        public BoxScoreInningDto() {
            InningTeams = new List<IBoxScoreHalfInning>();
        }

        public int InningNumber { get; set; }
        public bool IsCurrentInning { get; set; }
        public Boolean IsTotalsInning { get; set; }
        public List<IBoxScoreHalfInning> InningTeams { get; set; }
    }
}

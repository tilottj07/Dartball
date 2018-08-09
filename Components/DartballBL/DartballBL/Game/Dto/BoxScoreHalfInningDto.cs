using System;
using Dartball.BusinessLayer.Game.Interface.Models;

namespace Dartball.BusinessLayer.Game.Dto
{
    public class BoxScoreHalfInningDto : IBoxScoreHalfInning
    {

        public string TeamName { get; set; }
        public int? Score { get; set; }


    }
}

using System;
using System.Collections.Generic;

namespace Dartball.BusinessLayer.Game.Interface.Models
{
    public interface IBoxScore
    {
        DateTime GameDate { get; }
        List<IBoxScoreInning> Innings { get; }
        List<string> TeamNames { get; }
    }
}

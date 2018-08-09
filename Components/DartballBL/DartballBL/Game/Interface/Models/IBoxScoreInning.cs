using System;
using System.Collections.Generic;

namespace Dartball.BusinessLayer.Game.Interface.Models
{
    public interface IBoxScoreInning
    {

        int InningNumber { get; }
        bool IsCurrentInning { get; }
        bool IsTotalsInning { get; }
        List<IBoxScoreHalfInning> InningTeams { get; }

    }
}

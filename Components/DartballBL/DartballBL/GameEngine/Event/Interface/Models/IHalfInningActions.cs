using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.GameEngine.Event.Interface.Models
{
    public interface IHalfInningActions
    {
        bool AdvanceToNextHalfInning { get; }
        int TotalOuts { get; }

        bool IsRunnerOnFirst { get; }
        bool IsRunnerOnSecond { get; }
        bool IsRunnerOnThird { get; }

        int TotalRuns { get; }

    }
}

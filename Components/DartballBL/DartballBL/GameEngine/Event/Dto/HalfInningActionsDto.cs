using Dartball.BusinessLayer.GameEngine.Event.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.GameEngine.Event.Dto
{
    public class HalfInningActionsDto : IHalfInningActions
    {

        public bool AdvanceToNextHalfInning { get; set; }
        public int TotalOuts { get; set; }


        public bool IsRunnerOnFirst { get; set; }
        public bool IsRunnerOnSecond { get; set; }
        public bool IsRunnerOnThird { get; set; }

        public int TotalRuns { get; set; }

    }
}

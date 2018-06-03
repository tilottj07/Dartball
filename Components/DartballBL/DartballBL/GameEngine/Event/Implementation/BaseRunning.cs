using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.GameEngine.Event.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Dartball.BusinessLayer.Game.Implementation.GameEventService;

namespace Dartball.BusinessLayer.GameEngine.Event.Implementation
{
    public class BaseRunning
    {

        public static HalfInningActionsDto PopulateHalfInningBaseRunnersTotalRuns(
            HalfInningActionsDto dto, List<IGameInningTeamBatter> gameInningTeamBatters)
        {
            dto.IsRunnerOnFirst = false;
            dto.IsRunnerOnSecond = false;
            dto.IsRunnerOnThird = false;
            dto.TotalRuns = 0;

            foreach (var item in gameInningTeamBatters.OrderBy(x => x.Sequence))
            {
                switch((EventType)item.EventType)
                {
                    case EventType.Single:
                        if (dto.IsRunnerOnThird)
                        {
                            dto.IsRunnerOnThird = false;
                            dto.TotalRuns++;
                        }
                        if (dto.IsRunnerOnSecond)
                        {
                            dto.IsRunnerOnSecond = false;
                            dto.IsRunnerOnThird = true;
                        }
                        if (dto.IsRunnerOnFirst)
                        {
                            dto.IsRunnerOnSecond = true;
                            dto.IsRunnerOnFirst = false;
                        }
                        dto.IsRunnerOnFirst = true;
                        break;

                    case EventType.Double:
                        if (dto.IsRunnerOnThird)
                        {
                            dto.IsRunnerOnThird = false;
                            dto.TotalRuns++;
                        }
                        if (dto.IsRunnerOnSecond)
                        {
                            dto.IsRunnerOnSecond = false;
                            dto.TotalRuns++;
                        }
                        if (dto.IsRunnerOnFirst)
                        {
                            dto.IsRunnerOnFirst = false;
                            dto.IsRunnerOnThird = true;
                        }
                        dto.IsRunnerOnSecond = true;
                        break;

                    case EventType.Triple:
                        if (dto.IsRunnerOnThird)
                        {
                            dto.IsRunnerOnThird = false;
                            dto.TotalRuns++;
                        }
                        if (dto.IsRunnerOnSecond)
                        {
                            dto.IsRunnerOnSecond = false;
                            dto.TotalRuns++;
                        }
                        if (dto.IsRunnerOnFirst)
                        {
                            dto.IsRunnerOnFirst = false;
                            dto.TotalRuns++;
                        }
                        dto.IsRunnerOnThird = true;
                        break;

                    case EventType.HomeRun:
                        if (dto.IsRunnerOnThird)
                        {
                            dto.IsRunnerOnThird = false;
                            dto.TotalRuns++;
                        }
                        if (dto.IsRunnerOnSecond)
                        {
                            dto.IsRunnerOnSecond = false;
                            dto.TotalRuns++;
                        }
                        if (dto.IsRunnerOnFirst)
                        {
                            dto.IsRunnerOnFirst = false;
                            dto.TotalRuns++;
                        }
                        break;
                }
            }

            return dto;
        }

    }
}

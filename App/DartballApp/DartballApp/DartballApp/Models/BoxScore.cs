using System;
using System.Collections.Generic;
using System.Linq;
using Dartball.BusinessLayer.Game.Interface.Models;

namespace DartballApp.Models
{
    public class BoxScore
    {
        public BoxScore(IBoxScore boxScore)
        {
            Innings = new List<BoxScoreInning>();
            AwayTeamName = string.Empty;
            HomeTeamName = string.Empty;

            if (boxScore != null)
            {
                GameDate = boxScore.GameDate;

                foreach (var inning in boxScore.Innings)
                {
                    Innings.Add(new BoxScoreInning(inning));
                }

                int index = 0;
                foreach (string teamName in boxScore.TeamNames)
                {
                    if (index == 0) AwayTeamName = teamName;
                    else if (index == 1) HomeTeamName = teamName;

                    index++;
                }
            }


        }

        public DateTime GameDate { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }

        public List<BoxScoreInning> Innings { get; set; }



        public class BoxScoreInning {
            public BoxScoreInning(IBoxScoreInning boxScoreInning) {
                InningTeams = new List<BoxScoreHalfInning>();
                if (boxScoreInning != null) {
                    InningNumber = boxScoreInning.InningNumber;
                    IsCurrentInning = boxScoreInning.IsCurrentInning;

                    foreach(var item in boxScoreInning.InningTeams) {
                        InningTeams.Add(new BoxScoreHalfInning(item));
                    }
                }
            }

            public int InningNumber { get; set; }
            public bool IsCurrentInning { get; set; }
            public List<BoxScoreHalfInning> InningTeams { get; set; }
        }

        public class BoxScoreHalfInning {
            public BoxScoreHalfInning(IBoxScoreHalfInning boxScoreHalfInning) {

                TeamName = string.Empty;

                if (boxScoreHalfInning != null) {
                    TeamName = boxScoreHalfInning.TeamName;
                    Score = boxScoreHalfInning.Score.HasValue ? boxScoreHalfInning.Score.ToString() : string.Empty;
                }
            }

            public string TeamName { get; set; }
            public string Score { get; set; }

        }
    }
}

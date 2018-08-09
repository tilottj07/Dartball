using System;
using System.Collections.Generic;
using System.Linq;
using Dartball.BusinessLayer.Game.Dto;
using Dartball.BusinessLayer.Game.Interface;
using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.Team.Implementation;
using Dartball.BusinessLayer.Team.Interface;
using Dartball.BusinessLayer.Team.Interface.Models;

namespace Dartball.BusinessLayer.Game.Implementation
{
    public class BoxScoreService : IBoxScoreService
    {
        ITeamService Team;
        IGameService Game;
        IGameTeamService GameTeam;
        IGameInningService GameInning;
        IGameInningTeamService GameInningTeam;

        public BoxScoreService()
        {
            Team = new TeamService();
            Game = new GameService();
            GameTeam = new GameTeamService();
            GameInning = new GameInningService();
            GameInningTeam = new GameInningTeamService();
        }



        public IBoxScore GetBoxScore(Guid gameId) {
            BoxScoreDto boxScore = new BoxScoreDto();

            var game = Game.GetGame(gameId);
            if (game != null && !game.DeleteDate.HasValue)
            {
                boxScore.GameDate = game.GameDate;

                var gameTeams = GameTeam.GetGameTeams(gameId).OrderBy(x => x.TeamBattingSequence).ToList();
                var teams = GetTeams(gameTeams);
                var gameInnings = GameInning.GetGameInnings(gameId).OrderBy(x => x.InningNumber).ToList();
                var gameInningTeams = GameInningTeam.GetTeamInningsForEntireGame(gameId);

                foreach(var team in teams) {
                    boxScore.TeamNames.Add(team.Name);
                }

                //always add all 9 innings for a standard game
                for (int i = 1; i <= 9; i++)
                {
                    var inning = gameInnings.FirstOrDefault(x => x.InningNumber == i);
                    if (inning != null)
                    {
                        foreach (var gameTeam in gameTeams)
                        {
                            BoxScoreInningDto boxScoreInning = new BoxScoreInningDto()
                            {
                                InningNumber = inning.InningNumber,
                                IsCurrentInning = gameInnings.IndexOf(inning) == (gameInnings.Count - 1)
                            };

                            var team = teams.FirstOrDefault(x => x.TeamId == gameTeam.TeamId);
                            if (team != null)
                            {
                                foreach (var item in gameInningTeams.Where(x => x.GameInningId == inning.GameInningId && x.GameTeamId == gameTeam.GameTeamId))
                                {
                                    BoxScoreHalfInningDto boxScoreHalfInning = new BoxScoreHalfInningDto()
                                    {
                                        TeamName = team.Name,
                                        Score = item.Score
                                    };
                                    boxScoreInning.InningTeams.Add(boxScoreHalfInning);
                                }
                            }

                            boxScore.Innings.Add(boxScoreInning);
                        }
                    }
                    else
                    {
                        BoxScoreInningDto boxScoreInning = new BoxScoreInningDto()
                        {
                            InningNumber = i,
                            IsCurrentInning = false
                        };
                        foreach (var team in teams)  {
                            boxScoreInning.InningTeams.Add(new BoxScoreHalfInningDto() { TeamName = team.Name });
                        }
                        boxScore.Innings.Add(boxScoreInning);
                    }
                }


                //add any potentail extra innings
                var extraInnings = gameInnings.Where(x => x.InningNumber > 9).ToList();
                foreach (var inning in extraInnings)
                {
                    foreach (var gameTeam in gameTeams)
                    {
                        BoxScoreInningDto boxScoreInning = new BoxScoreInningDto()
                        {
                            InningNumber = inning.InningNumber,
                            IsCurrentInning = extraInnings.IndexOf(inning) == (extraInnings.Count - 1)
                        };

                        var team = teams.FirstOrDefault(x => x.TeamId == gameTeam.TeamId);
                        if (team != null) {

                            foreach(var item in gameInningTeams.Where(x => x.GameInningId == inning.GameInningId && x.GameTeamId == gameTeam.GameTeamId)) {
                                BoxScoreHalfInningDto boxScoreHalfInning = new BoxScoreHalfInningDto()
                                {
                                    TeamName = team.Name,
                                    Score = item.Score
                                };
                                boxScoreInning.InningTeams.Add(boxScoreHalfInning);
                            }
                        }

                        boxScore.Innings.Add(boxScoreInning);
                    }
                }


                //add totals 
                BoxScoreInningDto boxScoreTotalsInning = new BoxScoreInningDto()
                {
                    InningNumber = int.MaxValue,
                    IsCurrentInning = false,
                    IsTotalsInning = true
                };
                foreach(var team in teams) {
                    BoxScoreHalfInningDto boxScoreHalfInning = new BoxScoreHalfInningDto()
                    {
                        TeamName = team.Name,
                        Score = boxScore.Innings.Sum(x => x.InningTeams
                                                .Where(y => y.TeamName == team.Name && y.Score.HasValue)
                                                .Sum(t => t.Score.Value))
                    };
                    boxScoreTotalsInning.InningTeams.Add(boxScoreHalfInning);
                }

                boxScore.Innings.Add(boxScoreTotalsInning);

            }

            return boxScore;
        }


        List<ITeam> GetTeams(List<IGameTeam> gameTeams) {
            return Team.GetTeams(gameTeams.Select(x => x.TeamId).ToList());
        }
    }
}

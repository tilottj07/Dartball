using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dartball.BusinessLayer.Game.Dto;
using Dartball.BusinessLayer.Game.Implementation;
using Dartball.BusinessLayer.Game.Interface;
using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.GameEngine.InGame.Implementation;
using Dartball.BusinessLayer.GameEngine.InGame.Interface;
using Dartball.BusinessLayer.Player.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using Dartball.BusinessLayer.Team.Implementation;
using Dartball.BusinessLayer.Team.Interface;
using Dartball.BusinessLayer.Team.Interface.Models;
using static Dartball.BusinessLayer.Game.Implementation.GameEventService;

namespace DartballApp.ViewModels.Game
{
    public class PlayGameViewModel
    {
        IBoxScoreService BoxScore;
        ITeamService Team;
        ITeamPlayerLineupService TeamPlayerLineup;
        IGameService Game;
        IGameTeamService GameTeam;
        IGameInningService GameInning;
        IGameInningTeamService GameInningTeam;
        IGameInningTeamBatterService GameInningTeamBatter;
        IHalfInningService HalfInning;

        public PlayGameViewModel(Guid gameId)
        {
            GameId = gameId;
            BoxScore = new BoxScoreService();
            Team = new TeamService();
            TeamPlayerLineup = new TeamPlayerLineupService();
            Game = new GameService();
            GameTeam = new GameTeamService();
            GameInning = new GameInningService();
            GameInningTeam = new GameInningTeamService();
            GameInningTeamBatter = new GameInningTeamBatterService();
            HalfInning = new HalfInningService();

            PreLoadGameData();
        }


        void PreLoadGameData()
        {
            CurrentGame = new Models.Game(Game.GetGame(GameId));
            GameTeams = GameTeam.GetGameTeams(GameId).Where(y => !y.DeleteDate.HasValue).OrderBy(y => y.TeamBattingSequence).ToList();

            //Fill Teams
            Teams = Team.GetTeams(GameTeams.Select(y => y.TeamId).ToList());

            //load all players in this game
            AllPlayers = new List<IPlayer>();
            foreach (var gt in GameTeams)
            {
                foreach (var player in TeamPlayerLineup.GetTeamSortedBattingOrderPlayers(gt.TeamId)) AllPlayers.Add(player);                
            }
        }

        Models.Game CurrentGame { get; set; }
        Models.GameInning CurrentGameInning { get; set; }
        Models.GameInningTeam CurrentGameInningTeam { get; set; }
        Models.GameInningTeamBatter CurrentGameInningTeamBatter { get; set; }
        Models.Team CurrentTeam { get; set; }
        Models.Player CurrentPlayer { get; set; }


        List<IGameTeam> GameTeams { get; set; }
        List<ITeam> Teams { get; set; }

        List<IPlayer> AllPlayers { get; set; }


        public Guid GameId { get; set; }
        public Models.BoxScore GameBoxScore { get; set; }

        public int CurrentInning { get { return CurrentGameInning != null ? CurrentGameInning.InningNumber : 0; } }
        public string CurrentInningDisplay { get { return $"Inning #: {CurrentInning}"; } }

        public Guid AtBatTeamId { get { return CurrentTeam != null ? CurrentTeam.TeamId : Guid.Empty; } }
        public string AtBatTeam { get { return CurrentTeam != null ? CurrentTeam.Name : string.Empty; } }




        public Guid PlayerId { get { return CurrentPlayer != null ? CurrentPlayer.PlayerId : Guid.Empty; } }
        public string AtBatPlayer { get { return CurrentPlayer != null ? CurrentPlayer.DisplayName : string.Empty; } }
        public int InningBatterSequence { get { return CurrentGameInningTeamBatter != null ? CurrentGameInningTeamBatter.Sequence : 0; } }


        public int Outs { get; set; }
        public string OutsDisplay { get { return $"Inning Outs: {Outs}"; } }
        public int Runs { get; set; }
        public string RunsDisplay { get { return $"Inning Runs: {Runs}"; } }
        public bool IsRunnerOnFirst { get; set; }
        public bool IsRunnerOnSecond { get; set; }
        public bool IsRunnerOnThird { get; set; }
        bool AdvanceToNextHalfInning { get; set; }

        public string RunnersOnDisplay {
            get {
                StringBuilder sb = new StringBuilder();

                sb.Append("Runners On: ");
                if (IsRunnerOnFirst) sb.Append("1 ");
                if (IsRunnerOnSecond) sb.Append("2 ");
                if (IsRunnerOnThird) sb.Append("3 ");

                return sb.ToString().Trim();
            }
        }



        public void FillBoxScore()
        {
            GameBoxScore = new Models.BoxScore(BoxScore.GetBoxScore(GameId));
        }


        public void InitializeGame()
        {
            FillCurrentInning();
            if (!CurrentGameInning.HasData)
            {
                AdvanceToNextInning(isBeginningOfGame: true);
            }

            FillCurrentAtBatTeam();
            if (!CurrentGameInningTeam.HasData)
            {
                AdvanceToNextInningTeam(isBeginingOfGame: true);
            }

            FillCurrentAtBatTeamPlayer();
            if (!CurrentGameInningTeamBatter.HasData)
            {
                AdvanceToNextInningTeamBatter();
            }
        }


        public void FillCurrentInning()
        {
            CurrentGameInning = new Models.GameInning(GameInning.GetCurrentGameInning(GameId));
        }


        public void FillCurrentAtBatTeam()
        {
            CurrentGameInningTeam = new Models.GameInningTeam(GameInningTeam.GetCurrentGameInningTeam(GameId));
            var gameTeam = GameTeams.FirstOrDefault(y => y.GameTeamId == CurrentGameInningTeam.GameTeamId);
            if (gameTeam != null) {
                CurrentTeam = new Models.Team(Teams.FirstOrDefault(y => y.TeamId == gameTeam.TeamId));
            }
        }



        public void FillCurrentAtBatTeamPlayer()
        {
            if (CurrentGameInning != null && CurrentGameInningTeam != null) {
                CurrentGameInningTeamBatter = new Models.GameInningTeamBatter(GameInningTeamBatter.GetCurrentGameInningTeamBatter(GameId));
                CurrentPlayer = new Models.Player(AllPlayers.FirstOrDefault(y => y.PlayerId == CurrentGameInningTeamBatter.PlayerId));
            }
        }


        public void SaveEventType(EventType eventType)
        {
            if (CurrentGameInning != null && CurrentGameInningTeam != null && CurrentGameInningTeamBatter != null)
            {
                //update the event for the current batter
                var currentBatter = GameInningTeamBatter.GetCurrentGameInningTeamBatter(GameId);
                if (currentBatter != null)
                {
                    GameInningTeamBatterDto gameInningTeamBatter = new GameInningTeamBatterDto()
                    {
                        GameInningTeamBatterId = currentBatter.GameInningTeamBatterId,
                        GameInningTeamId = currentBatter.GameInningTeamId,
                        EventType = (int)eventType,
                        PlayerId = currentBatter.PlayerId,
                        Sequence = currentBatter.Sequence,
                        RBIs = currentBatter.RBIs
                    };
                    GameInningTeamBatter.Update(gameInningTeamBatter);

                    //update outs, runs, and baserunners 
                    var halfInningActions = HalfInning.GetHalfInningActions(CurrentGameInningTeam.GameInningTeamId);
                    Outs = halfInningActions.TotalOuts;
                    Runs = halfInningActions.TotalRuns;
                    IsRunnerOnFirst = halfInningActions.IsRunnerOnFirst;
                    IsRunnerOnSecond = halfInningActions.IsRunnerOnSecond;
                    IsRunnerOnThird = halfInningActions.IsRunnerOnThird;
                    AdvanceToNextHalfInning = halfInningActions.AdvanceToNextHalfInning;

                    //update total runs 
                    var currentInningTeam = GameInningTeam.GetCurrentGameInningTeam(GameId);
                    if (currentInningTeam != null)
                    {
                        GameInningTeamDto gameInningTeam = new GameInningTeamDto()
                        {
                            GameTeamId = currentInningTeam.GameTeamId,
                            GameInningTeamId = currentInningTeam.GameInningTeamId,
                            GameInningId = currentInningTeam.GameInningId,
                            IsRunnerOnFirst = IsRunnerOnFirst,
                            IsRunnerOnSecond = IsRunnerOnSecond,
                            IsRunnerOnThird = IsRunnerOnThird,
                            Outs = Outs,
                            Score = Runs
                        };
                        GameInningTeam.Update(gameInningTeam);
                    }
                }

                AdvanceToNextInningTeamBatter();
            }
        }


        void AdvanceToNextInning(bool isBeginningOfGame = false)
        {
            GameInningDto gameInning = new GameInningDto()
            {
                InningNumber = GameInning.GetNextGameInningNumber(GameId),
                GameId = GameId
            };
            GameInning.Save(gameInning);
            FillCurrentInning();
            AdvanceToNextInningTeam(isBeginningOfGame);
        }


        void AdvanceToNextInningTeam(bool isBeginingOfGame = false)
        {
            ClearBoard();
            if (CurrentGameInning != null)
            {
                Guid? nextAtBatTeamId = GameInningTeam.GetNextAtBatTeamId(GameId);
                if (nextAtBatTeamId.HasValue)
                {
                    var atBatGameTeam = GameTeams.FirstOrDefault(y => y.TeamId == nextAtBatTeamId.Value);

                    bool shouldAdvanceInning = GameTeams.IndexOf(atBatGameTeam) == 0 && isBeginingOfGame == false;
                    if (shouldAdvanceInning == false)
                    {
                        GameInningTeamDto gameInningTeam = new GameInningTeamDto()
                        {
                            GameTeamId = atBatGameTeam.GameTeamId,
                            GameInningId = CurrentGameInning.GameInningId,
                            IsRunnerOnFirst = false,
                            IsRunnerOnThird = false,
                            IsRunnerOnSecond = false,
                            Outs = 0,
                            Score = 0
                        };
                        GameInningTeam.AddNew(gameInningTeam);
                        FillCurrentAtBatTeam();
                        AdvanceToNextInningTeamBatter();
                    }
                    else
                    {
                        if (IsGameOver() == true) GameComplete();
                        else
                        {
                            AdvanceToNextInning();
                        }
                    }
                }
            }
        }




        void AdvanceToNextInningTeamBatter()
        {
            if (AdvanceToNextHalfInning == true) AdvanceToNextInningTeam();
            else
            {
                if (CurrentGameInning != null && CurrentGameInningTeam != null)
                {
                    Guid? nextPlayerToBat = GameInningTeamBatter.GetNextGameBatterPlayerId(GameId, AtBatTeamId);
                    if (nextPlayerToBat.HasValue)
                    {
                        int batterSequence = CurrentGameInningTeamBatter != null ? CurrentGameInningTeamBatter.Sequence + 1 : 0;

                        GameInningTeamBatterDto gameInningTeamBatter = new GameInningTeamBatterDto()
                        {
                            PlayerId = nextPlayerToBat.Value,
                            GameInningTeamId = CurrentGameInningTeam.GameInningTeamId,
                            EventType = (int)EventType.Unknown,
                            RBIs = 0,
                            Sequence = InningBatterSequence
                        };
                        GameInningTeamBatter.Save(gameInningTeamBatter);
                        FillCurrentAtBatTeamPlayer();
                    }
                }
            }
        }


        void GameComplete()
        {
            //TODO
        }



        void ClearBoard() {
            Runs = 0;
            Outs = 0;
            IsRunnerOnFirst = false;
            IsRunnerOnSecond = false;
            IsRunnerOnThird = false;
            AdvanceToNextHalfInning = false;
        }

        bool IsGameOver()
        {
            bool isOver = false;
            if (CurrentInning >= 9)
            {
                var gameScores = Game.GetGameTeamScores(GameId);
                if (gameScores.Select(y => y.Item2).Distinct().Count() > 1) isOver = true;
            }

            return isOver;
        }
    }
}

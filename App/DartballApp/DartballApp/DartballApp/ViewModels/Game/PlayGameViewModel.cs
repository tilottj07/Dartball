using System;
using System.Collections.Generic;
using System.Linq;
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
            GameTeams = GameTeam.GetGameTeams(GameId).Where(y => !y.DeleteDate.HasValue).OrderBy(y => y.TeamBattingSequence).ToList();

            //Fill Teams
            Teams = Team.GetTeams(GameTeams.Select(y => y.TeamId).ToList());

            //Fill Team Lineups and players
            TeamLineupDict = new Dictionary<Guid, List<IPlayer>>();
            AllPlayers = new List<IPlayer>();
            foreach (var gt in GameTeams)
            {
                if (!TeamLineupDict.ContainsKey(gt.GameTeamId)) TeamLineupDict.Add(gt.GameTeamId, new List<IPlayer>());
                foreach (var player in TeamPlayerLineup.GetTeamSortedBattingOrderPlayers(gt.TeamId))
                {
                    TeamLineupDict[gt.GameTeamId].Add(player);
                    AllPlayers.Add(player);
                }
            }
        }


        List<IGameTeam> GameTeams { get; set; }
        List<ITeam> Teams { get; set; }

        Dictionary<Guid, List<IPlayer>> TeamLineupDict { get; set; }
        List<IPlayer> AllPlayers { get; set; }


        public Guid GameId { get; set; }
        public Models.BoxScore GameBoxScore { get; set; }

        public int CurrentInning { get; set; }
        public Guid CurrentGameInningId { get; set; }
        Guid CurrentGameInningTeamId { get; set; }

        public Guid AtBatGameTeamId { get; set; }
        public string AtBatTeam { get; set; }


        public Guid PlayerId { get; set; }
        public string AtBatPlayer { get; set; }
        public int InningBatterSequence { get; set; }


        public int Outs { get; set; }
        public int Runs { get; set; }
        public bool IsRunnerOnFirst { get; set; }
        public bool IsRunnerOnSecond { get; set; }
        public bool IsRunnerOnThird { get; set; }



        public void FillBoxScore()
        {
            GameBoxScore = new Models.BoxScore(BoxScore.GetBoxScore(GameId));
        }


        public ChangeResult InitializeGame()
        {
            //add inning 1
            CurrentGameInningId = Guid.NewGuid();
            GameInningDto gameInning = new GameInningDto()
            {
                InningNumber = 1,
                GameId = GameId,
                GameInningId = CurrentGameInningId
            };

            var result = GameInning.Save(gameInning);

            return result;
        }


        public void FillCurrentInning()
        {
            var currentInning = GameInning.GetCurrentGameInning(GameId);
            if (currentInning != null)
            {
                CurrentInning = currentInning.InningNumber;
                CurrentGameInningId = currentInning.GameInningId;
            }
        }


        public void FillCurrentAtBatTeam()
        {
            var currentTeam = GameInningTeam.GetCurrentGameInningTeam(GameId);
            if (currentTeam == null)
            {
                AdvanceToNextInningTeam();
                currentTeam = GameInningTeam.GetCurrentGameInningTeam(GameId);
            }

            AtBatTeam = string.Empty;
            if (currentTeam != null)
            {
                AtBatGameTeamId = currentTeam.GameTeamId;
                CurrentGameInningTeamId = currentTeam.GameInningTeamId;
                var gt = GameTeams.FirstOrDefault(y => y.GameTeamId == currentTeam.GameTeamId);
                if (gt != null)
                {
                    var team = Teams.FirstOrDefault(y => y.TeamId == gt.TeamId);
                    if (team != null)
                    {
                        AtBatTeam = team.Name;
                    }
                }
            }
        }



        public void FillCurrentAtBatTeamPlayer()
        {
            var currentBatter = GameInningTeamBatter.GetCurrentGameInningTeamBatter(GameId);
            if (currentBatter == null)
            {
                AdvanceToNextInningTeamBatter();
                currentBatter = GameInningTeamBatter.GetCurrentGameInningTeamBatter(GameId);
            }

            if (currentBatter != null)
            {
                PlayerId = currentBatter.PlayerId;
                var playerInfo = AllPlayers.FirstOrDefault(y => y.PlayerId == currentBatter.PlayerId);
                if (playerInfo != null)
                {
                    AtBatPlayer = $"{playerInfo.Name} {playerInfo.LastName}".Trim();
                }
            }
        }


        public void SaveEventType(EventType eventType) {

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
                var halfInningActions = HalfInning.GetHalfInningActions(CurrentGameInningTeamId);
                Outs = halfInningActions.TotalOuts;
                Runs = halfInningActions.TotalRuns;
                IsRunnerOnFirst = halfInningActions.IsRunnerOnFirst;
                IsRunnerOnSecond = halfInningActions.IsRunnerOnSecond;
                IsRunnerOnThird = halfInningActions.IsRunnerOnThird;

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


        void AdvanceToNextInning()
        {
            CurrentInning++;
            GameInningDto gameInning = new GameInningDto()
            {
                GameId = GameId,
                InningNumber = CurrentInning,
            };
            GameInning.Save(gameInning);
        }


        void AdvanceToNextInningTeam()
        {
            if (AtBatGameTeamId == Guid.Empty) AtBatGameTeamId = GameTeams.FirstOrDefault().GameTeamId;

            var gameTeam = GameTeams.FirstOrDefault(y => y.GameTeamId == AtBatGameTeamId);
            if (gameTeam != null)
            {
                int nextGameTeamIndex = GameTeams.IndexOf(gameTeam) + 1;
                bool advanceToNextInning = false;

                if (nextGameTeamIndex > (GameTeams.Count - 1))
                {
                    nextGameTeamIndex = 0;
                    advanceToNextInning = true;
                }

                if (advanceToNextInning)
                {
                    bool isGameOver = false;
                    if (CurrentInning >= 9)
                    {
                        List<int> distinctGameScores = Game.GetGameTeamScores(GameId).Select(y => y.Item2).Distinct().ToList();
                        if (distinctGameScores.Count > 1) isGameOver = true;
                    }

                    if (isGameOver == true) GameComplete();
                    else
                    {
                        AdvanceToNextInning();
                        FillCurrentInning();
                        FillCurrentAtBatTeam();
                        FillCurrentAtBatTeamPlayer();
                    }
                }
                else
                {
                    var nextGameTeam = GameTeams[nextGameTeamIndex];
                    GameInningTeamDto gameInningTeam = new GameInningTeamDto()
                    {
                        GameTeamId = nextGameTeam.GameTeamId,
                        GameInningId = CurrentGameInningId,
                        IsRunnerOnFirst = false,
                        IsRunnerOnThird = false,
                        IsRunnerOnSecond = false,
                        Outs = 0,
                        Score = 0
                    };
                    GameInningTeam.AddNew(gameInningTeam);
                }
            }
        }


        void AdvanceToNextInningTeamBatter()
        {
            if (TeamLineupDict.ContainsKey(AtBatGameTeamId))
            {
                var lineupPlayers = TeamLineupDict[AtBatGameTeamId];

                int playerIndex = 0;
                if (PlayerId != Guid.Empty)
                {
                    var atBatPlayer = lineupPlayers.FirstOrDefault(y => y.PlayerId == PlayerId);
                    playerIndex = lineupPlayers.IndexOf(atBatPlayer);
                }

                if (playerIndex >= (lineupPlayers.Count - 1)) AdvanceToNextInningTeam();                
                else
                {
                    var nextAtBatPlayer = lineupPlayers[playerIndex + 1];
                    InningBatterSequence++;

                    GameInningTeamBatterDto gameInningTeamBatter = new GameInningTeamBatterDto()
                    {
                        PlayerId = nextAtBatPlayer.PlayerId,
                        GameInningTeamId = CurrentGameInningTeamId,
                        EventType = (int)EventType.Unknown,
                        RBIs = 0,
                        Sequence = InningBatterSequence
                    };
                    GameInningTeamBatter.Save(gameInningTeamBatter);
                    FillCurrentAtBatTeamPlayer();
                }
            }
        }


        void GameComplete()
        {

        }





    }
}

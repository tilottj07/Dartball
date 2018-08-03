using System;
using System.Collections.Generic;
using System.Linq;
using Dartball.BusinessLayer.Game.Dto;
using Dartball.BusinessLayer.Game.Implementation;
using Dartball.BusinessLayer.Game.Interface;
using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.Player.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using Dartball.BusinessLayer.Team.Implementation;
using Dartball.BusinessLayer.Team.Interface;
using Dartball.BusinessLayer.Team.Interface.Models;

namespace DartballApp.ViewModels.Game
{
    public class PlayGameViewModel
    {
        IBoxScoreService BoxScore;
        ITeamService Team;
        ITeamPlayerLineupService TeamPlayerLineup;
        IGameTeamService GameTeam;
        IGameInningService GameInning;
        IGameInningTeamService GameInningTeam;

        public PlayGameViewModel(Guid gameId)
        {
            GameId = gameId;
            BoxScore = new BoxScoreService();
            Team = new TeamService();
            TeamPlayerLineup = new TeamPlayerLineupService();
            GameTeam = new GameTeamService();
            GameInning = new GameInningService();
            GameInningTeam = new GameInningTeamService();

            PreLoadGameData();
        }


        void PreLoadGameData()
        {
            GameTeams = GameTeam.GetGameTeams(GameId).Where(y => !y.DeleteDate.HasValue).OrderBy(y => y.TeamBattingSequence).ToList();

            //Fill Teams
            Teams = new List<ITeam>();
            foreach (var gt in GameTeams)
            {
                var team = Team.GetTeam(gt.TeamId);
                if (team != null) Teams.Add(team);
            }

            //Fill Team Lineups
            TeamLineupDict = new Dictionary<Guid, List<IPlayer>>();
            foreach (var gt in GameTeams)
            {
                if (!TeamLineupDict.ContainsKey(gt.GameTeamId)) TeamLineupDict.Add(gt.GameTeamId, new List<IPlayer>());
                foreach (var player in TeamPlayerLineup.GetTeamSortedBattingOrderPlayers(gt.TeamId))
                {
                    TeamLineupDict[gt.GameTeamId].Add(player);
                }
            }
        }


        List<IGameTeam> GameTeams { get; set; }
        List<ITeam> Teams { get; set; }

        Dictionary<Guid, List<IPlayer>> TeamLineupDict { get; set; }


        public Guid GameId { get; set; }
        public Models.BoxScore GameBoxScore { get; set; }

        public int CurrentInning { get; set; }
        public Guid CurrentGameInningId { get; set; }

        public Guid AtBatGameTeamId { get; set; }
        public string AtBatTeam { get; set; }


        public Guid PlayerId { get; set; }
        public string AtBatPlayer { get; set; }
        public int BattingOrderPosition { get; set; }




        public void FillBoxScore() {
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


        public void FillCurrentAtBatTeam() {
            var currentTeam = GameInningTeam.GetCurrentGameInningTeam(GameId);
            if (currentTeam == null) {
                //add inning team
                GameInningTeamDto gameInningTeam = new GameInningTeamDto()
                {
                    GameTeamId = GameTeams.FirstOrDefault().GameTeamId,
                    GameInningId = CurrentGameInningId,
                    IsRunnerOnFirst = false,
                    IsRunnerOnThird = false,
                    IsRunnerOnSecond = false,
                    Outs = 0,
                    Score = 0
                };
                GameInningTeam.AddNew(gameInningTeam);
                currentTeam = GameInningTeam.GetCurrentGameInningTeam(GameId);
            }

            AtBatTeam = string.Empty;
            if (currentTeam != null) {
                var gt = GameTeams.FirstOrDefault(y => y.GameTeamId == currentTeam.GameTeamId);
                if (gt != null) {
                    var team = Teams.FirstOrDefault(y => y.TeamId == gt.TeamId);
                    if (team != null) {
                        AtBatTeam = team.Name;
                    }
                }
            }
        }

    }
}

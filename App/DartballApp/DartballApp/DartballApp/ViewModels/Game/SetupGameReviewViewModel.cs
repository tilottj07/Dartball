using System;
using System.Collections.Generic;
using System.Linq;
using Dartball.BusinessLayer.Game.Implementation;
using Dartball.BusinessLayer.Game.Interface;
using Dartball.BusinessLayer.Team.Implementation;
using Dartball.BusinessLayer.Team.Interface;

namespace DartballApp.ViewModels.Game
{
    public class SetupGameReviewViewModel
    {
        ITeamService Team;
        ITeamPlayerLineupService Lineup;
        IGameTeamService GameTeam;

        public SetupGameReviewViewModel(Guid gameId)
        {
            GameId = gameId;

            Team = new TeamService();
            Lineup = new TeamPlayerLineupService();
            GameTeam = new GameTeamService();
        }

        public Guid GameId { get; set; }

        public string AwayTeamName { get; set; }
        public string HomeTeamName { get; set; }

        public string AwayTeamDisplayDescription {
            get {
                return $"Away Team: {AwayTeamName}";
            }
        }
        public string HomeTeamDisplayDescription {
            get {
                return $"Home Team: {HomeTeamName}";
            }
        }

        Guid AwayTeamId { get; set; }
        Guid HomeTeamId { get; set; }

        public List<Models.Player> AwayTeamLineup { get; set; }
        public List<Models.Player> HomeTeamLineup { get; set; }


        public void FillInformation() {
            FillTeamInformation();
            FillLineups();
        }

        void FillTeamInformation() {
            var gameTeams = GameTeam.GetGameTeams(GameId).OrderBy(y => y.TeamBattingSequence).ToList();
            if (gameTeams.Count >= 2)
            {
                var awayTeam = Team.GetTeam(gameTeams[0].TeamId);
                if (awayTeam != null)
                {
                    AwayTeamName = awayTeam.Name;
                    AwayTeamId = awayTeam.TeamId;
                }

                var homeTeam = Team.GetTeam(gameTeams[1].TeamId);
                if (homeTeam != null)
                {
                    HomeTeamName = homeTeam.Name;
                    HomeTeamId = homeTeam.TeamId;
                }
            }
        }

        void FillLineups() {
            AwayTeamLineup = new List<Models.Player>();
            HomeTeamLineup = new List<Models.Player>();

            foreach(var item in Lineup.GetTeamSortedBattingOrderPlayers(AwayTeamId)) {
                AwayTeamLineup.Add(new Models.Player(item));
            }

            foreach(var item in Lineup.GetTeamSortedBattingOrderPlayers(HomeTeamId)) {
                HomeTeamLineup.Add(new Models.Player(item));
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Dartball.BusinessLayer.Game.Dto;
using Dartball.BusinessLayer.Game.Implementation;
using Dartball.BusinessLayer.Game.Interface;
using Dartball.BusinessLayer.Shared.Models;
using Dartball.BusinessLayer.Team.Implementation;
using Dartball.BusinessLayer.Team.Interface;

namespace DartballApp.ViewModels.Game
{
    public class SetupGameViewModel
    {
        ITeamService Team;
        IGameService Game;
        IGameTeamService GameTeam;

        public SetupGameViewModel(Guid leagueId)
        {
            LeagueId = leagueId;

            Team = new TeamService();
            Game = new GameService();
            GameTeam = new GameTeamService();
        }


        public Guid LeagueId { get; set; }
        public List<Models.Team> LeagueTeams { get; set; }


        public List<Models.Team> AwayTeams { get; set; }
        public List<Models.Team> HomeTeams { get; set; }


        public void FillTeams() {
            LeagueTeams = new List<Models.Team>();
            foreach(var item in Team.GetTeams(LeagueId).OrderBy(y => y.Name)) {
                LeagueTeams.Add(new Models.Team(item));
            }

            AwayTeams = LeagueTeams;
            HomeTeams = LeagueTeams;
        }



        public ChangeResult SaveGameTeams(Models.Team awayTeam, Models.Team homeTeam) {

            //create game
            Guid gameId = Guid.NewGuid();
            var result = Game.AddNew(new GameDto()
            {
                GameId = gameId,
                GameDate = DateTime.UtcNow,
                LeagueId = LeagueId
            });

            //add away team to game
            if (result.IsSuccess) {
                result = GameTeam.Save(new GameTeamDto()
                {
                    GameId = gameId,
                    TeamId = awayTeam.TeamId,
                    TeamBattingSequence = 0
                });
            }

            //add home team to game
            if (result.IsSuccess) {
                result = GameTeam.Save(new GameTeamDto()
                {
                    GameId = gameId,
                    TeamId = homeTeam.TeamId,
                    TeamBattingSequence = 1
                });
            }

            return result;
        }

    }
}

using Dartball.BusinessLayer.Game.Dto;
using Dartball.BusinessLayer.Game.Implementation;
using Dartball.BusinessLayer.Game.Interface;
using Dartball.BusinessLayer.League.Dto;
using Dartball.BusinessLayer.League.Implementation;
using Dartball.BusinessLayer.League.Interface;
using Dartball.BusinessLayer.Player.Dto;
using Dartball.BusinessLayer.Player.Implementation;
using Dartball.BusinessLayer.Player.Interface;
using Dartball.BusinessLayer.Team.Dto;
using Dartball.BusinessLayer.Team.Implementation;
using Dartball.BusinessLayer.Team.Interface;
using System;

namespace DartballBLUnitTest.IntegrationValidation
{
    public class IntegrationBase
    {
        private ILeagueService League;
        private ITeamService Team;
        private IGameService Game;
        private IPlayerService Player;
        private IGameInningService GameInning;
        private IGameTeamService GameTeam;
        private IGameInningTeamService GameInningTeam;

        public IntegrationBase()
        {
            League = new LeagueService();
            Team = new TeamService();
            Game = new GameService();
            Player = new PlayerService();
            GameInning = new GameInningService();
            GameTeam = new GameTeamService();
            GameInningTeam = new GameInningTeamService();
        }


        public Guid SeedLeague()
        {
            Guid id = Guid.NewGuid();

            LeagueDto dto = new LeagueDto()
            {
                LeagueId = id,
                Name = "Seed League",
                Password = "1234"
            };

            League.AddNew(dto);

            return id;
        }
        public void DeleteSeededLeague(Guid leagueId)
        {
            League.RemoveLeague(leagueId);
        }


        public Guid SeedTeam()
        {
            Guid id = Guid.NewGuid();
            Guid leagueId = SeedLeague();

            TeamDto dto = new TeamDto()
            {
                TeamId = id,
                LeagueId = leagueId,
                Handicap = 3,
                Name = "Bombers",
                Password = "KaBoom",
                ShouldSync = true
            };

            Team.AddNew(dto);

            return id;
        }
        public void DeleteSeededTeam(Guid teamId)
        {
            var team = Team.GetTeam(teamId);
            Guid leagueId = team.LeagueId;

            Team.Remove(teamId);
            League.RemoveLeague(leagueId);
        }

        
        public Guid SeedGame()
        {
            Guid gameId = Guid.NewGuid();
            Guid leagueId = SeedLeague();

            GameDto dto = new GameDto()
            {
                GameId = gameId,
                LeagueId = leagueId
            };

            Game.AddNew(dto);

            return gameId;
        }
        public void DeleteSeededGame(Guid gameId)
        {
            var game = Game.GetGame(gameId);
            Guid leagueId = game.LeagueId;

            Game.Remove(gameId);
            League.RemoveLeague(leagueId);
        }


        public Guid SeedPlayer()
        {
            Guid playerId = Guid.NewGuid();

            PlayerDto dto = new PlayerDto()
            {
                PlayerId = playerId,
                EmailAddress = "test@gamil.com",
                Name = "Test Player",
                Password = "password",
                UserName = "TestPlayer",
                ShouldSync = false
            };

            Player.AddNew(dto);

            return playerId;
        }
        public void DeleteSeededPlayer(Guid playerId)
        {
            Player.Remove(playerId);
        }


        public Guid SeedGameInning()
        {
            Guid gameInningId = Guid.NewGuid();
            Guid seedGameId = SeedGame();

            GameInningDto dto = new GameInningDto()
            {
                GameInningId = gameInningId,
                GameId = seedGameId,
                InningNumber = 1
            };

            GameInning.AddNew(dto);

            return gameInningId;
        }
        public void DeleteSeededGameInning(Guid gameInningId)
        {
            var gameInning = GameInning.GetGameInning(gameInningId);
            GameInning.Remove(gameInning.GameId, gameInning.InningNumber);
            DeleteSeededGame(gameInning.GameId);
        }


        public Guid SeedGameTeam()
        {
            Guid gameTeamId = Guid.NewGuid();
            Guid seedGameId = SeedGame();
            Guid seedTeamId = SeedTeam();

            GameTeamDto dto = new GameTeamDto()
            {
                GameTeamId = gameTeamId,
                GameId = seedGameId,
                TeamId = seedTeamId
            };

            GameTeam.AddNew(dto);

            return gameTeamId;
        }
        public void DeleteSeededGameTeam(Guid gameTeamId)
        {
            var gameTeam = GameTeam.GetGameTeam(gameTeamId);
            GameTeam.Remove(gameTeam.GameId, gameTeam.TeamId);
            DeleteSeededGame(gameTeam.GameId);
            DeleteSeededTeam(gameTeam.TeamId);
        }


        public Guid SeedGameInningTeam()
        {
            Guid gameInningTeamId = Guid.NewGuid();
            Guid seedGameInningId = SeedGameInning();
            Guid seedGameTeamId = SeedGameTeam();

            GameInningTeamDto dto = new GameInningTeamDto()
            {
                GameInningTeamId = gameInningTeamId,
                GameInningId = seedGameInningId,
                GameTeamId = seedGameTeamId,
                Outs = 0,
                Score = 0
            };

            GameInningTeam.AddNew(dto);

            return gameInningTeamId;
        }
        public void DeleteSeededGameInningTeam(Guid gameInningTeamId)
        {
            var git = GameInningTeam.GetGameInningTeam(gameInningTeamId);
            GameInningTeam.Remove(git.GameInningId, git.GameTeamId);
            DeleteSeededGameInning(git.GameInningId);
            DeleteSeededGameTeam(git.GameTeamId);
        }

    }
}

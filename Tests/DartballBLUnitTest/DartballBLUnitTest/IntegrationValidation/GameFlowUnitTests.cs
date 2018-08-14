using System;
using System.Collections.Generic;
using Dartball.BusinessLayer.Game.Dto;
using Dartball.BusinessLayer.Game.Implementation;
using Dartball.BusinessLayer.Game.Interface;
using Dartball.BusinessLayer.Team.Implementation;
using Dartball.BusinessLayer.Team.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Dartball.BusinessLayer.Game.Implementation.GameEventService;

namespace DartballBLUnitTest.IntegrationValidation
{
    [TestClass]
    public class GameFlowUnitTests : IntegrationBase
    {
        IGameInningService GameInning;
        IGameInningTeamService GameInningTeam;
        IGameTeamService GameTeam;
        ITeamService Team;
        IGameInningTeamBatterService GameInningTeamBatter;

        public GameFlowUnitTests()
        {
            GameInning = new GameInningService();
            GameInningTeam = new GameInningTeamService();
            GameTeam = new GameTeamService();
            Team = new TeamService();
            GameInningTeamBatter = new GameInningTeamBatterService();
        }

        [TestMethod]
        public void GameNextInningTest()
        {

            Guid gameInningId = SeedGameInning();

            var gameInning = GameInning.GetGameInning(gameInningId);
            Assert.IsNotNull(gameInning);

            int nextInning = GameInning.GetNextGameInningNumber(gameInning.GameId);
            Assert.AreEqual(nextInning, 2);

            DeleteSeededGameInning(gameInningId);
        }

        [TestMethod]
        public void GameNextInningTeamTest()
        {
            Guid teamOneId = SeedTeam();
            Guid teamTwoId = SeedTeamTwo();

            var teamOne = Team.GetTeam(teamOneId);
            var teamTwo = Team.GetTeam(teamTwoId);
            Assert.IsNotNull(teamOne);
            Assert.IsNotNull(teamTwo);

            Guid gameId = SeedGame();

            Guid gtOneId = SeedGameTeam(gameId, teamOne.TeamId, battingSequence: 0);
            Guid gtTwoId = SeedGameTeam(gameId, teamTwo.TeamId, battingSequence: 1);

            GameInningDto gameInningDto = new GameInningDto()
            {
                InningNumber = 1,
                GameId = gameId,
                GameInningId = Guid.NewGuid()
            };
            GameInning.AddNew(gameInningDto);

            Guid? nextTeamId = GameInningTeam.GetNextAtBatTeamId(gameId);
            Assert.AreEqual(nextTeamId, teamOne.TeamId);

            GameInningTeamDto dto = new GameInningTeamDto()
            {
                GameInningTeamId = Guid.NewGuid(),
                GameTeamId = gtOneId,
                GameInningId = gameInningDto.GameInningId,
                Outs = 0,
                Score = 0
            };
            GameInningTeam.AddNew(dto);

            nextTeamId = GameInningTeam.GetNextAtBatTeamId(gameId);
            Assert.AreEqual(nextTeamId, teamTwo.TeamId);

            gameInningDto.InningNumber = 2;
            gameInningDto.GameInningId = Guid.NewGuid();
            GameInning.AddNew(gameInningDto);

            nextTeamId = GameInningTeam.GetNextAtBatTeamId(gameId);
            Assert.AreEqual(teamOne.TeamId, nextTeamId);

            DeleteSeededGame(gameId);
            DeleteSeededTeam(teamOneId);
            DeleteSeededTeam(teamTwoId);
        }


        [TestMethod]
        public void NextGameInningTeamBatterTest()
        {
            //seed teams
            Guid teamOneId = SeedTeam();
            Guid teamTwoId = SeedTeamTwo();

            //seed players
            Guid playerOneId = SeedPlayer();
            Guid playerTwoId = SeedPlayerTwo();
            Guid playerThreeId = SeedPlayerThree();
            Guid playerFourId = SeedPlayerFour();

            //seed team lineups
            List<Guid> teamOneLineup = new List<Guid> {
                playerOneId, playerTwoId, playerThreeId, playerFourId
            };
            SeedTeamPlayerLineup(teamOneId, teamOneLineup);

            List<Guid> teamTwoLineup = new List<Guid> {
                playerFourId, playerThreeId, playerTwoId, playerOneId
            };
            SeedTeamPlayerLineup(teamTwoId, teamTwoLineup);

            //seed game 
            Guid gameId = SeedGame();
            Guid gameTeamOneId = SeedGameTeam(gameId, teamOneId, battingSequence: 0);
            Guid gameTeamTwoId = SeedGameTeam(gameId, teamTwoId, battingSequence: 1);

            GameInningDto gameInningDto = new GameInningDto()
            {
                InningNumber = 1,
                GameId = gameId,
                GameInningId = Guid.NewGuid()
            };
            GameInning.AddNew(gameInningDto);

            GameInningTeamDto gameInningTeamDto = new GameInningTeamDto()
            {
                GameTeamId = gameTeamOneId,
                GameInningId = gameInningDto.GameInningId,
                GameInningTeamId = Guid.NewGuid()
            };
            GameInningTeam.AddNew(gameInningTeamDto);

            //see if the first player in the linup is selected
            Guid? nextBatterId = GameInningTeamBatter.GetNextGameBatterPlayerId(gameId, teamOneId);
            Assert.AreEqual(nextBatterId, playerOneId);

            GameInningTeamBatterDto gameInningTeamBatterDto = new GameInningTeamBatterDto()
            {
                GameInningTeamId = gameInningTeamDto.GameInningTeamId,
                PlayerId = playerOneId,
                EventType = (int)EventType.Out,
                GameInningTeamBatterId = Guid.NewGuid(),
                Sequence = 0
            };
            GameInningTeamBatter.AddNew(gameInningTeamBatterDto);

            //see if the second player in the lineup is selected
            nextBatterId = GameInningTeamBatter.GetNextGameBatterPlayerId(gameId, teamOneId);
            Assert.AreEqual(nextBatterId, playerTwoId);

            GameInningTeamBatterDto gameInningTeamBatter2Dto = new GameInningTeamBatterDto()
            {
                GameInningTeamId = gameInningTeamDto.GameInningTeamId,
                PlayerId = playerTwoId,
                EventType = (int)EventType.Single,
                GameInningTeamBatterId = Guid.NewGuid(),
                Sequence = 1
            };
            GameInningTeamBatter.AddNew(gameInningTeamBatter2Dto);

            //see if the thrid batter is selected
            nextBatterId = GameInningTeamBatter.GetNextGameBatterPlayerId(gameId, teamOneId);
            Assert.AreEqual(nextBatterId, playerThreeId);

            GameInningTeamBatterDto gameInningTeamBatter3Dto = new GameInningTeamBatterDto()
            {
                GameInningTeamId = gameInningTeamDto.GameInningTeamId,
                PlayerId = playerThreeId,
                EventType = (int)EventType.Single,
                GameInningTeamBatterId = Guid.NewGuid(),
                Sequence = 2
            };
            GameInningTeamBatter.AddNew(gameInningTeamBatter3Dto);

            //see if fourth batter is selected
            nextBatterId = GameInningTeamBatter.GetNextGameBatterPlayerId(gameId, teamOneId);
            Assert.AreEqual(nextBatterId, playerFourId);

            GameInningTeamBatterDto gameInningTeamBatter4Dto = new GameInningTeamBatterDto()
            {
                GameInningTeamId = gameInningTeamDto.GameInningTeamId,
                PlayerId = playerFourId,
                EventType = (int)EventType.Single,
                GameInningTeamBatterId = Guid.NewGuid(),
                Sequence = 3
            };
            GameInningTeamBatter.AddNew(gameInningTeamBatter4Dto);

            //see if first batter is up again
            nextBatterId = GameInningTeamBatter.GetNextGameBatterPlayerId(gameId, teamOneId);
            Assert.AreEqual(nextBatterId, playerOneId);

            //cleanup test data
            DeleteSeededTeam(teamOneId);
            DeleteSeededTeam(teamTwoId);
            DeleteSeededTeamPlayerLineup(teamOneId);
            DeleteSeededTeamPlayerLineup(teamTwoId);
            DeleteSeededGame(gameId);
            DeleteSeededPlayer(playerOneId);
            DeleteSeededPlayer(playerTwoId);
            DeleteSeededPlayer(playerThreeId);
            DeleteSeededPlayer(playerFourId);

        }


        [TestMethod]
        public void GameInningTeamBatterCurrentBatterTest() {
            //seed teams
            Guid teamOneId = SeedTeam();
            Guid teamTwoId = SeedTeamTwo();

            //seed players
            Guid playerOneId = SeedPlayer();
            Guid playerTwoId = SeedPlayerTwo();
            Guid playerThreeId = SeedPlayerThree();
            Guid playerFourId = SeedPlayerFour();

            //seed game 
            Guid gameId = SeedGame();
            Guid gameTeamOneId = SeedGameTeam(gameId, teamOneId, battingSequence: 0);
            Guid gameTeamTwoId = SeedGameTeam(gameId, teamTwoId, battingSequence: 1);

            GameInningDto gameInningDto = new GameInningDto()
            {
                InningNumber = 1,
                GameId = gameId,
                GameInningId = Guid.NewGuid()
            };
            GameInning.AddNew(gameInningDto);

            GameInningTeamDto gameInningTeamDto = new GameInningTeamDto()
            {
                GameTeamId = gameTeamOneId,
                GameInningId = gameInningDto.GameInningId,
                GameInningTeamId = Guid.NewGuid()
            };
            GameInningTeam.AddNew(gameInningTeamDto);


            GameInningTeamBatterDto gameInningTeamBatterDto = new GameInningTeamBatterDto()
            {
                GameInningTeamId = gameInningTeamDto.GameInningTeamId,
                PlayerId = playerOneId,
                EventType = (int)EventType.Out,
                GameInningTeamBatterId = Guid.NewGuid(),
                Sequence = 0
            };
            GameInningTeamBatter.AddNew(gameInningTeamBatterDto);

            var currentBatter = GameInningTeamBatter.GetCurrentGameInningTeamBatter(gameId);
            Assert.IsNotNull(currentBatter);
            Assert.AreEqual(playerOneId, currentBatter.PlayerId);

            GameInningTeamBatterDto gameInningTeamBatter2Dto = new GameInningTeamBatterDto()
            {
                GameInningTeamId = gameInningTeamDto.GameInningTeamId,
                PlayerId = playerTwoId,
                EventType = (int)EventType.Single,
                GameInningTeamBatterId = Guid.NewGuid(),
                Sequence = 1
            };
            GameInningTeamBatter.AddNew(gameInningTeamBatter2Dto);

            currentBatter = GameInningTeamBatter.GetCurrentGameInningTeamBatter(gameId);
            Assert.IsNotNull(currentBatter);
            Assert.AreEqual(currentBatter.PlayerId, playerTwoId);

            //cleanup test data
            DeleteSeededTeam(teamOneId);
            DeleteSeededTeam(teamTwoId);
            DeleteSeededGame(gameId);
            DeleteSeededPlayer(playerOneId);
            DeleteSeededPlayer(playerTwoId);
            DeleteSeededPlayer(playerThreeId);
            DeleteSeededPlayer(playerFourId);

        }

    }
}

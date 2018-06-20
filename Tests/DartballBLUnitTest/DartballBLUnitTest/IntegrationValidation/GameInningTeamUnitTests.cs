using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dartball.BusinessLayer.Game.Dto;
using Dartball.BusinessLayer.Game.Implementation;
using Dartball.BusinessLayer.Game.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DartballBLUnitTest.IntegrationValidation
{
    [TestClass]
    public class GameInningTeamUnitTests
    {
        private IGameInningTeamService GameInningTeam;

        public GameInningTeamUnitTests()
        {
            GameInningTeam = new GameInningTeamService();

            TEST_GAME_INNING_ALTERNATE_KEY = Guid.NewGuid();
            TEST_GAME_TEAM_ALTERNATE_KEY = Guid.NewGuid();
        }

        private Guid TEST_GAME_INNING_ALTERNATE_KEY;
        private Guid TEST_GAME_TEAM_ALTERNATE_KEY;
        private const int TEST_SCORE = 3;
        private const int TEST_SCORE_2 = 4;
        private const int TEST_OUTS = 1;
        private const int TEST_OUTS_2 = 2;
        private const bool TEST_IS_RUNNER_ON_FIRST = true;
        private const bool TEST_IS_RUNNER_ON_FIRST_2 = false;
        private const bool TEST_IS_RUNNER_ON_SECOND = false;
        private const bool TEST_IS_RUNNER_ON_SECOND_2 = true;
        private const bool TEST_IS_RUNNER_ON_THIRD = false;
        private const bool TEST_IS_RUNNER_ON_THIRD_2 = true;


        [TestMethod]
        public void AddUpdateGameInningTeamTest()
        {
            GameInningTeamDto dto = new GameInningTeamDto()
            {
                GameInningId = TEST_GAME_INNING_ALTERNATE_KEY,
                GameTeamId = TEST_GAME_TEAM_ALTERNATE_KEY,
                Score = TEST_SCORE,
                Outs = TEST_OUTS,
                IsRunnerOnFirst = TEST_IS_RUNNER_ON_FIRST,
                IsRunnerOnSecond = TEST_IS_RUNNER_ON_SECOND,
                IsRunnerOnThird = TEST_IS_RUNNER_ON_THIRD
            };

            var addResult = GameInningTeam.AddNew(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var item = GameInningTeam.GetGameInningTeam(TEST_GAME_TEAM_ALTERNATE_KEY, TEST_GAME_INNING_ALTERNATE_KEY);
            Assert.IsNotNull(item);
            Assert.AreEqual(TEST_GAME_INNING_ALTERNATE_KEY, item.GameInningId);
            Assert.AreEqual(TEST_GAME_TEAM_ALTERNATE_KEY, item.GameTeamId);
            Assert.AreEqual(TEST_SCORE, item.Score);
            Assert.AreEqual(TEST_OUTS, item.Outs);
            Assert.AreEqual(TEST_IS_RUNNER_ON_FIRST, item.IsRunnerOnFirst);
            Assert.AreEqual(TEST_IS_RUNNER_ON_SECOND, item.IsRunnerOnSecond);
            Assert.AreEqual(TEST_IS_RUNNER_ON_THIRD, item.IsRunnerOnThird);

            dto.GameInningTeamId = item.GameInningTeamId;
            dto.Score = TEST_SCORE_2;
            dto.Outs = TEST_OUTS_2;
            dto.IsRunnerOnFirst = TEST_IS_RUNNER_ON_FIRST_2;
            dto.IsRunnerOnSecond = TEST_IS_RUNNER_ON_SECOND_2;
            dto.IsRunnerOnThird = TEST_IS_RUNNER_ON_THIRD_2;

            var updateResult = GameInningTeam.Update(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            var inningTeams = GameInningTeam.GetInningTeams(TEST_GAME_INNING_ALTERNATE_KEY);
            Assert.IsTrue(inningTeams.Count >= 1);

            var teamInnings = GameInningTeam.GetTeamInnings(TEST_GAME_TEAM_ALTERNATE_KEY);
            Assert.IsTrue(teamInnings.Count >= 1);

            item = inningTeams.FirstOrDefault(x => x.GameTeamId == TEST_GAME_TEAM_ALTERNATE_KEY);
            Assert.IsNotNull(item);
            Assert.AreEqual(TEST_GAME_INNING_ALTERNATE_KEY, item.GameInningId);
            Assert.AreEqual(TEST_GAME_TEAM_ALTERNATE_KEY, item.GameTeamId);
            Assert.AreEqual(TEST_SCORE_2, item.Score);
            Assert.AreEqual(TEST_OUTS_2, item.Outs);
            Assert.AreEqual(TEST_IS_RUNNER_ON_FIRST_2, item.IsRunnerOnFirst);
            Assert.AreEqual(TEST_IS_RUNNER_ON_SECOND_2, item.IsRunnerOnSecond);
            Assert.AreEqual(TEST_IS_RUNNER_ON_THIRD_2, item.IsRunnerOnThird);

            var removeResult = GameInningTeam.Remove(TEST_GAME_INNING_ALTERNATE_KEY, TEST_GAME_TEAM_ALTERNATE_KEY);
            Assert.IsTrue(removeResult.IsSuccess);

            item = GameInningTeam.GetGameInningTeam(TEST_GAME_TEAM_ALTERNATE_KEY, TEST_GAME_INNING_ALTERNATE_KEY);
            Assert.IsNull(item);
        }

        [TestMethod]
        public void InvalidGameInningIdTest()
        {
            GameInningTeamDto dto = new GameInningTeamDto()
            {
                GameTeamId = TEST_GAME_TEAM_ALTERNATE_KEY,
                Score = TEST_SCORE,
                Outs = TEST_OUTS,
                IsRunnerOnFirst = TEST_IS_RUNNER_ON_FIRST,
                IsRunnerOnSecond = TEST_IS_RUNNER_ON_SECOND,
                IsRunnerOnThird = TEST_IS_RUNNER_ON_THIRD
            };
            var result = GameInningTeam.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidGameTeamIdTest()
        {
            GameInningTeamDto dto = new GameInningTeamDto()
            {
                GameInningId = TEST_GAME_INNING_ALTERNATE_KEY,
                Score = TEST_SCORE,
                Outs = TEST_OUTS,
                IsRunnerOnFirst = TEST_IS_RUNNER_ON_FIRST,
                IsRunnerOnSecond = TEST_IS_RUNNER_ON_SECOND,
                IsRunnerOnThird = TEST_IS_RUNNER_ON_THIRD
            };
            var result = GameInningTeam.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidGameInningTeamIdTest()
        {
            GameInningTeamDto dto = new GameInningTeamDto()
            {
                GameInningId = TEST_GAME_INNING_ALTERNATE_KEY,
                GameTeamId = TEST_GAME_TEAM_ALTERNATE_KEY,
                Score = TEST_SCORE,
                Outs = TEST_OUTS,
                IsRunnerOnFirst = TEST_IS_RUNNER_ON_FIRST,
                IsRunnerOnSecond = TEST_IS_RUNNER_ON_SECOND,
                IsRunnerOnThird = TEST_IS_RUNNER_ON_THIRD
            };
            var result = GameInningTeam.Update(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void TooManyOutsTest()
        {
            GameInningTeamDto dto = new GameInningTeamDto()
            {
                GameInningId = TEST_GAME_INNING_ALTERNATE_KEY,
                GameTeamId = TEST_GAME_TEAM_ALTERNATE_KEY,
                Score = TEST_SCORE,
                Outs = 4,
                IsRunnerOnFirst = TEST_IS_RUNNER_ON_FIRST,
                IsRunnerOnSecond = TEST_IS_RUNNER_ON_SECOND,
                IsRunnerOnThird = TEST_IS_RUNNER_ON_THIRD
            };
            var result = GameInningTeam.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void TooFewOutsTest()
        {
            GameInningTeamDto dto = new GameInningTeamDto()
            {
                GameInningId = TEST_GAME_INNING_ALTERNATE_KEY,
                GameTeamId = TEST_GAME_TEAM_ALTERNATE_KEY,
                Score = TEST_SCORE,
                Outs = -1,
                IsRunnerOnFirst = TEST_IS_RUNNER_ON_FIRST,
                IsRunnerOnSecond = TEST_IS_RUNNER_ON_SECOND,
                IsRunnerOnThird = TEST_IS_RUNNER_ON_THIRD
            };
            var result = GameInningTeam.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidScoreTest()
        {
            GameInningTeamDto dto = new GameInningTeamDto()
            {
                GameInningId = TEST_GAME_INNING_ALTERNATE_KEY,
                GameTeamId = TEST_GAME_TEAM_ALTERNATE_KEY,
                Score = -2,
                Outs = TEST_OUTS,
                IsRunnerOnFirst = TEST_IS_RUNNER_ON_FIRST,
                IsRunnerOnSecond = TEST_IS_RUNNER_ON_SECOND,
                IsRunnerOnThird = TEST_IS_RUNNER_ON_THIRD
            };
            var result = GameInningTeam.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

    }
}

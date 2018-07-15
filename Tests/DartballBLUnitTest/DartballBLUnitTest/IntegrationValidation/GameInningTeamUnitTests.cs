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
    public class GameInningTeamUnitTests : IntegrationBase
    {
        private IGameInningTeamService GameInningTeam;

        public GameInningTeamUnitTests()
        {
            GameInningTeam = new GameInningTeamService();

            Dartball.Data.DartballContext dartballContext = new Dartball.Data.DartballContext();
            dartballContext.Migrate();
        }

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
            Guid seedGameInningId = SeedGameInning();
            Guid seedGameTeamId = SeedGameTeam();

            GameInningTeamDto dto = new GameInningTeamDto()
            {
                GameInningId = seedGameInningId,
                GameTeamId = seedGameTeamId,
                Score = TEST_SCORE,
                Outs = TEST_OUTS,
                IsRunnerOnFirst = TEST_IS_RUNNER_ON_FIRST,
                IsRunnerOnSecond = TEST_IS_RUNNER_ON_SECOND,
                IsRunnerOnThird = TEST_IS_RUNNER_ON_THIRD
            };

            var addResult = GameInningTeam.AddNew(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var item = GameInningTeam.GetGameInningTeam(seedGameTeamId, seedGameInningId);
            Assert.IsNotNull(item);
            Assert.AreEqual(seedGameInningId, item.GameInningId);
            Assert.AreEqual(seedGameTeamId, item.GameTeamId);
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

            var inningTeams = GameInningTeam.GetInningTeams(seedGameInningId);
            Assert.IsTrue(inningTeams.Count >= 1);

            var teamInnings = GameInningTeam.GetTeamInnings(seedGameTeamId);
            Assert.IsTrue(teamInnings.Count >= 1);

            item = inningTeams.FirstOrDefault(x => x.GameTeamId == seedGameTeamId);
            Assert.IsNotNull(item);
            Assert.AreEqual(seedGameInningId, item.GameInningId);
            Assert.AreEqual(seedGameTeamId, item.GameTeamId);
            Assert.AreEqual(TEST_SCORE_2, item.Score);
            Assert.AreEqual(TEST_OUTS_2, item.Outs);
            Assert.AreEqual(TEST_IS_RUNNER_ON_FIRST_2, item.IsRunnerOnFirst);
            Assert.AreEqual(TEST_IS_RUNNER_ON_SECOND_2, item.IsRunnerOnSecond);
            Assert.AreEqual(TEST_IS_RUNNER_ON_THIRD_2, item.IsRunnerOnThird);

            var removeResult = GameInningTeam.Remove(seedGameInningId, seedGameTeamId);
            Assert.IsTrue(removeResult.IsSuccess);

            item = GameInningTeam.GetGameInningTeam(seedGameTeamId, seedGameInningId);
            Assert.IsNull(item);

            DeleteSeededGameInning(seedGameInningId);
            DeleteSeededGameTeam(seedGameTeamId);
        }

        [TestMethod]
        public void InvalidGameInningIdTest()
        {
            GameInningTeamDto dto = new GameInningTeamDto()
            {
                GameTeamId = Guid.NewGuid(),
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
                GameInningId = Guid.NewGuid(),
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
                GameInningId = Guid.NewGuid(),
                GameTeamId = Guid.NewGuid(),
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
                GameInningId = Guid.NewGuid(),
                GameTeamId = Guid.NewGuid(),
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
                GameInningId = Guid.NewGuid(),
                GameTeamId = Guid.NewGuid(),
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
                GameInningId = Guid.NewGuid(),
                GameTeamId = Guid.NewGuid(),
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

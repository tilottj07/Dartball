﻿using System;
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
    public class GameTeamUnitTests
    {
        private IGameTeamService GameTeam;

        public GameTeamUnitTests()
        {
            GameTeam = new GameTeamService();

            TEST_GAME_ALTERNATE_KEY = Guid.NewGuid();
            TEST_TEAM_ALTERNATE_KEY = Guid.NewGuid();
        }

        private Guid TEST_GAME_ALTERNATE_KEY;
        private Guid TEST_TEAM_ALTERNATE_KEY;


        [TestMethod]
        public void AddUpdateGameTeamTest()
        {
            GameTeamDto dto = new GameTeamDto()
            {
                GameId = TEST_GAME_ALTERNATE_KEY,
                TeamId = TEST_TEAM_ALTERNATE_KEY
            };

            var addResult = GameTeam.AddNew(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var item = GameTeam.GetGameTeam(TEST_GAME_ALTERNATE_KEY, TEST_TEAM_ALTERNATE_KEY);
            Assert.IsNotNull(item);
            Assert.AreEqual(TEST_TEAM_ALTERNATE_KEY, item.TeamId);
            Assert.AreEqual(TEST_GAME_ALTERNATE_KEY, item.GameId);

            dto.GameTeamId = item.GameTeamId;
            dto.DeleteDate = DateTime.UtcNow;

            var updateResult = GameTeam.Update(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            item = GameTeam.GetGameTeam(TEST_GAME_ALTERNATE_KEY, TEST_TEAM_ALTERNATE_KEY);
            Assert.IsNotNull(item);
            Assert.IsNotNull(item.DeleteDate);

            dto.DeleteDate = null;
            updateResult = GameTeam.Update(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            var items = GameTeam.GetGameTeams(TEST_GAME_ALTERNATE_KEY);
            Assert.IsTrue(items.Count >= 1);

            item = items.FirstOrDefault(x => x.TeamId == TEST_TEAM_ALTERNATE_KEY);
            Assert.IsNotNull(item);

            var removeResult = GameTeam.Remove(TEST_GAME_ALTERNATE_KEY, TEST_TEAM_ALTERNATE_KEY);
            Assert.IsTrue(removeResult.IsSuccess);

            item = GameTeam.GetGameTeam(TEST_GAME_ALTERNATE_KEY, TEST_TEAM_ALTERNATE_KEY);
            Assert.IsNull(item);
        }

        [TestMethod]
        public void InvalidGameIdTest()
        {
            GameTeamDto dto = new GameTeamDto()
            {
                TeamId = TEST_TEAM_ALTERNATE_KEY
            };

            var result = GameTeam.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidTeamIdTest()
        {
            GameTeamDto dto = new GameTeamDto()
            {
                GameId = TEST_GAME_ALTERNATE_KEY,
            };

            var result = GameTeam.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidGameTeamIdTest()
        {
            GameTeamDto dto = new GameTeamDto()
            {
                GameId = TEST_GAME_ALTERNATE_KEY,
                TeamId = TEST_TEAM_ALTERNATE_KEY
            };

            var result = GameTeam.Update(dto);
            Assert.IsFalse(result.IsSuccess);
        }
    }
}

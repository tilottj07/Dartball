using System;
using System.Collections.Generic;
using System.Text;
using Dartball.BusinessLayer.Player.Dto;
using Dartball.BusinessLayer.Player.Implementation;
using Dartball.BusinessLayer.Player.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DartballBLUnitTest.IntegrationValidation
{
    [TestClass]
    public class PlayerTeamUnitTests
    {
        private IPlayerTeamService PlayerTeamService;

        public PlayerTeamUnitTests()
        {
            PlayerTeamService = new PlayerTeamService();

            TEST_TEAM_ID = Guid.NewGuid();
            TEST_PLAYER_ID = Guid.NewGuid();
        }

        private Guid TEST_TEAM_ID;
        private Guid TEST_PLAYER_ID;


        [TestMethod]
        public void AddUpdatePlayerTeamTest()
        {
            PlayerTeamDto dto = new PlayerTeamDto()
            {
                PlayerId = TEST_PLAYER_ID,
                TeamId = TEST_TEAM_ID
            };

            var addResult = PlayerTeamService.AddNew(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var playerTeam = PlayerTeamService.GetPlayerTeam(TEST_TEAM_ID, TEST_PLAYER_ID);
            Assert.IsNotNull(playerTeam);
            Assert.AreEqual(TEST_TEAM_ID, playerTeam.TeamId);
            Assert.AreEqual(TEST_PLAYER_ID, playerTeam.PlayerId);
            Assert.IsFalse(playerTeam.PlayerTeamId == Guid.Empty);

            dto.PlayerTeamId = playerTeam.PlayerTeamId;
            var updateResult = PlayerTeamService.Update(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            var teamPlayers = PlayerTeamService.GetTeamPlayers(TEST_TEAM_ID);
            Assert.IsTrue(teamPlayers.Count >= 1);

            var removeResult = PlayerTeamService.Remove(TEST_PLAYER_ID, TEST_TEAM_ID);
            Assert.IsTrue(removeResult.IsSuccess);

            playerTeam = PlayerTeamService.GetPlayerTeam(TEST_TEAM_ID, TEST_PLAYER_ID);
            Assert.IsNull(playerTeam);
        }


        [TestMethod]
        public void InvalidTeamIdTest()
        {
            PlayerTeamDto dto = new PlayerTeamDto()
            {
                PlayerId = TEST_PLAYER_ID,
                TeamId = Guid.Empty
            };

            var addResult = PlayerTeamService.AddNew(dto);
            Assert.IsFalse(addResult.IsSuccess);
        }

        [TestMethod]
        public void InvalidPlayerIdTest()
        {
            PlayerTeamDto dto = new PlayerTeamDto()
            {
                PlayerId = Guid.Empty,
                TeamId = TEST_TEAM_ID
            };

            var addResult = PlayerTeamService.AddNew(dto);
            Assert.IsFalse(addResult.IsSuccess);
        }

        [TestMethod]
        public void InvalidTeamPlayerIdTest()
        {
            PlayerTeamDto dto = new PlayerTeamDto()
            {
                PlayerId = TEST_PLAYER_ID,
                TeamId = Guid.Empty
            };

            var result = PlayerTeamService.Update(dto);
            Assert.IsFalse(result.IsSuccess);
        }

    }
}

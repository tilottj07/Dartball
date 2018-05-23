using System;
using System.Collections.Generic;
using System.Text;
using Dartball.BusinessLayer.Player.Dto;
using Dartball.BusinessLayer.Player.Implementation;
using Dartball.BusinessLayer.Player.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DartballBLUnitTest
{
    [TestClass]
    public class PlayerTeamUnitTests
    {
        private IPlayerTeamService PlayerTeamService;

        public PlayerTeamUnitTests()
        {
            PlayerTeamService = new PlayerTeamService();

            TEST_TEAM_ALTERNATE_KEY = Guid.NewGuid();
            TEST_PLAYER_ALTERNATE_KEY = Guid.NewGuid();
        }

        private Guid TEST_TEAM_ALTERNATE_KEY;
        private Guid TEST_PLAYER_ALTERNATE_KEY;


        [TestMethod]
        public void AddUpdatePlayerTeamTest()
        {
            PlayerTeamDto dto = new PlayerTeamDto()
            {
                PlayerAlternateKey = TEST_PLAYER_ALTERNATE_KEY,
                TeamAlternateKey = TEST_TEAM_ALTERNATE_KEY
            };

            var addResult = PlayerTeamService.AddNew(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var playerTeam = PlayerTeamService.GetPlayerTeam(TEST_TEAM_ALTERNATE_KEY, TEST_PLAYER_ALTERNATE_KEY);
            Assert.IsNotNull(playerTeam);
            Assert.AreEqual(TEST_TEAM_ALTERNATE_KEY, playerTeam.TeamAlternateKey);
            Assert.AreEqual(TEST_PLAYER_ALTERNATE_KEY, playerTeam.PlayerAlternateKey);
            Assert.IsFalse(playerTeam.PlayerTeamAlternateKey == Guid.Empty);

            dto.PlayerTeamAlternateKey = playerTeam.PlayerTeamAlternateKey;
            var updateResult = PlayerTeamService.Update(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            var teamPlayers = PlayerTeamService.GetTeamPlayers(TEST_TEAM_ALTERNATE_KEY);
            Assert.IsTrue(teamPlayers.Count >= 1);

            var removeResult = PlayerTeamService.Remove(TEST_PLAYER_ALTERNATE_KEY, TEST_TEAM_ALTERNATE_KEY);
            Assert.IsTrue(removeResult.IsSuccess);

            playerTeam = PlayerTeamService.GetPlayerTeam(TEST_TEAM_ALTERNATE_KEY, TEST_PLAYER_ALTERNATE_KEY);
            Assert.IsNull(playerTeam);
        }


        [TestMethod]
        public void InvalidTeamAlternateKeyTest()
        {
            PlayerTeamDto dto = new PlayerTeamDto()
            {
                PlayerAlternateKey = TEST_PLAYER_ALTERNATE_KEY,
                TeamAlternateKey = Guid.Empty
            };

            var addResult = PlayerTeamService.AddNew(dto);
            Assert.IsFalse(addResult.IsSuccess);
        }

        [TestMethod]
        public void InvalidPlayerAlternateKeyTest()
        {
            PlayerTeamDto dto = new PlayerTeamDto()
            {
                PlayerAlternateKey = Guid.Empty,
                TeamAlternateKey = TEST_TEAM_ALTERNATE_KEY
            };

            var addResult = PlayerTeamService.AddNew(dto);
            Assert.IsFalse(addResult.IsSuccess);
        }

    }
}

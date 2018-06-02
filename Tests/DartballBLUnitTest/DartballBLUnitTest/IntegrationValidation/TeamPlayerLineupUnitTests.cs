using System;
using System.Collections.Generic;
using System.Text;
using Dartball.BusinessLayer.Team.Dto;
using Dartball.BusinessLayer.Team.Implementation;
using Dartball.BusinessLayer.Team.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DartballBLUnitTest.IntegrationValidation
{
    [TestClass]
    public class TeamPlayerLineupUnitTests
    {
        private ITeamPlayerLineupService Lineup;

        public TeamPlayerLineupUnitTests()
        {
            Lineup = new TeamPlayerLineupService();

            TEST_TEAM_ALTERNATE_KEY = Guid.NewGuid();
            TEST_PLAYER_ALTERNATE_KEY = Guid.NewGuid();
        }

        private Guid TEST_TEAM_ALTERNATE_KEY;
        private Guid TEST_PLAYER_ALTERNATE_KEY;
        private const int TEST_BATTING_ORDER = 3;
        private const int TEST_BATTING_ORDER_2 = 4;


        [TestMethod]
        public void AddUpdateTeamPlayerLineupTest()
        {
            TeamPlayerLineupDto dto = new TeamPlayerLineupDto()
            {
                TeamAlternateKey = TEST_TEAM_ALTERNATE_KEY,
                PlayerAlternateKey = TEST_PLAYER_ALTERNATE_KEY,
                BattingOrder = TEST_BATTING_ORDER
            };

            var addResult = Lineup.AddNew(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var teamPlayerLineupItem = Lineup.GetTeamPlayerLineupItem(TEST_TEAM_ALTERNATE_KEY, TEST_PLAYER_ALTERNATE_KEY);
            Assert.IsNotNull(teamPlayerLineupItem);
            Assert.IsFalse(teamPlayerLineupItem.TeamPlayerLineupAlternateKey == Guid.Empty);
            Assert.AreEqual(teamPlayerLineupItem.TeamAlternateKey, TEST_TEAM_ALTERNATE_KEY);
            Assert.AreEqual(teamPlayerLineupItem.PlayerAlternateKey, TEST_PLAYER_ALTERNATE_KEY);

            dto.TeamPlayerLineupAlternateKey = teamPlayerLineupItem.TeamPlayerLineupAlternateKey;
            dto.BattingOrder = TEST_BATTING_ORDER_2;

            var updateResult = Lineup.Update(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            teamPlayerLineupItem = Lineup.GetTeamPlayerLineupItem(TEST_TEAM_ALTERNATE_KEY, TEST_PLAYER_ALTERNATE_KEY);
            Assert.IsNotNull(teamPlayerLineupItem);
            Assert.AreEqual(TEST_BATTING_ORDER_2, teamPlayerLineupItem.BattingOrder);

            var teamLineup = Lineup.GetTeamLineup(TEST_TEAM_ALTERNATE_KEY);
            Assert.IsTrue(teamLineup.Count >= 1);

            var removeResult = Lineup.Remove(TEST_TEAM_ALTERNATE_KEY, TEST_PLAYER_ALTERNATE_KEY);
            Assert.IsTrue(removeResult.IsSuccess);

            teamPlayerLineupItem = Lineup.GetTeamPlayerLineupItem(TEST_TEAM_ALTERNATE_KEY, TEST_PLAYER_ALTERNATE_KEY);
            Assert.IsNull(teamPlayerLineupItem);
        }

        [TestMethod]
        public void InvalidTeamAlternateKeyTest()
        {
            TeamPlayerLineupDto dto = new TeamPlayerLineupDto()
            {
                PlayerAlternateKey = TEST_PLAYER_ALTERNATE_KEY,
                BattingOrder = TEST_BATTING_ORDER
            };

            var result = Lineup.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidPlayerAlternateKeyTest()
        {
            TeamPlayerLineupDto dto = new TeamPlayerLineupDto()
            {
                TeamAlternateKey = TEST_TEAM_ALTERNATE_KEY,
                BattingOrder = TEST_BATTING_ORDER
            };

            var result = Lineup.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidTeamPlayerLineupAlternateKeyTest()
        {
            TeamPlayerLineupDto dto = new TeamPlayerLineupDto()
            {
                TeamAlternateKey = TEST_TEAM_ALTERNATE_KEY,
                PlayerAlternateKey = TEST_PLAYER_ALTERNATE_KEY,
                BattingOrder = TEST_BATTING_ORDER
            };

            var result = Lineup.Update(dto);
            Assert.IsFalse(result.IsSuccess);
        }
    }
}

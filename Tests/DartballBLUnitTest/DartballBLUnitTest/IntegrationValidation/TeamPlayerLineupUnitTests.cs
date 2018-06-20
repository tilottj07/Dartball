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

            TEST_TEAM_ID = Guid.NewGuid();
            TEST_PLAYER_ID = Guid.NewGuid();
        }

        private Guid TEST_TEAM_ID;
        private Guid TEST_PLAYER_ID;
        private const int TEST_BATTING_ORDER = 3;
        private const int TEST_BATTING_ORDER_2 = 4;


        [TestMethod]
        public void AddUpdateTeamPlayerLineupTest()
        {
            TeamPlayerLineupDto dto = new TeamPlayerLineupDto()
            {
                TeamId = TEST_TEAM_ID,
                PlayerId = TEST_PLAYER_ID,
                BattingOrder = TEST_BATTING_ORDER
            };

            var addResult = Lineup.AddNew(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var teamPlayerLineupItem = Lineup.GetTeamPlayerLineupItem(TEST_TEAM_ID, TEST_PLAYER_ID);
            Assert.IsNotNull(teamPlayerLineupItem);
            Assert.IsFalse(teamPlayerLineupItem.TeamPlayerLineupId == Guid.Empty);
            Assert.AreEqual(teamPlayerLineupItem.TeamId, TEST_TEAM_ID);
            Assert.AreEqual(teamPlayerLineupItem.PlayerId, TEST_PLAYER_ID);

            dto.TeamPlayerLineupId = teamPlayerLineupItem.TeamPlayerLineupId;
            dto.BattingOrder = TEST_BATTING_ORDER_2;

            var updateResult = Lineup.Update(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            teamPlayerLineupItem = Lineup.GetTeamPlayerLineupItem(TEST_TEAM_ID, TEST_PLAYER_ID);
            Assert.IsNotNull(teamPlayerLineupItem);
            Assert.AreEqual(TEST_BATTING_ORDER_2, teamPlayerLineupItem.BattingOrder);

            var teamLineup = Lineup.GetTeamLineup(TEST_TEAM_ID);
            Assert.IsTrue(teamLineup.Count >= 1);

            var removeResult = Lineup.Remove(TEST_TEAM_ID, TEST_PLAYER_ID);
            Assert.IsTrue(removeResult.IsSuccess);

            teamPlayerLineupItem = Lineup.GetTeamPlayerLineupItem(TEST_TEAM_ID, TEST_PLAYER_ID);
            Assert.IsNull(teamPlayerLineupItem);
        }

        [TestMethod]
        public void InvalidTeamIdTest()
        {
            TeamPlayerLineupDto dto = new TeamPlayerLineupDto()
            {
                PlayerId = TEST_PLAYER_ID,
                BattingOrder = TEST_BATTING_ORDER
            };

            var result = Lineup.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidPlayerIdTest()
        {
            TeamPlayerLineupDto dto = new TeamPlayerLineupDto()
            {
                TeamId = TEST_TEAM_ID,
                BattingOrder = TEST_BATTING_ORDER
            };

            var result = Lineup.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidTeamPlayerLineupIdTest()
        {
            TeamPlayerLineupDto dto = new TeamPlayerLineupDto()
            {
                TeamId = TEST_TEAM_ID,
                PlayerId = TEST_PLAYER_ID,
                BattingOrder = TEST_BATTING_ORDER
            };

            var result = Lineup.Update(dto);
            Assert.IsFalse(result.IsSuccess);
        }
    }
}

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
    public class TeamPlayerLineupUnitTests : IntegrationBase
    {
        private ITeamPlayerLineupService Lineup;

        public TeamPlayerLineupUnitTests()
        {
            Lineup = new TeamPlayerLineupService();

            Dartball.Data.DartballContext dartballContext = new Dartball.Data.DartballContext();
            dartballContext.Migrate();
        }

        private const int TEST_BATTING_ORDER = 3;
        private const int TEST_BATTING_ORDER_2 = 4;


        [TestMethod]
        public void AddUpdateTeamPlayerLineupTest()
        {
            Guid seedTeamId = SeedTeam();
            Guid seedPlayerId = SeedPlayer();

            TeamPlayerLineupDto dto = new TeamPlayerLineupDto()
            {
                TeamId = seedTeamId,
                PlayerId = seedPlayerId,
                BattingOrder = TEST_BATTING_ORDER
            };

            var addResult = Lineup.AddNew(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var teamPlayerLineupItem = Lineup.GetTeamPlayerLineupItem(seedTeamId, seedPlayerId);
            Assert.IsNotNull(teamPlayerLineupItem);
            Assert.IsFalse(teamPlayerLineupItem.TeamPlayerLineupId == Guid.Empty);
            Assert.AreEqual(teamPlayerLineupItem.TeamId, seedTeamId);
            Assert.AreEqual(teamPlayerLineupItem.PlayerId, seedPlayerId);

            dto.TeamPlayerLineupId = teamPlayerLineupItem.TeamPlayerLineupId;
            dto.BattingOrder = TEST_BATTING_ORDER_2;

            var updateResult = Lineup.Update(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            teamPlayerLineupItem = Lineup.GetTeamPlayerLineupItem(seedTeamId, seedPlayerId);
            Assert.IsNotNull(teamPlayerLineupItem);
            Assert.AreEqual(TEST_BATTING_ORDER_2, teamPlayerLineupItem.BattingOrder);

            var teamLineup = Lineup.GetTeamLineup(seedTeamId);
            Assert.IsTrue(teamLineup.Count >= 1);

            var removeResult = Lineup.Remove(seedTeamId, seedPlayerId);
            Assert.IsTrue(removeResult.IsSuccess);

            teamPlayerLineupItem = Lineup.GetTeamPlayerLineupItem(seedTeamId, seedPlayerId);
            Assert.IsNull(teamPlayerLineupItem);

            DeleteSeededTeam(seedTeamId);
            DeleteSeededPlayer(seedPlayerId);
        }

        [TestMethod]
        public void InvalidTeamIdTest()
        {
            TeamPlayerLineupDto dto = new TeamPlayerLineupDto()
            {
                PlayerId = Guid.NewGuid(),
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
                TeamId = Guid.NewGuid(),
                BattingOrder = TEST_BATTING_ORDER
            };

            var result = Lineup.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }


    }
}

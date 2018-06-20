using System;
using System.Collections.Generic;
using System.Text;
using Dartball.BusinessLayer.Team.Implementation;
using Dartball.BusinessLayer.Team.Interface;
using Dartball.BusinessLayer.Team.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DartballBLUnitTest.IntegrationValidation
{
    [TestClass]
    public class TeamUnitTests
    {
        private ITeamService TeamService;

        public TeamUnitTests()
        {
            TeamService = new TeamService();
            TEST_ALTERNATE_ID = Guid.NewGuid();
            TEST_LEAGUE_ALTERNATE_KEY = Guid.NewGuid();
        }

        private const string TEST_TEAM_NAME = "TeamNameTestTravis123?";
        private const string TEST_TEAM_NAME_2 = "TeamNameTestTravis123?!";
        private const string TEST_TEAM_PASSWORD = "Password1?!#$%";
        private const int TEST_HANDICAP = 2;
        private const bool TEST_SHOULD_SYNC = false;
        private Guid TEST_ALTERNATE_ID;
        private Guid TEST_LEAGUE_ALTERNATE_KEY;

        [TestMethod]
        public void AddRemoveTeamUnitTest()
        {
            TeamDto dto = new TeamDto()
            {
                TeamId = TEST_ALTERNATE_ID,
                LeagueId = TEST_LEAGUE_ALTERNATE_KEY,
                Name = TEST_TEAM_NAME,
                Password = TEST_TEAM_PASSWORD,
                Handicap = TEST_HANDICAP,
                ShouldSync = TEST_SHOULD_SYNC
            };

            var addResult = TeamService.AddNew(dto);
            Assert.IsTrue(addResult.IsSuccess);

            dto.Name = TEST_TEAM_NAME_2;
            dto.ShouldSync = true;

            var updateResult = TeamService.Update(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            var team = TeamService.GetTeam(TEST_ALTERNATE_ID);
            Assert.IsNotNull(team);
            Assert.AreEqual(TEST_TEAM_NAME_2, team.Name);
            Assert.AreEqual(true, team.ShouldSync);

            var leagueTeams = TeamService.GetTeams(TEST_LEAGUE_ALTERNATE_KEY);
            Assert.IsTrue(leagueTeams.Count >= 1);

            var deleteResult = TeamService.Remove(TEST_ALTERNATE_ID);
            Assert.IsTrue(deleteResult.IsSuccess);

            var emptyRecord = TeamService.GetTeam(TEST_ALTERNATE_ID);
            Assert.IsNull(emptyRecord);
        }

        [TestMethod]
        public void TeamNameEmptyTest()
        {
            TeamDto dto = new TeamDto()
            {
                TeamId = TEST_ALTERNATE_ID,
                LeagueId = TEST_LEAGUE_ALTERNATE_KEY,
                Name = string.Empty,
                Password = TEST_TEAM_PASSWORD,
                Handicap = TEST_HANDICAP,
                ShouldSync = TEST_SHOULD_SYNC
            };

            var result = TeamService.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }
        
        [TestMethod]
        public void TeamNameTooLongTest()
        {
            TeamDto dto = new TeamDto()
            {
                TeamId = TEST_ALTERNATE_ID,
                LeagueId = TEST_LEAGUE_ALTERNATE_KEY,
                Name = TEST_TEAM_NAME + "kjadsjhglkasdhglksjdhglksjdhglkasjhdglksjdhglkajshdglkajhdglkajhdglkasjdhglkasjhdglkasdjhglkasdjghlaksdgjh",
                Password = TEST_TEAM_PASSWORD,
                Handicap = TEST_HANDICAP,
                ShouldSync = TEST_SHOULD_SYNC
            };

            var result = TeamService.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void BadLeagueIdTest()
        {
            TeamDto dto = new TeamDto()
            {
                TeamId = TEST_ALTERNATE_ID,
                Name = string.Empty,
                Password = TEST_TEAM_PASSWORD,
                Handicap = TEST_HANDICAP,
                ShouldSync = TEST_SHOULD_SYNC
            };

            var result = TeamService.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

    }
}

using Dartball.BusinessLayer.League.Dto;
using Dartball.BusinessLayer.League.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartballBLUnitTests
{
    [TestClass]
    public class LeagueUnitTests
    {
        private ILeagueService LeagueService;
        public LeagueUnitTests()
        {
            LeagueService = new Dartball.BusinessLayer.League.Implementation.LeagueService();
        }

        private const string TEST_LEAGUE_NAME = "LeagueNameTestTravis123?";
        private const string TEST_LEAGUE_PASSWORD = "Password1?!#$%";

        [TestMethod]
        public void AddRemoveLeagueTest()
        {
            LeagueDto dto = new LeagueDto
            {
                Name = TEST_LEAGUE_NAME,
                Password = TEST_LEAGUE_PASSWORD
            };

            var result = LeagueService.Save(dto);
            Assert.IsTrue(result.IsSuccess);

            //make sure the league is actually in the db
            var league = LeagueService.GetLeague(TEST_LEAGUE_NAME);
            Assert.IsNotNull(league);

            //cleanup by deleting the record
            var deleteResult = LeagueService.RemoveLeague(TEST_LEAGUE_NAME);
            Assert.IsTrue(deleteResult.IsSuccess);

            //make sure the league is gone
            var deletedLeague = LeagueService.GetLeague(TEST_LEAGUE_NAME);
            Assert.IsNull(deletedLeague);
        }


    }
}

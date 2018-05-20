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
            TEST_ALTERNATE_ID = Guid.NewGuid();
        }

        private const string TEST_LEAGUE_NAME = "LeagueNameTestTravis123?";
        private const string TEST_LEAGUE_NAME_2 = "LeagueNameTestTravis123?!";
        private const string TEST_LEAGUE_PASSWORD = "Password1?!#$%";
        private Guid TEST_ALTERNATE_ID;

        [TestMethod]
        public void AddRemoveLeagueTest()
        {
            LeagueDto dto = new LeagueDto
            {
                LeagueAlternateKey = TEST_ALTERNATE_ID,
                Name = TEST_LEAGUE_NAME,
                Password = TEST_LEAGUE_PASSWORD
            };

            var result = LeagueService.AddNew(dto);
            Assert.IsTrue(result.IsSuccess);

            //make sure the league is actually in the db
            var league = LeagueService.GetLeague(TEST_ALTERNATE_ID);
            Assert.IsNotNull(league);
            Assert.AreEqual(TEST_LEAGUE_NAME, league.Name);
            Assert.AreEqual(TEST_LEAGUE_PASSWORD, league.Password);

            //make sure an update works
            dto.Name = TEST_LEAGUE_NAME_2;
            var nameChangeResult = LeagueService.Update(dto);
            Assert.IsTrue(nameChangeResult.IsSuccess);

            league = LeagueService.GetLeague(TEST_ALTERNATE_ID);
            Assert.AreEqual(TEST_LEAGUE_NAME_2, league.Name);

            //cleanup by deleting the record
            var deleteResult = LeagueService.RemoveLeague(TEST_ALTERNATE_ID);
            Assert.IsTrue(deleteResult.IsSuccess);

            //make sure the league is gone
            var deletedLeague = LeagueService.GetLeague(TEST_ALTERNATE_ID);
            Assert.IsNull(deletedLeague);
        }

       


    }
}

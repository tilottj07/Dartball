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

       

        [TestMethod] 
        public void InsertNewLeagueTest()
        {
            LeagueDto dto = new LeagueDto();

        }


    }
}

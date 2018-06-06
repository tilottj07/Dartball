﻿using System;
using System.Collections.Generic;
using System.Text;
using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.GameEngine.InGame.Implementation;
using Dartball.BusinessLayer.GameEngine.InGame.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DartballBLUnitTest.GameLogic.InGame
{
    [TestClass]
    public class HalfInningUnitTests : InGameBase
    {
        private IHalfInningService Service;

        public HalfInningUnitTests() 
        {
            Service = new HalfInningService();
        }

        [TestMethod]
        public void OutOutOutInningTest()
        {
            List<IGameInningTeamBatter> gameInningTeamBatters = new List<IGameInningTeamBatter>
            {
                GetTestOutAtBat(),
                GetTestOutAtBat(),
                GetTestOutAtBat()
            };

            var actions = Service.GetHalfInningActions(gameInningTeamBatters);
            Assert.IsTrue(actions.TotalOuts == 3);
            Assert.IsTrue(actions.TotalRuns == 0);
            Assert.IsTrue(actions.AdvanceToNextHalfInning == true);
        }

        [TestMethod]
        public void OutSingleOutOutInningTest()
        {
            List<IGameInningTeamBatter> gameInningTeamBatters = new List<IGameInningTeamBatter>
            {
                GetTestOutAtBat(),
                GetTestSingleAtBat(),
                GetTestOutAtBat(),
                GetTestOutAtBat()
            };

            var actions = Service.GetHalfInningActions(gameInningTeamBatters);
            Assert.IsTrue(actions.TotalOuts == 3);
            Assert.IsTrue(actions.TotalRuns == 0);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.AdvanceToNextHalfInning == true);
        }

        [TestMethod]
        public void OutSingleTripleOutOutInningTest()
        {
            List<IGameInningTeamBatter> gameInningTeamBatters = new List<IGameInningTeamBatter>
            {
                GetTestOutAtBat(),
                GetTestSingleAtBat(),
                GetTestTripleAtBat(),
                GetTestOutAtBat(),
                GetTestOutAtBat()
            };

            var actions = Service.GetHalfInningActions(gameInningTeamBatters);
            Assert.IsTrue(actions.TotalOuts == 3);
            Assert.IsTrue(actions.TotalRuns == 1);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.AdvanceToNextHalfInning == true);
        }

        [TestMethod]
        public void SingleOutSingleOutSingleTripleOutInningTest()
        {
            List<IGameInningTeamBatter> gameInningTeamBatters = new List<IGameInningTeamBatter>
            {
                GetTestSingleAtBat(),
                GetTestOutAtBat(),
                GetTestSingleAtBat(),
                GetTestOutAtBat(),
                GetTestSingleAtBat(),
                GetTestTripleAtBat(),
                GetTestOutAtBat()
            };

            var actions = Service.GetHalfInningActions(gameInningTeamBatters);
            Assert.IsTrue(actions.TotalOuts == 3);
            Assert.IsTrue(actions.TotalRuns == 3);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.AdvanceToNextHalfInning == true);
        }



    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.GameEngine.Event.Dto;
using Dartball.BusinessLayer.GameEngine.Event.Implementation;
using Dartball.BusinessLayer.GameEngine.Event.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DartballBLUnitTest.GameLogic.Event
{
    [TestClass]
    public class GameEventOutUnitTests : EventBase
    {
        private IGameEventOutService Service;
        public GameEventOutUnitTests()
        {
            Service = new GameEventOutService();
        }

        public object IActions { get; private set; }
        public object ActionsDto { get; private set; }

        [TestMethod]
        public void OutTotalTest()
        {
            List<IGameInningTeamBatter> gameInningTeamBatters = new List<IGameInningTeamBatter>
            {
                GetTestOutAtBat(),
                GetTestOutAtBat()
            };

            var actions = Service.FillOutActions(new HalfInningActionsDto(), gameInningTeamBatters);
            Assert.IsTrue(actions.TotalOuts == 2);
        }

        [TestMethod]
        public void AdvanceToNextHalfInningTest()
        {
            List<IGameInningTeamBatter> gameInningTeamBatters = new List<IGameInningTeamBatter>
            {
                GetTestOutAtBat(),
                GetTestOutAtBat(),
                GetTestOutAtBat()
            };

            var actions = Service.FillOutActions(new HalfInningActionsDto(), gameInningTeamBatters);
            Assert.IsTrue(actions.AdvanceToNextHalfInning);
        }

    }
}

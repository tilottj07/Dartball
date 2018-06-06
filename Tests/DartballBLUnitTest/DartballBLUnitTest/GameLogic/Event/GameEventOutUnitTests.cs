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
    public class GameEventOutUnitTests
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
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                TotalOuts = 1
            };

            var actions = Service.FillOutActions(dto);
            Assert.IsTrue(actions.TotalOuts == 2);
        }

        [TestMethod]
        public void AdvanceToNextHalfInningTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                TotalOuts = 2
            };

            var actions = Service.FillOutActions(dto);
            Assert.IsTrue(actions.TotalOuts == 3);
            Assert.IsTrue(actions.AdvanceToNextHalfInning);
        }

    }
}

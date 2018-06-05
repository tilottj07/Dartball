using System;
using System.Collections.Generic;
using System.Text;
using Dartball.BusinessLayer.GameEngine.Event.Dto;
using Dartball.BusinessLayer.GameEngine.Event.Implementation;
using Dartball.BusinessLayer.GameEngine.Event.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DartballBLUnitTest.GameLogic.Event
{
    [TestClass]
    public class GameEventTwoBaseSingleUnitTests : EventBase
    {
        private IGameEventTwoBaseSingleService Service;

        public GameEventTwoBaseSingleUnitTests()
        {
            Service = new GameEventTwoBaseSingleService();
        }


        [TestMethod]
        public void BasesEmptyTwoBaseSingleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto();

            var actions = Service.FillTwoBaseSingleActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 0);
        }

        [TestMethod]
        public void RunnerOnFirstTwoBaseSingleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true
            };

            var actions = Service.FillTwoBaseSingleActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 0);
        }

        [TestMethod]
        public void RunnerOnSecondTwoBaseSingleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnSecond = true
            };

            var actions = Service.FillTwoBaseSingleActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 1);
        }

        [TestMethod]
        public void RunnerOnThirdTwoBaseSingleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnThird = true
            };

            var actions = Service.FillTwoBaseSingleActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 1);
        }

        [TestMethod]
        public void RunnerOnFirstAndSecondTwoBaseSingleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnSecond = true
            };

            var actions = Service.FillTwoBaseSingleActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 1);
        }

        [TestMethod]
        public void RunnerOnSecondAndThirdTwoBaseSingleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnSecond = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillTwoBaseSingleActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 2);
        }


        [TestMethod]
        public void RunnerOnFirstAndThirdTwoBaseSingleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillTwoBaseSingleActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 1);
        }

        [TestMethod]
        public void TheBasesLoadedTwoBaseSingleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnSecond = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillTwoBaseSingleActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 2);
        }

    }
}

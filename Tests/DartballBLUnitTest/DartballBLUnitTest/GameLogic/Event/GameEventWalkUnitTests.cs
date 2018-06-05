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
    public class GameEventWalkUnitTests : EventBase
    {
        private IGameEventWalkService Service;

        public GameEventWalkUnitTests()
        {
            Service = new GameEventWalkService();
        }


        [TestMethod]
        public void BasesEmptyWalkTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto();

            var actions = Service.FillWalkActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 0);
        }

        [TestMethod]
        public void RunnerOnFirstWalkTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true
            };

            var actions = Service.FillWalkActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 0);
        }

        [TestMethod]
        public void RunnerOnSecondWalkTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnSecond = true
            };

            var actions = Service.FillWalkActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 0);
        }

        [TestMethod]
        public void RunnerOnThirdWalkTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnThird = true
            };

            var actions = Service.FillWalkActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 0);
        }

        [TestMethod]
        public void RunnerOnFirstAndSecondWalkTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnSecond = true
            };

            var actions = Service.FillWalkActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 0);
        }

        [TestMethod]
        public void RunnerOnSecondAndThirdWalkTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnSecond = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillWalkActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 0);
        }


        [TestMethod]
        public void RunnerOnFirstAndThirdWalkTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillWalkActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 0);
        }

        [TestMethod]
        public void TheBasesLoadedWalkTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnSecond = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillWalkActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 1);
        }

    }
}

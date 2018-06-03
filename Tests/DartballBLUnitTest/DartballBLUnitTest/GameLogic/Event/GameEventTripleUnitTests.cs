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
    public class GameEventTripleUnitTests : EventBase
    {
        private IGameEventTripleService Service;

        public GameEventTripleUnitTests()
        {
            Service = new GameEventTripleService();
        }


        [TestMethod]
        public void BasesEmptyTripleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto();

            var actions = Service.FillTripleActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 0);
        }

        [TestMethod]
        public void RunnerOnFirstTripleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true
            };

            var actions = Service.FillTripleActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 1);
        }

        [TestMethod]
        public void RunnerOnSecondTripleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnSecond = true
            };

            var actions = Service.FillTripleActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 1);
        }

        [TestMethod]
        public void RunnerOnThirdTripleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnThird = true
            };

            var actions = Service.FillTripleActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 1);
        }

        [TestMethod]
        public void RunnerOnFirstAndSecondTripleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnSecond = true
            };

            var actions = Service.FillTripleActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 2);
        }

        [TestMethod]
        public void RunnerOnSecondAndThirdTripleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnSecond = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillTripleActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 2);
        }


        [TestMethod]
        public void RunnerOnFirstAndThirdTripleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillTripleActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 2);
        }

        [TestMethod]
        public void TheBasesLoadedTripleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnSecond = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillTripleActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 3);
        }

    }
}
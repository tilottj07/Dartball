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
    public class GameEventDoubleUnitTests
    {
        private IGameEventDoubleService Service;

        public GameEventDoubleUnitTests()
        {
            Service = new GameEventDoubleService();
        }


        [TestMethod]
        public void BasesEmptyDoubleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto();

            var actions = Service.FillDoubleActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 0);
        }

        [TestMethod]
        public void RunnerOnFirstDoubleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true
            };

            var actions = Service.FillDoubleActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 0);
        }

        [TestMethod]
        public void RunnerOnSecondDoubleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnSecond = true
            };

            var actions = Service.FillDoubleActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 1);
        }

        [TestMethod]
        public void RunnerOnThirdDoubleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnThird = true
            };

            var actions = Service.FillDoubleActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 1);
        }

        [TestMethod]
        public void RunnerOnFirstAndSecondDoubleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnSecond = true
            };

            var actions = Service.FillDoubleActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 1);
        }

        [TestMethod]
        public void RunnerOnSecondAndThirdDoubleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnSecond = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillDoubleActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 2);
        }


        [TestMethod]
        public void RunnerOnFirstAndThirdDoubleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillDoubleActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 1);
        }

        [TestMethod]
        public void TheBasesLoadedDoubleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnSecond = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillDoubleActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 2);
        }

    }
}
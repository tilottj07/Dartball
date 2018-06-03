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
    public class GameEventHomeRunUnitTests : EventBase
    {
        private IGameEventHomeRunService Service;

        public GameEventHomeRunUnitTests()
        {
            Service = new GameEventHomeRunService();
        }


        [TestMethod]
        public void BasesEmptyHomeRunTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto();

            var actions = Service.FillHomeRunActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 1);
        }

        [TestMethod]
        public void RunnerOnFirstHomeRunTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true
            };

            var actions = Service.FillHomeRunActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 2);
        }

        [TestMethod]
        public void RunnerOnSecondHomeRunTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnSecond = true
            };

            var actions = Service.FillHomeRunActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 2);
        }

        [TestMethod]
        public void RunnerOnThirdHomeRunTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnThird = true
            };

            var actions = Service.FillHomeRunActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 2);
        }

        [TestMethod]
        public void RunnerOnFirstAndSecondHomeRunTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnSecond = true
            };

            var actions = Service.FillHomeRunActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 3);
        }

        [TestMethod]
        public void RunnerOnSecondAndThirdHomeRunTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnSecond = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillHomeRunActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 3);
        }


        [TestMethod]
        public void RunnerOnFirstAndThirdHomeRunTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillHomeRunActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 3);
        }

        [TestMethod]
        public void TheBasesLoadedHomeRunTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnSecond = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillHomeRunActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 4);
        }

    }
}

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
    public class GameEventSingleUnitTests 
    {
        private IGameEventSingleService Service;

        public GameEventSingleUnitTests()
        {
            Service = new GameEventSingleService();
        }


        [TestMethod]
        public void BasesEmptySingleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto();

            var actions = Service.FillSingleActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 0);
        }

        [TestMethod]
        public void RunnerOnFirstSingleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true
            };

            var actions = Service.FillSingleActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 0);
        }

        [TestMethod]
        public void RunnerOnSecondSingleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnSecond = true
            };

            var actions = Service.FillSingleActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 0);
        }

        [TestMethod]
        public void RunnerOnThirdSingleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnThird = true
            };

            var actions = Service.FillSingleActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 1);
        }

        [TestMethod]
        public void RunnerOnFirstAndSecondSingleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnSecond = true
            };

            var actions = Service.FillSingleActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 0);
        }

        [TestMethod]
        public void RunnerOnSecondAndThirdSingleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnSecond = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillSingleActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 1);
        }


        [TestMethod]
        public void RunnerOnFirstAndThirdSingleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillSingleActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 1);
        }

        [TestMethod]
        public void TheBasesLoadedSingleTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnSecond = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillSingleActions(dto);
            Assert.IsTrue(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 1);
        }

    }
}

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
    public class GameEventSacraficeHitUnitTests : EventBase
    {
        private IGameEventSacraficeHitService Service;

        public GameEventSacraficeHitUnitTests()
        {
            Service = new GameEventSacraficeHitService();
        }


        [TestMethod]
        public void BasesEmptySacraficeHitTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto();

            var actions = Service.FillSacraficeHitActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 0);
            Assert.IsTrue(actions.TotalOuts == 1);
        }

        [TestMethod]
        public void RunnerOnFirstSacraficeHitTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true
            };

            var actions = Service.FillSacraficeHitActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 0);
            Assert.IsTrue(actions.TotalOuts == 1);
        }

        [TestMethod]
        public void RunnerOnSecondSacraficeHitTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnSecond = true
            };

            var actions = Service.FillSacraficeHitActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 0);
            Assert.IsTrue(actions.TotalOuts == 1);
        }

        [TestMethod]
        public void RunnerOnThirdTwoOutsSacraficeHitTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnThird = true,
                TotalOuts = 2
            };

            var actions = Service.FillSacraficeHitActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 0);
            Assert.IsTrue(actions.TotalOuts == 3);
            Assert.IsTrue(actions.AdvanceToNextHalfInning == true);
        }

        [TestMethod]
        public void RunnerOnThirdSacraficeHitTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnThird = true
            };

            var actions = Service.FillSacraficeHitActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 1);
            Assert.IsTrue(actions.TotalOuts == 1);
        }

        [TestMethod]
        public void RunnerOnFirstAndSecondSacraficeHitTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnSecond = true
            };

            var actions = Service.FillSacraficeHitActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 0);
            Assert.IsTrue(actions.TotalOuts == 1);
        }

        [TestMethod]
        public void RunnerOnSecondAndThirdSacraficeHitTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnSecond = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillSacraficeHitActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 1);
            Assert.IsTrue(actions.TotalOuts == 1);
        }


        [TestMethod]
        public void RunnerOnFirstAndThirdSacraficeHitTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillSacraficeHitActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 1);
            Assert.IsTrue(actions.TotalOuts == 1);
        }

        [TestMethod]
        public void TheBasesLoadedSacraficeHitTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnSecond = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillSacraficeHitActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalRuns == 1);
            Assert.IsTrue(actions.TotalOuts == 1);
        }

    }
}

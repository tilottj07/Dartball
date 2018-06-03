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
    public class GameEventDoublePlayUnitTests : EventBase
    {
        private IGameEventDoublePlayService Service;

        public GameEventDoublePlayUnitTests()
        {
            Service = new GameEventDoublePlayService();
        }


        [TestMethod]
        public void BasesEmptyDoublePlayTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto();

            var actions = Service.FillDoublePlayActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalOuts == 1);
        }

        [TestMethod]
        public void RunnerOnFirstDoublePlayTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true
            };

            var actions = Service.FillDoublePlayActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalOuts == 2);
        }

        [TestMethod]
        public void RunnerOnSecondDoublePlayTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnSecond = true
            };

            var actions = Service.FillDoublePlayActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalOuts == 2);
        }

        [TestMethod]
        public void RunnerOnThirdDoublePlayTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnThird = true
            };

            var actions = Service.FillDoublePlayActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalOuts == 2);
        }

        [TestMethod]
        public void RunnerOnFirstAndSecondDoublePlayTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnSecond = true
            };

            var actions = Service.FillDoublePlayActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsFalse(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalOuts == 2);
        }

        [TestMethod]
        public void RunnerOnSecondAndThirdDoublePlayTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnSecond = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillDoublePlayActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalOuts == 2);
        }


        [TestMethod]
        public void RunnerOnFirstAndThirdDoublePlayTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillDoublePlayActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsFalse(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalOuts == 2);
        }

        [TestMethod]
        public void TheBasesLoadedDoublePlayTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto
            {
                IsRunnerOnFirst = true,
                IsRunnerOnSecond = true,
                IsRunnerOnThird = true
            };

            var actions = Service.FillDoublePlayActions(dto);
            Assert.IsFalse(actions.IsRunnerOnFirst);
            Assert.IsTrue(actions.IsRunnerOnSecond);
            Assert.IsTrue(actions.IsRunnerOnThird);
            Assert.IsTrue(actions.TotalOuts == 2);
        }

        [TestMethod]
        public void AdvanceToNextHalfInningTest()
        {
            HalfInningActionsDto dto = new HalfInningActionsDto()
            {
                TotalOuts = 2
            };
            var actions = Service.FillDoublePlayActions(dto);
            Assert.IsTrue(actions.AdvanceToNextHalfInning);
            Assert.IsTrue(actions.TotalOuts == 3);

            dto.TotalOuts = 1;
            dto.IsRunnerOnFirst = true;

            actions = Service.FillDoublePlayActions(dto);
            Assert.IsTrue(actions.AdvanceToNextHalfInning);
            Assert.IsTrue(actions.TotalOuts == 3);

            dto.TotalOuts = 2;
            actions = Service.FillDoublePlayActions(dto);
            Assert.IsTrue(actions.AdvanceToNextHalfInning);
            Assert.IsTrue(actions.TotalOuts == 4);

        }


    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;
using Dartball.BusinessLayer.Game.Dto;
using Dartball.BusinessLayer.Game.Implementation;
using Dartball.BusinessLayer.Game.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DartballBLUnitTest.IntegrationValidation
{
    [TestClass]
    public class GameInningUnitTests
    {
        private IGameInningService GameInning;

        public GameInningUnitTests()
        {
            GameInning = new GameInningService();
            TEST_GAME_ALTERNATE_KEY = Guid.NewGuid();
        }

        private Guid TEST_GAME_ALTERNATE_KEY;
        private const int TEST_INNING_NUMBER = 3;


        [TestMethod]
        public void AddUpdateGameInningTest()
        {
            GameInningDto dto = new GameInningDto()
            {
                GameAlternateKey = TEST_GAME_ALTERNATE_KEY,
                InningNumber = TEST_INNING_NUMBER
            };

            var addResult = GameInning.AddNew(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var item = GameInning.GetGameInning(TEST_GAME_ALTERNATE_KEY, TEST_INNING_NUMBER);
            Assert.IsNotNull(item);
            Assert.AreEqual(TEST_GAME_ALTERNATE_KEY, item.GameAlternateKey);
            Assert.AreEqual(TEST_INNING_NUMBER, item.InningNumber);

            dto.InningNumber = 4;
            addResult = GameInning.AddNew(dto);
            Assert.IsTrue(addResult.IsSuccess);

            item = GameInning.GetGameInning(TEST_GAME_ALTERNATE_KEY, inningNumber: 4);

            dto.GameInningAlternateKey = item.GameInningAlternateKey;
            dto.DeleteDate = DateTime.UtcNow;
            var updateResult = GameInning.Update(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            item = GameInning.GetGameInning(TEST_GAME_ALTERNATE_KEY, inningNumber: 4);
            Assert.IsNotNull(item);
            Assert.IsNotNull(item.DeleteDate);

            dto.DeleteDate = null;
            updateResult = GameInning.Update(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            item = GameInning.GetGameInning(TEST_GAME_ALTERNATE_KEY, inningNumber: 4);
            Assert.IsNotNull(item);
            Assert.IsNull(item.DeleteDate);

            var items = GameInning.GetGameInnings(TEST_GAME_ALTERNATE_KEY);
            Assert.IsTrue(items.Count >= 1);

            foreach(var i in items)
            {
                var removeResult = GameInning.Remove(i.GameAlternateKey, i.InningNumber);
                Assert.IsTrue(removeResult.IsSuccess);
            }

            items = GameInning.GetGameInnings(TEST_GAME_ALTERNATE_KEY);
            Assert.IsTrue(items.Count == 0);
        }


        [TestMethod]
        public void InvalidGameAlternateKeyTest()
        {
            GameInningDto dto = new GameInningDto()
            {
                InningNumber = TEST_INNING_NUMBER
            };

            var result = GameInning.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidInningNumberTest()
        {
            GameInningDto dto = new GameInningDto()
            {
                GameAlternateKey = TEST_GAME_ALTERNATE_KEY,
                InningNumber = -2
            };

            var result = GameInning.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidGameInningAlternateKeyTest()
        {
            GameInningDto dto = new GameInningDto()
            {
                GameAlternateKey = TEST_GAME_ALTERNATE_KEY,
                InningNumber = TEST_INNING_NUMBER
            };

            var result = GameInning.Update(dto);
            Assert.IsFalse(result.IsSuccess);
        }

    }
}
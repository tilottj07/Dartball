using System;
using System.Collections.Generic;
using System.Text;
using Dartball.BusinessLayer.Game.Dto;
using Dartball.BusinessLayer.Game.Implementation;
using Dartball.BusinessLayer.Game.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DartballBLUnitTest.IntegrationValidation
{
    [TestClass]
    public class GameInningUnitTests : IntegrationBase
    {
        private IGameInningService GameInning;

        public GameInningUnitTests()
        {
            GameInning = new GameInningService();
        }

        private const int TEST_INNING_NUMBER = 3;


        [TestMethod]
        public void AddUpdateGameInningTest()
        {
            Guid seedGameId = SeedGame();

            GameInningDto dto = new GameInningDto()
            {
                GameId = seedGameId,
                InningNumber = TEST_INNING_NUMBER
            };

            var addResult = GameInning.AddNew(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var item = GameInning.GetGameInning(seedGameId, TEST_INNING_NUMBER);
            Assert.IsNotNull(item);
            Assert.AreEqual(seedGameId, item.GameId);
            Assert.AreEqual(TEST_INNING_NUMBER, item.InningNumber);

            dto.InningNumber = 4;
            addResult = GameInning.AddNew(dto);
            Assert.IsTrue(addResult.IsSuccess);

            item = GameInning.GetGameInning(seedGameId, inningNumber: 4);

            dto.GameInningId = item.GameInningId;
            dto.DeleteDate = DateTime.UtcNow;
            var updateResult = GameInning.Update(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            item = GameInning.GetGameInning(seedGameId, inningNumber: 4);
            Assert.IsNotNull(item);
            Assert.IsNotNull(item.DeleteDate);

            dto.DeleteDate = null;
            updateResult = GameInning.Update(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            item = GameInning.GetGameInning(seedGameId, inningNumber: 4);
            Assert.IsNotNull(item);
            Assert.IsNull(item.DeleteDate);

            var items = GameInning.GetGameInnings(seedGameId);
            Assert.IsTrue(items.Count >= 1);

            foreach(var i in items)
            {
                var removeResult = GameInning.Remove(i.GameId, i.InningNumber);
                Assert.IsTrue(removeResult.IsSuccess);
            }

            items = GameInning.GetGameInnings(seedGameId);
            Assert.IsTrue(items.Count == 0);

            DeleteSeededGame(seedGameId);
        }


        [TestMethod]
        public void InvalidGameIdTest()
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
                GameId = Guid.NewGuid(),
                InningNumber = -2
            };

            var result = GameInning.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidGameInningIdTest()
        {
            GameInningDto dto = new GameInningDto()
            {
                GameId = Guid.NewGuid(),
                InningNumber = TEST_INNING_NUMBER
            };

            var result = GameInning.Update(dto);
            Assert.IsFalse(result.IsSuccess);
        }

    }
}

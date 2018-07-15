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
    public class GameUnitTests : IntegrationBase
    {
        private IGameService Game;

        public GameUnitTests()
        {
            Game = new GameService();

            Dartball.Data.DartballContext dartballContext = new Dartball.Data.DartballContext();
            dartballContext.Migrate();

            TEST_GAME_ID = Guid.NewGuid();
            TEST_GAME_DATE = DateTime.Today;
            TEST_GAME_DATE_2 = DateTime.Today.AddDays(-1);
        }


        private Guid TEST_GAME_ID;
        private DateTime TEST_GAME_DATE;
        private DateTime TEST_GAME_DATE_2;


        [TestMethod]
        public void AddUpdateGameTest()
        {
            Guid seedLeagueId = SeedLeague();
            GameDto dto = new GameDto()
            {
                GameId = TEST_GAME_ID,
                LeagueId = seedLeagueId,
                GameDate = TEST_GAME_DATE
            };

            var addNewResult = Game.AddNew(dto);
            Assert.IsTrue(addNewResult.IsSuccess);

            var item = Game.GetGame(TEST_GAME_ID);
            Assert.IsNotNull(item);
            Assert.AreEqual(seedLeagueId, item.LeagueId);
            Assert.AreEqual(TEST_GAME_DATE, item.GameDate);
            Assert.AreEqual(TEST_GAME_ID, item.GameId);
            Assert.IsNull(item.DeleteDate);

            dto.GameDate = TEST_GAME_DATE_2;
            var updateResult = Game.Update(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            item = Game.GetGame(TEST_GAME_ID);
            Assert.IsNotNull(item);
            Assert.AreEqual(TEST_GAME_DATE_2, item.GameDate);

            var items = Game.GetLeagueGames(seedLeagueId);
            Assert.IsTrue(items.Count >= 1);

            items = Game.GetAllGames();
            Assert.IsTrue(items.Count >= 1);

            var removeResult = Game.Remove(TEST_GAME_ID);
            Assert.IsTrue(removeResult.IsSuccess);

            item = Game.GetGame(TEST_GAME_ID);
            Assert.IsNull(item);

            DeleteSeededLeague(seedLeagueId);
        }


        [TestMethod]
        public void InvalidLeagueIdTest()
        {
            GameDto dto = new GameDto()
            {
                GameId = TEST_GAME_ID,
                GameDate = TEST_GAME_DATE
            };

            var result = Game.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidGameDateTest()
        {
            GameDto dto = new GameDto()
            {
                GameId = TEST_GAME_ID,
                LeagueId = Guid.NewGuid(),
                GameDate = TEST_GAME_DATE.AddDays(10)
            };

            var result = Game.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidGameIdTest()
        {
            GameDto dto = new GameDto()
            {
                LeagueId = Guid.NewGuid(),
                GameDate = TEST_GAME_DATE
            };

            var result = Game.Update(dto);
            Assert.IsFalse(result.IsSuccess);
        }
    }
}

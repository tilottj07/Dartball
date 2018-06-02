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
    public class GameUnitTests
    {
        private IGameService Game;

        public GameUnitTests()
        {
            Game = new GameService();

            TEST_LEAGUE_ALTERNATE_KEY = Guid.NewGuid();
            TEST_GAME_ALTERNATE_KEY = Guid.NewGuid();
            TEST_GAME_DATE = DateTime.Today;
            TEST_GAME_DATE_2 = DateTime.Today.AddDays(-1);
        }


        private Guid TEST_LEAGUE_ALTERNATE_KEY;
        private Guid TEST_GAME_ALTERNATE_KEY;
        private DateTime TEST_GAME_DATE;
        private DateTime TEST_GAME_DATE_2;


        [TestMethod]
        public void AddUpdateGameTest()
        {
            GameDto dto = new GameDto()
            {
                GameAlternateKey = TEST_GAME_ALTERNATE_KEY,
                LeagueAlternateKey = TEST_LEAGUE_ALTERNATE_KEY,
                GameDate = TEST_GAME_DATE
            };

            var addNewResult = Game.AddNew(dto);
            Assert.IsTrue(addNewResult.IsSuccess);

            var item = Game.GetGame(TEST_GAME_ALTERNATE_KEY);
            Assert.IsNotNull(item);
            Assert.AreEqual(TEST_LEAGUE_ALTERNATE_KEY, item.LeagueAlternateKey);
            Assert.AreEqual(TEST_GAME_DATE, item.GameDate);
            Assert.AreEqual(TEST_GAME_ALTERNATE_KEY, item.GameAlternateKey);
            Assert.IsNull(item.DeleteDate);

            dto.GameDate = TEST_GAME_DATE_2;
            var updateResult = Game.Update(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            item = Game.GetGame(TEST_GAME_ALTERNATE_KEY);
            Assert.IsNotNull(item);
            Assert.AreEqual(TEST_GAME_DATE_2, item.GameDate);

            var items = Game.GetLeagueGames(TEST_LEAGUE_ALTERNATE_KEY);
            Assert.IsTrue(items.Count >= 1);

            items = Game.GetAllGames();
            Assert.IsTrue(items.Count >= 1);

            var removeResult = Game.Remove(TEST_GAME_ALTERNATE_KEY);
            Assert.IsTrue(removeResult.IsSuccess);

            item = Game.GetGame(TEST_GAME_ALTERNATE_KEY);
            Assert.IsNull(item);
        }


        [TestMethod]
        public void InvalidLeagueAlternateKeyTest()
        {
            GameDto dto = new GameDto()
            {
                GameAlternateKey = TEST_GAME_ALTERNATE_KEY,
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
                GameAlternateKey = TEST_GAME_ALTERNATE_KEY,
                LeagueAlternateKey = TEST_LEAGUE_ALTERNATE_KEY,
                GameDate = TEST_GAME_DATE.AddDays(10)
            };

            var result = Game.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidGameAlternateKeyTest()
        {
            GameDto dto = new GameDto()
            {
                LeagueAlternateKey = TEST_LEAGUE_ALTERNATE_KEY,
                GameDate = TEST_GAME_DATE
            };

            var result = Game.Update(dto);
            Assert.IsFalse(result.IsSuccess);
        }
    }
}

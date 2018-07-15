using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dartball.BusinessLayer.Game.Dto;
using Dartball.BusinessLayer.Game.Implementation;
using Dartball.BusinessLayer.Game.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DartballBLUnitTest.IntegrationValidation
{
    [TestClass]
    public class GameTeamUnitTests : IntegrationBase
    {
        private IGameTeamService GameTeam;

        public GameTeamUnitTests()
        {
            GameTeam = new GameTeamService();

            Dartball.Data.DartballContext dartballContext = new Dartball.Data.DartballContext();
            dartballContext.Migrate();
        }



        [TestMethod]
        public void AddUpdateGameTeamTest()
        {
            Guid seedGameId = SeedGame();
            Guid seedTeamId = SeedTeam();

            GameTeamDto dto = new GameTeamDto()
            {
                GameId = seedGameId,
                TeamId = seedTeamId
            };

            var addResult = GameTeam.AddNew(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var item = GameTeam.GetGameTeam(seedGameId, seedTeamId);
            Assert.IsNotNull(item);
            Assert.AreEqual(seedTeamId, item.TeamId);
            Assert.AreEqual(seedGameId, item.GameId);

            dto.GameTeamId = item.GameTeamId;
            dto.DeleteDate = DateTime.UtcNow;

            var updateResult = GameTeam.Update(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            item = GameTeam.GetGameTeam(seedGameId, seedTeamId);
            Assert.IsNotNull(item);
            Assert.IsNotNull(item.DeleteDate);

            dto.DeleteDate = null;
            updateResult = GameTeam.Update(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            var items = GameTeam.GetGameTeams(seedGameId);
            Assert.IsTrue(items.Count >= 1);

            item = items.FirstOrDefault(x => x.TeamId == seedTeamId);
            Assert.IsNotNull(item);

            var removeResult = GameTeam.Remove(seedGameId, seedTeamId);
            Assert.IsTrue(removeResult.IsSuccess);

            item = GameTeam.GetGameTeam(seedGameId, seedTeamId);
            Assert.IsNull(item);

            DeleteSeededGame(seedGameId);
            DeleteSeededTeam(seedTeamId);
        }

        [TestMethod]
        public void InvalidGameIdTest()
        {
            GameTeamDto dto = new GameTeamDto()
            {
                TeamId = Guid.NewGuid()
            };

            var result = GameTeam.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidTeamIdTest()
        {
            GameTeamDto dto = new GameTeamDto()
            {
                GameId = Guid.NewGuid(),
            };

            var result = GameTeam.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidGameTeamIdTest()
        {
            GameTeamDto dto = new GameTeamDto()
            {
                GameId = Guid.NewGuid(),
                TeamId = Guid.NewGuid()
            };

            var result = GameTeam.Update(dto);
            Assert.IsFalse(result.IsSuccess);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dartball.BusinessLayer.Player.Dto;
using Dartball.BusinessLayer.Player.Implementation;
using Dartball.BusinessLayer.Player.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DartballBLUnitTest.IntegrationValidation
{
    [TestClass]
    public class PlayerTeamUnitTests : IntegrationBase
    {
        private IPlayerTeamService PlayerTeamService;

        public PlayerTeamUnitTests()
        {
            PlayerTeamService = new PlayerTeamService();

            Dartball.Data.DartballContext dartballContext = new Dartball.Data.DartballContext();
            dartballContext.Migrate();
        }



        [TestMethod]
        public void AddUpdatePlayerTeamTest()
        {
            Guid seedTeamId = SeedTeam();
            Guid seedPlayerId = SeedPlayer();

            PlayerTeamDto dto = new PlayerTeamDto()
            {
                PlayerId = seedPlayerId,
                TeamId = seedTeamId
            };

            var addResult = PlayerTeamService.AddNew(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var playerTeam = PlayerTeamService.GetPlayerTeam(seedTeamId, seedPlayerId);
            Assert.IsNotNull(playerTeam);
            Assert.AreEqual(seedTeamId, playerTeam.TeamId);
            Assert.AreEqual(seedPlayerId, playerTeam.PlayerId);
            Assert.IsFalse(playerTeam.PlayerTeamId == Guid.Empty);

            dto.PlayerTeamId = playerTeam.PlayerTeamId;
            var updateResult = PlayerTeamService.Update(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            var teamPlayers = PlayerTeamService.GetTeamPlayers(seedTeamId);
            Assert.IsTrue(teamPlayers.Count >= 1);

            var teamPlayerInfos = PlayerTeamService.GetTeamPlayerInformations(seedTeamId);
            Assert.IsTrue(teamPlayerInfos.Count >= 1);
            Assert.IsNotNull(teamPlayerInfos.FirstOrDefault().Name);

            var removeResult = PlayerTeamService.Remove(seedPlayerId, seedTeamId);
            Assert.IsTrue(removeResult.IsSuccess);

            playerTeam = PlayerTeamService.GetPlayerTeam(seedTeamId, seedPlayerId);
            Assert.IsNull(playerTeam);

            DeleteSeededTeam(seedTeamId);
            DeleteSeededPlayer(seedPlayerId);
        }


        [TestMethod]
        public void InvalidTeamIdTest()
        {
            PlayerTeamDto dto = new PlayerTeamDto()
            {
                PlayerId = Guid.NewGuid(),
                TeamId = Guid.Empty
            };

            var addResult = PlayerTeamService.AddNew(dto);
            Assert.IsFalse(addResult.IsSuccess);
        }

        [TestMethod]
        public void InvalidPlayerIdTest()
        {
            PlayerTeamDto dto = new PlayerTeamDto()
            {
                PlayerId = Guid.Empty,
                TeamId = Guid.NewGuid()
            };

            var addResult = PlayerTeamService.AddNew(dto);
            Assert.IsFalse(addResult.IsSuccess);
        }

        [TestMethod]
        public void InvalidTeamPlayerIdTest()
        {
            PlayerTeamDto dto = new PlayerTeamDto()
            {
                PlayerId = Guid.NewGuid(),
                TeamId = Guid.Empty
            };

            var result = PlayerTeamService.Update(dto);
            Assert.IsFalse(result.IsSuccess);
        }

    }
}

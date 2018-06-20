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
    public class GameInningTeamBatterUnitTests : IntegrationBase
    {
        private IGameInningTeamBatterService Service;

        public GameInningTeamBatterUnitTests()
        {
            Service = new GameInningTeamBatterService();
        }

        private const int TEST_SEQUENCE = 1;
        private const int TEST_RBIS = 0;
        private const int TEST_RBIS_2 = 1;
        private const int TEST_EVENT_TYPE = 1;
        private const int TEST_EVENT_TYPE_2 = 2;
        private const int TEST_TARGET_EVENT_TYPE = 0;
        private const int TEST_TARGET_EVENT_TYPE_2 = 1;



        [TestMethod]
        public void AddUpdateGameInningTeamBatterTest()
        {
            Guid seedGameInningTeamId = SeedGameInningTeam();
            Guid seedPlayerId = SeedPlayer();

            GameInningTeamBatterDto dto = new GameInningTeamBatterDto()
            {
                GameInningTeamId = seedGameInningTeamId,
                PlayerId = seedPlayerId,
                Sequence = TEST_SEQUENCE,
                EventType = TEST_EVENT_TYPE,
                TargetEventType = TEST_TARGET_EVENT_TYPE,
                RBIs = TEST_RBIS
            };

            var addResult = Service.AddNew(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var item = Service.GetGameInningTeamBatter(seedGameInningTeamId, TEST_SEQUENCE);
            Assert.IsNotNull(item);
            Assert.AreEqual(item.GameInningTeamId, seedGameInningTeamId);
            Assert.AreEqual(item.PlayerId, seedPlayerId);
            Assert.AreEqual(item.Sequence, TEST_SEQUENCE);
            Assert.AreEqual(item.RBIs, TEST_RBIS);
            Assert.AreEqual(item.EventType, TEST_EVENT_TYPE);
            Assert.AreEqual(item.TargetEventType, TEST_TARGET_EVENT_TYPE);

            dto.GameInningTeamBatterId = item.GameInningTeamBatterId;
            dto.RBIs = TEST_RBIS_2;
            dto.EventType = TEST_EVENT_TYPE_2;
            dto.TargetEventType = TEST_TARGET_EVENT_TYPE_2;

            var updateResult = Service.Update(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            var inningAtBats = Service.GetGameInningTeamBatters(seedGameInningTeamId);
            Assert.IsTrue(inningAtBats.Count > 0);

            item = inningAtBats.FirstOrDefault(x => x.Sequence == TEST_SEQUENCE);
            Assert.IsNotNull(item);
            Assert.AreEqual(item.GameInningTeamId, seedGameInningTeamId);
            Assert.AreEqual(item.PlayerId, seedPlayerId);
            Assert.AreEqual(item.Sequence, TEST_SEQUENCE);
            Assert.AreEqual(item.RBIs, TEST_RBIS_2);
            Assert.AreEqual(item.EventType, TEST_EVENT_TYPE_2);
            Assert.AreEqual(item.TargetEventType, TEST_TARGET_EVENT_TYPE_2);

            var removeResult = Service.Remove(seedGameInningTeamId, TEST_SEQUENCE);
            Assert.IsTrue(removeResult.IsSuccess);

            item = Service.GetGameInningTeamBatter(seedGameInningTeamId, TEST_SEQUENCE);
            Assert.IsNull(item);

            DeleteSeededGameInningTeam(seedGameInningTeamId);
            DeleteSeededPlayer(seedPlayerId);
        }

        [TestMethod]
        public void InvalidGameInningTeamIdTest()
        {
            GameInningTeamBatterDto dto = new GameInningTeamBatterDto()
            {
                PlayerId = Guid.NewGuid(),
                Sequence = TEST_SEQUENCE,
                EventType = TEST_EVENT_TYPE,
                TargetEventType = TEST_TARGET_EVENT_TYPE,
                RBIs = TEST_RBIS
            };

            var result = Service.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidPlayerIdTest()
        {
            GameInningTeamBatterDto dto = new GameInningTeamBatterDto()
            {
                GameInningTeamId = Guid.NewGuid(),
                Sequence = TEST_SEQUENCE,
                EventType = TEST_EVENT_TYPE,
                TargetEventType = TEST_TARGET_EVENT_TYPE,
                RBIs = TEST_RBIS
            };

            var result = Service.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidSequenceTest()
        {
            GameInningTeamBatterDto dto = new GameInningTeamBatterDto()
            {
                GameInningTeamId = Guid.NewGuid(),
                PlayerId = Guid.NewGuid(),
                Sequence = -1,
                EventType = TEST_EVENT_TYPE,
                TargetEventType = TEST_TARGET_EVENT_TYPE,
                RBIs = TEST_RBIS
            };

            var result = Service.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidRBIsTest()
        {
            GameInningTeamBatterDto dto = new GameInningTeamBatterDto()
            {
                GameInningTeamId = Guid.NewGuid(),
                PlayerId = Guid.NewGuid(),
                Sequence = TEST_SEQUENCE,
                EventType = TEST_EVENT_TYPE,
                TargetEventType = TEST_TARGET_EVENT_TYPE,
                RBIs = -3
            };

            var result = Service.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidGameInningTeamBatterIdTest()
        {
            GameInningTeamBatterDto dto = new GameInningTeamBatterDto()
            {
                GameInningTeamId = Guid.NewGuid(),
                PlayerId = Guid.NewGuid(),
                Sequence = TEST_SEQUENCE,
                EventType = TEST_EVENT_TYPE,
                TargetEventType = TEST_TARGET_EVENT_TYPE,
                RBIs = TEST_RBIS
            };

            var result = Service.Update(dto);
            Assert.IsFalse(result.IsSuccess);
        }


        [TestMethod]
        public void InvalidEventTypeTest()
        {
            GameInningTeamBatterDto dto = new GameInningTeamBatterDto()
            {
                GameInningTeamId = Guid.NewGuid(),
                PlayerId = Guid.NewGuid(),
                Sequence = TEST_SEQUENCE,
                EventType = 92,
                TargetEventType = TEST_TARGET_EVENT_TYPE,
                RBIs = TEST_RBIS
            };

            var result = Service.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidTargetEventTypeTest()
        {
            GameInningTeamBatterDto dto = new GameInningTeamBatterDto()
            {
                GameInningTeamId = Guid.NewGuid(),
                PlayerId = Guid.NewGuid(),
                Sequence = TEST_SEQUENCE,
                EventType = TEST_EVENT_TYPE,
                TargetEventType = 105,
                RBIs = TEST_RBIS
            };

            var result = Service.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }


    }
}

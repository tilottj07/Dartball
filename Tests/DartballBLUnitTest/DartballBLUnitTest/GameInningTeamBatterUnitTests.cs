using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dartball.BusinessLayer.Game.Dto;
using Dartball.BusinessLayer.Game.Implementation;
using Dartball.BusinessLayer.Game.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DartballBLUnitTest
{
    [TestClass]
    public class GameInningTeamBatterUnitTests
    {
        private IGameInningTeamBatterService Service;

        public GameInningTeamBatterUnitTests()
        {
            Service = new GameInningTeamBatterService();

            TEST_GAME_INNING_TEAM_ALTERNATE_KEY = Guid.NewGuid();
            TEST_PLAYER_ALTERNATE_KEY = Guid.NewGuid();
        }

        private Guid TEST_GAME_INNING_TEAM_ALTERNATE_KEY;
        private Guid TEST_PLAYER_ALTERNATE_KEY;
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
            GameInningTeamBatterDto dto = new GameInningTeamBatterDto()
            {
                GameInningTeamAlternateKey = TEST_GAME_INNING_TEAM_ALTERNATE_KEY,
                PlayerAlternateKey = TEST_PLAYER_ALTERNATE_KEY,
                Sequence = TEST_SEQUENCE,
                EventType = TEST_EVENT_TYPE,
                TargetEventType = TEST_TARGET_EVENT_TYPE,
                RBIs = TEST_RBIS
            };

            var addResult = Service.AddNew(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var item = Service.GetGameInningTeamBatter(TEST_GAME_INNING_TEAM_ALTERNATE_KEY, TEST_SEQUENCE);
            Assert.IsNotNull(item);
            Assert.AreEqual(item.GameInningTeamAlternateKey, TEST_GAME_INNING_TEAM_ALTERNATE_KEY);
            Assert.AreEqual(item.PlayerAlternateKey, TEST_PLAYER_ALTERNATE_KEY);
            Assert.AreEqual(item.Sequence, TEST_SEQUENCE);
            Assert.AreEqual(item.RBIs, TEST_RBIS);
            Assert.AreEqual(item.EventType, TEST_EVENT_TYPE);
            Assert.AreEqual(item.TargetEventType, TEST_TARGET_EVENT_TYPE);

            dto.GameInningTeamBatterAlternateKey = item.GameInningTeamBatterAlternateKey;
            dto.RBIs = TEST_RBIS_2;
            dto.EventType = TEST_EVENT_TYPE_2;
            dto.TargetEventType = TEST_TARGET_EVENT_TYPE_2;

            var updateResult = Service.Update(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            var inningAtBats = Service.GetGameInningTeamBatters(TEST_GAME_INNING_TEAM_ALTERNATE_KEY);
            Assert.IsTrue(inningAtBats.Count > 0);

            item = inningAtBats.FirstOrDefault(x => x.Sequence == TEST_SEQUENCE);
            Assert.IsNotNull(item);
            Assert.AreEqual(item.GameInningTeamAlternateKey, TEST_GAME_INNING_TEAM_ALTERNATE_KEY);
            Assert.AreEqual(item.PlayerAlternateKey, TEST_PLAYER_ALTERNATE_KEY);
            Assert.AreEqual(item.Sequence, TEST_SEQUENCE);
            Assert.AreEqual(item.RBIs, TEST_RBIS_2);
            Assert.AreEqual(item.EventType, TEST_EVENT_TYPE_2);
            Assert.AreEqual(item.TargetEventType, TEST_TARGET_EVENT_TYPE_2);

            var removeResult = Service.Remove(TEST_GAME_INNING_TEAM_ALTERNATE_KEY, TEST_SEQUENCE);
            Assert.IsTrue(removeResult.IsSuccess);

            item = Service.GetGameInningTeamBatter(TEST_GAME_INNING_TEAM_ALTERNATE_KEY, TEST_SEQUENCE);
            Assert.IsNull(item);
        }

        [TestMethod]
        public void InvalidGameInningTeamAlternateKeyTest()
        {
            GameInningTeamBatterDto dto = new GameInningTeamBatterDto()
            {
                PlayerAlternateKey = TEST_PLAYER_ALTERNATE_KEY,
                Sequence = TEST_SEQUENCE,
                EventType = TEST_EVENT_TYPE,
                TargetEventType = TEST_TARGET_EVENT_TYPE,
                RBIs = TEST_RBIS
            };

            var result = Service.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidPlayerAlternateKeyTest()
        {
            GameInningTeamBatterDto dto = new GameInningTeamBatterDto()
            {
                GameInningTeamAlternateKey = TEST_GAME_INNING_TEAM_ALTERNATE_KEY,
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
                GameInningTeamAlternateKey = TEST_GAME_INNING_TEAM_ALTERNATE_KEY,
                PlayerAlternateKey = TEST_PLAYER_ALTERNATE_KEY,
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
                GameInningTeamAlternateKey = TEST_GAME_INNING_TEAM_ALTERNATE_KEY,
                PlayerAlternateKey = TEST_PLAYER_ALTERNATE_KEY,
                Sequence = TEST_SEQUENCE,
                EventType = TEST_EVENT_TYPE,
                TargetEventType = TEST_TARGET_EVENT_TYPE,
                RBIs = -3
            };

            var result = Service.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidGameInningTeamBatterAlternateKeyTest()
        {
            GameInningTeamBatterDto dto = new GameInningTeamBatterDto()
            {
                GameInningTeamAlternateKey = TEST_GAME_INNING_TEAM_ALTERNATE_KEY,
                PlayerAlternateKey = TEST_PLAYER_ALTERNATE_KEY,
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
                GameInningTeamAlternateKey = TEST_GAME_INNING_TEAM_ALTERNATE_KEY,
                PlayerAlternateKey = TEST_PLAYER_ALTERNATE_KEY,
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
                GameInningTeamAlternateKey = TEST_GAME_INNING_TEAM_ALTERNATE_KEY,
                PlayerAlternateKey = TEST_PLAYER_ALTERNATE_KEY,
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

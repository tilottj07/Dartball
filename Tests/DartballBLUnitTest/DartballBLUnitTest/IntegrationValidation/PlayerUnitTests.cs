using System;
using System.Collections.Generic;
using System.Text;
using Dartball.BusinessLayer.Player.Dto;
using Dartball.BusinessLayer.Player.Implementation;
using Dartball.BusinessLayer.Player.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DartballBLUnitTest.IntegrationValidation
{
    [TestClass]
    public class PlayerUnitTests
    {
        private IPlayerService PlayerService;

        public PlayerUnitTests()
        {
            PlayerService = new PlayerService();
            TEST_ID = Guid.NewGuid();
        }

        private Guid TEST_ID;
        private const string TEST_NAME = "Johnny Appleseed";
        private const string TEST_NAME2 = "Jonny Appleseed";
        private const string TEST_USERNAME = "johnnyappleseed";
        private const string TEST_PASSWORD = "password987";
        private const string TEST_EMAIL = "appleseed123@gmail.com";


        [TestMethod]
        public void AddRemovePlayerTest()
        {
            PlayerDto dto = new PlayerDto()
            {
                PlayerId = TEST_ID,
                Name = TEST_NAME,
                UserName = TEST_USERNAME,
                Password = TEST_PASSWORD,
                EmailAddress = TEST_EMAIL,
                ShouldSync = false
            };

            var addResult = PlayerService.AddNew(dto);
            Assert.IsTrue(addResult.IsSuccess);

            dto.Name = TEST_NAME2;
            dto.ShouldSync = true;
            var updateResult = PlayerService.Update(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            var player = PlayerService.GetPlayer(TEST_ID);
            Assert.IsNotNull(player);
            Assert.AreEqual(TEST_NAME2, player.Name);
            Assert.AreEqual(true, player.ShouldSync);

            var players = PlayerService.GetPlayers();
            Assert.IsTrue(players.Count >= 1);

            var deleteResult = PlayerService.Remove(TEST_ID);
            Assert.IsTrue(deleteResult.IsSuccess);
        }

        [TestMethod]
        public void BlankNameTest()
        {
            PlayerDto dto = new PlayerDto()
            {
                PlayerId = TEST_ID,
                Name = string.Empty,
                UserName = TEST_USERNAME,
                Password = TEST_PASSWORD,
                EmailAddress = TEST_EMAIL,
                ShouldSync = false
            };

            var result = PlayerService.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void NameTooLongTest()
        {
            PlayerDto dto = new PlayerDto()
            {
                PlayerId = TEST_ID,
                Name = "ads;kfj;alsdkjf;lasjdg;lasdhgkjhdlfasgdfljhgsdflkjaghsdkgjhklasjdghjsjhdgkljashdgkajshdglkjahsdkgjhaslkjdghaslkdgjhasljkdgh",
                UserName = TEST_USERNAME,
                Password = TEST_PASSWORD,
                EmailAddress = TEST_EMAIL,
                ShouldSync = false
            };

            var result = PlayerService.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void BlankUserNameTest()
        {
            PlayerDto dto = new PlayerDto()
            {
                PlayerId = TEST_ID,
                Name = TEST_NAME,
                UserName = string.Empty,
                Password = TEST_PASSWORD,
                EmailAddress = TEST_EMAIL,
                ShouldSync = false
            };

            var result = PlayerService.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void UserNameTooLongTest()
        {
            PlayerDto dto = new PlayerDto()
            {
                PlayerId = TEST_ID,
                Name = TEST_NAME,
                UserName = "etuyewrtoiuewyroityweoiurtyoiweutoiweyrtoiweyrtoiulqewytoiqwerytoiqeywoituyqeroiutyqoierutyoiqueytoiqueyrtoiuq",
                Password = TEST_PASSWORD,
                EmailAddress = TEST_EMAIL,
                ShouldSync = false
            };

            var result = PlayerService.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidEmailAddressTest()
        {
            PlayerDto dto = new PlayerDto()
            {
                PlayerId = TEST_ID,
                Name = TEST_NAME,
                UserName = TEST_USERNAME,
                Password = TEST_PASSWORD,
                EmailAddress = "Test",
                ShouldSync = false
            };

            var result = PlayerService.AddNew(dto);
            Assert.IsFalse(result.IsSuccess);
        }
    }
}

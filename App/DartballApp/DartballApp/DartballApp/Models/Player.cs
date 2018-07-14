using System;
using Dartball.BusinessLayer.Player.Interface.Models;

namespace DartballApp.Models
{
    public class Player
    {
        public Player(IPlayer player)
        {
            PlayerId = player.PlayerId;
            Name = player.Name;
            EmailAddress = player.EmailAddress;
            UserName = player.UserName;
            Password = player.Password;
        }

        Guid PlayerId { get; set; }
        string Name { get; set; }
        string EmailAddress { get; set; }
        string UserName { get; set; }
        string Password { get; set; }



    }
}

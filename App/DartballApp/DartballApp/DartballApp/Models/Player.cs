using System;
using System.ComponentModel;
using Dartball.BusinessLayer.Player.Interface.Models;

namespace DartballApp.Models
{
    public class Player
    {
        public Player(){ PlayerId = Guid.NewGuid(); }

        public Player(IPlayer player)
        {
            if (player != null) {

                PlayerId = player.PlayerId;
                FirstName = player.Name;
                LastName = player.LastName;
                EmailAddress = player.EmailAddress;
                UserName = player.UserName;
                Password = player.Password;
            }
                      
        }

        public Guid PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public bool PropertyChanged { get; set; }



        public string DisplayName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

    }
}

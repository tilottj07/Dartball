using System;
using System.Collections.Generic;
using System.Text;

namespace DartballApp.ViewModels
{
    public class PlayerViewModel
    {
        public PlayerViewModel(Models.Player player = null)
        {
            Player = player;
            if (Player == null)
            {
                Player.Name = string.Empty;
                Player.UserName = string.Empty;
                Player.Password = string.Empty;
                Player.EmailAddress = string.Empty;

                IsAddNew = true;
            }
        }

        public Models.Player Player { get; set; }
        public bool IsAddNew { get; set; }

    }
}

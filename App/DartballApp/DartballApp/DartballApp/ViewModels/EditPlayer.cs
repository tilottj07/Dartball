using System;
using Dartball.BusinessLayer.Player.Implementation;
using Dartball.BusinessLayer.Player.Interface;

namespace DartballApp.ViewModels
{
    public class EditPlayer
    {
        IPlayerService Service;

        public EditPlayer()
        {
            Service = new PlayerService();
        }


        public Models.Player Player { get; set; }
        public bool IsAddNew { get; set; }


        public void FillPlayer(Guid? playerId)
        {
            if (playerId.HasValue) {
                Player = new Models.Player(Service.GetPlayer(playerId.Value));
                IsAddNew = false;
            }
            else {
                Player = new Models.Player();
                IsAddNew = true;
            }

        }
    }
}

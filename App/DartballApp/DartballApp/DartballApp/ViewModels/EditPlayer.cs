using System;
using Dartball.BusinessLayer.Player.Dto;
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


        public void FillPlayer(Guid? playerId)
        {
            if (playerId.HasValue) {
                Player = new Models.Player(Service.GetPlayer(playerId.Value));
            }
            else {
                Player = new Models.Player();
            }
        }



        public void SavePlayer(Models.Player player)
        {
            PlayerDto dto = new PlayerDto()
            {
                PlayerId = player.PlayerId,
                EmailAddress = player.EmailAddress,
                Name = player.Name,
                UserName = player.UserName,
                Password = player.Password
            };
            Service.Save(dto);
        }
    }
}

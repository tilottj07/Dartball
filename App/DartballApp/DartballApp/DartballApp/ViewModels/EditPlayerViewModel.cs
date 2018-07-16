using System;
using Dartball.BusinessLayer.Player.Dto;
using Dartball.BusinessLayer.Player.Implementation;
using Dartball.BusinessLayer.Player.Interface;
using Dartball.BusinessLayer.Shared.Models;

namespace DartballApp.ViewModels
{
    public class EditPlayerViewModel
    {
        IPlayerService Service;

        public EditPlayerViewModel()
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



        public ChangeResult SavePlayer()
        {
            PlayerDto dto = new PlayerDto()
            {
                PlayerId = Player.PlayerId,
                EmailAddress = Player.EmailAddress,
                Name = Player.Name,
                UserName = Player.UserName,
                Password = Player.Password
            };
            return Service.Save(dto);
        }
    }
}

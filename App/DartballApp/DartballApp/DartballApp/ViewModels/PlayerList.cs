using System;
using System.Collections.Generic;
using System.Linq;
using Dartball.BusinessLayer.Player.Implementation;
using Dartball.BusinessLayer.Player.Interface;

namespace DartballApp.ViewModels
{
    public class PlayerList
    {
        IPlayerService PlayerService;


        public PlayerList()
        {
            PlayerService = new PlayerService();
        }



        public List<Models.Player> Players { get; set; }




        public void FillPlayers()
        {

            Players = new List<Models.Player>();
            foreach (var item in PlayerService.GetPlayers())
            {
                Players.Add(new Models.Player(item));
            }

        }
    }
}

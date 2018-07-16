using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Dartball.BusinessLayer.Player.Implementation;
using Dartball.BusinessLayer.Player.Interface;

namespace DartballApp.ViewModels
{
    public class PlayerListViewModel 
    {
        IPlayerService Service;


        public PlayerListViewModel()
        {
            Service = new PlayerService();
        }



        public List<Models.Player> Players { get; set; }




        public void FillPlayers()
        {

            Players = new List<Models.Player>();
            foreach (var item in Service.GetPlayers())
            {
                Players.Add(new Models.Player(item));
            }

        }
    }
}

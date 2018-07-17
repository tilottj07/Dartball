using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
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



        public ObservableCollection<Models.Player> Players { get; set; }

       

        public void FillPlayers()
        {
            Players = new ObservableCollection<Models.Player>();
            foreach (var item in Service.GetPlayers().OrderBy(y => y.Name))
            {
                Players.Add(new Models.Player(item));
            }
        }

     

    }
}

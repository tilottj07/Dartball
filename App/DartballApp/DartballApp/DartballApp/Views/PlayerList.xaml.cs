﻿using System;
using System.Collections.Generic;
using DartballApp.ViewModels;
using Xamarin.Forms;

namespace DartballApp.Views
{
    public partial class PlayerList : ContentPage
    {
        
        public List<Models.Player> Items { get; set; }

        public PlayerList()
        {
            var playerListViewModel = new PlayerListViewModel();
            playerListViewModel.FillPlayers();

            MessagingCenter.Subscribe<EditPlayer>(this, "PlayerEdited", (sender) => {
                playerListViewModel.FillPlayers();
                Items = playerListViewModel.Players;
            });

            BindingContext = this;



            Items = playerListViewModel.Players;


           // ToolbarItems.Add(new ToolbarItem("Add", "", async () => { await Navigation.PushAsync( new EditPlayer(playerId: null)); }));

            InitializeComponent();


           
        }

        public void GoToPlayerEdit(object sender, SelectedItemChangedEventArgs args) {

            var player = args.SelectedItem as Models.Player;

            Guid? playerId = null;
            if (player != null) playerId = player.PlayerId;

            Navigation.PushModalAsync(new EditPlayer(playerId));

        }
    }
}

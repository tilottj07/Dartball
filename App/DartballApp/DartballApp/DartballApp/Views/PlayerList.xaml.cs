using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DartballApp.ViewModels;
using Xamarin.Forms;

namespace DartballApp.Views
{
    public partial class PlayerList : ContentPage
    {
        
        public PlayerListViewModel ViewModel { get; set; }

        public PlayerList()
        {
            MessagingCenter.Subscribe<EditPlayer>(this, "PlayerEdited", (sender) => {
                RefreshPage();
            });

            ViewModel = new PlayerListViewModel();
            ViewModel.FillPlayers();

            BindingContext = this;


            InitializeComponent();
        }

        public void GoToPlayerEdit(object sender, SelectedItemChangedEventArgs args) {

            var player = args.SelectedItem as Models.Player;

            Guid? playerId = null;
            if (player != null) playerId = player.PlayerId;

            Navigation.PushModalAsync(new EditPlayer(playerId));

        }


        void RefreshPage() {
            BindingContext = null;
            ViewModel.FillPlayers();

            BindingContext = this;
        }

       
    }
}

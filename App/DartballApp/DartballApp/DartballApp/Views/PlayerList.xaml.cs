using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DartballApp.Views
{
    public partial class PlayerList : ContentPage
    {
        
        public ViewModels.PlayerList PlayersModel;

        public PlayerList()
        {
            PlayersModel = new ViewModels.PlayerList();
            PlayersModel.FillPlayers();

            BindingContext = PlayersModel;

            InitializeComponent();
        }

        public void GoToPlayerEdit(object sender, SelectedItemChangedEventArgs args) {

            var player = args.SelectedItem as Models.Player;

            Guid? playerId = null;
            if (player != null) playerId = player.PlayerId;

            Navigation.PushAsync(new NavigationPage(new EditPlayer(playerId)));

        }
    }
}

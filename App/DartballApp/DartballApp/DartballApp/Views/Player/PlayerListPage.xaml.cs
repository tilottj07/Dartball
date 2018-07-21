using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DartballApp.ViewModels.Player;
using Xamarin.Forms;

namespace DartballApp.Views.Player
{
    public partial class PlayerListPage : ContentPage
    {
        
        public PlayerListViewModel ViewModel { get; set; }

        public PlayerListPage()
        {
            MessagingCenter.Subscribe<EditPlayerPage>(this, "PlayerEdited", (sender) => {
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

            Navigation.PushModalAsync(new EditPlayerPage(playerId, leagueId: null));

        }


        void RefreshPage() {
            BindingContext = null;
            ViewModel.FillPlayers();

            BindingContext = this;
        }

       
    }
}

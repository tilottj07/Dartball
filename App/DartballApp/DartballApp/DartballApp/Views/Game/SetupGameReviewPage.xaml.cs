using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DartballApp.Views.Game
{
    public partial class SetupGameReviewPage : ContentPage
    {
        public ViewModels.Game.SetupGameReviewViewModel ViewModel { get; set; }

        public SetupGameReviewPage(Guid gameId)
        {
            InitializeComponent();

            ViewModel = new ViewModels.Game.SetupGameReviewViewModel(gameId);
            ViewModel.FillInformation();

            BindingContext = this;



            int counter = 0;
            foreach (var item in ViewModel.AwayTeamLineup)
            {
                AwayTeamContainer.Children.Add(new Label { Text = $"{++counter}. {item.DisplayName}" });
            }

            counter = 0;
            foreach (var item in ViewModel.HomeTeamLineup)
            {
                HomeTeamContainer.Children.Add(new Label { Text = $"{++counter}. {item.DisplayName}" });
            }

           
        }


        public async void PlayGame(object sender, EventArgs args) {
            bool answer = await DisplayAlert("Start Game?", "Are you sure you want to begin the game?  Once the game has started the " +
                                             "lineups can no longer be altered.", "Yes", "No");

            if (answer == true) NavigateToPlayGame();
        }

        public void Cancel(object sender, EventArgs args)  {
            Navigation.PopModalAsync();
        }

        void NavigateToPlayGame() {
            DisplayAlert("Hello Bucko", "Coming Soon...", "OK");
        }
    }
}

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
            ViewModel = new ViewModels.Game.SetupGameReviewViewModel(gameId);
            ViewModel.FillInformation();

            BindingContext = this;

            InitializeComponent();
        }


        public void PlayGame(object sender, EventArgs args) {
            DisplayAlert("Hello Bucko", "Coming Soon...", "OK");
        }

        public void Cancel(object sender, EventArgs args)  {
            Navigation.PopModalAsync();
        }

    }
}

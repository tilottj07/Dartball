using System;
using System.Collections.Generic;

using Xamarin.Forms;
using static Dartball.BusinessLayer.Game.Implementation.GameEventService;

namespace DartballApp.Views.Game
{
    public partial class PlayGamePage : ContentPage
    {
        public ViewModels.Game.PlayGameViewModel ViewModel { get; set; }

        public PlayGamePage(Guid gameId)
        {
            InitializeComponent();
            
            ViewModel = new ViewModels.Game.PlayGameViewModel(gameId);
            ViewModel.InitializeGame();
            ViewModel.FillCurrentInning();
            ViewModel.FillCurrentAtBatTeam();
            ViewModel.FillCurrentAtBatTeamPlayer();
            ViewModel.FillBoxScore();

            BindingContext = this;
        }


        public void SaveBatterEvent(object sender, EventArgs args) {
            BindingContext = null;

            var button = sender as Button;
            EventType eventType = (EventType)int.Parse(button.CommandParameter.ToString());

            ViewModel.SaveEventType(eventType);
            ViewModel.FillBoxScore();

            BindingContext = this;
        }
    }
}

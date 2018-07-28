using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace DartballApp.Views.Team
{
    public partial class EditTeamPlayersPage : ContentPage
    {
        public ViewModels.Team.EditTeamPlayersViewModel ViewModel { get; set; }

        public EditTeamPlayersPage(Guid teamId)
        {
           

            ViewModel = new ViewModels.Team.EditTeamPlayersViewModel(teamId);
            ViewModel.FillTeamInfo();
            ViewModel.FillPlayers();

            MessagingCenter.Subscribe<SelectTeamPlayerPage>(this, "PlayerAddedToTeam", (sender) =>
            {
                RefreshTeamPlayers();
            });

            BindingContext = this;

            InitializeComponent();
        }


        public void ChooseTeamPlayerToAdd(object sender, EventArgs args) {
            Navigation.PushModalAsync(new SelectTeamPlayerPage(ViewModel.TeamId));
        }



        void RefreshTeamPlayers()
        {
            BindingContext = null;

            ViewModel.FillPlayers();
            BindingContext = this;
        }


        public void GoBackToTeamsList(object sender, EventArgs args) {
            Navigation.PopModalAsync();
        }

        public async void RemovePlayerFromTeam(object sender, EventArgs args) {
            var button = sender as Button;
            Guid playerId = Guid.Parse(button.CommandParameter.ToString());

            string playerName = "this player";
            var playerInfo = ViewModel.TeamPlayers.FirstOrDefault(y => y.PlayerId == playerId);
            if (playerInfo != null) playerName = playerInfo.DisplayName;

            bool answer = await 
                DisplayAlert("Are you sure?", $"Are you sure you'd like to remove {playerName} from {ViewModel.TeamName}?", "Yes", "No");
            if (answer == true) RemovePlayer(playerId);

        }

        void RemovePlayer(Guid playerId) {
            var result = ViewModel.RemoveTeamPlayer(playerId);
            RefreshTeamPlayers();
        }
    }
}

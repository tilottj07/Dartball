using System;
using System.Text;
using Dartball.BusinessLayer.Shared.Models;
using Xamarin.Forms;

namespace DartballApp.Views.Team
{
    public partial class TeamPlayerLineupPage : ContentPage
    {
        public ViewModels.Team.TeamPlayerLineupViewModel ViewModel { get; set; }

        public TeamPlayerLineupPage(Guid teamId)
        {
            ViewModel = new ViewModels.Team.TeamPlayerLineupViewModel(teamId);
            ViewModel.FillBatters();
            ViewModel.FillTeamInfo();

            MessagingCenter.Subscribe<SelectTeamPlayerPage>(this, "PlayerAddedToTeam", (sender) => { RefreshSavePlayers(); });
            MessagingCenter.Subscribe<AddTeamLineupPlayerPage>(this, "PlayerAddedToLineup", (sender) => { RefreshSavePlayers(); });

            BindingContext = this;
            InitializeComponent();
        }



        public void AddTeamPlayerToLineup(object sender, EventArgs args) {
            Navigation.PushModalAsync(new AddTeamLineupPlayerPage(ViewModel.TeamId));
        }



        public void Done(object sender, EventArgs args) {

            if (ViewModel.HasChanges)
            {
                var result = ViewModel.SaveLineup();
                if (!result.IsSuccess) DisplayAlert(result);
            }
            Navigation.PopModalAsync();
        }

        private void RefreshSavePlayers() {
            BindingContext = null;
            ViewModel.FillBatters();

            var result = ViewModel.SaveLineup();
            if (!result.IsSuccess) DisplayAlert(result);

            BindingContext = this;
        }

        private void RefreshPlayers()
        {
            BindingContext = null;
            ViewModel.FillBatters();
            BindingContext = this;
        }
         
        public void PlayerUp(object sender, EventArgs args) {
            var button = sender as Button;
            Guid playerId = Guid.Parse(button.CommandParameter.ToString());

            ViewModel.MovePlayerUpInLineup(playerId);

            var result = ViewModel.SaveLineup();
            if (!result.IsSuccess) DisplayAlert(result);

            RefreshPlayers();
        }

        public void PlayerDown(object sender, EventArgs args) {
            var button = sender as Button;
            Guid playerId = Guid.Parse(button.CommandParameter.ToString());

            ViewModel.MovePlayerDownInLineup(playerId);
           
            var result = ViewModel.SaveLineup();
            if (!result.IsSuccess) DisplayAlert(result);

            RefreshPlayers();
        }

        public void PlayerRemove(object sender, EventArgs args) {
            var button = sender as Button;
            Guid playerId = Guid.Parse(button.CommandParameter.ToString());

            ViewModel.RemovePlayerFromLineup(playerId);

            var result = ViewModel.SaveLineup();
            if (!result.IsSuccess) DisplayAlert(result);

            RefreshPlayers();
        }



        private void DisplayAlert(ChangeResult result) {
            if (!result.IsSuccess)
            {
                StringBuilder sb = new StringBuilder();
                int index = 0;

                foreach (var item in result.ErrorMessages)
                {
                    if (index > 0) sb.Append(" ");
                    sb.Append(item);
                    index++;
                }
                DisplayAlert("Alert", sb.ToString(), "OK");
            }
        }

    }
}

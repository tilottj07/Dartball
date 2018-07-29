using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dartball.BusinessLayer.Shared.Models;
using Xamarin.Forms;

namespace DartballApp.Views.Game
{
    public partial class SetupGamePage : ContentPage
    {
        public ViewModels.Game.SetupGameViewModel ViewModel { get; set; }

        public SetupGamePage(Guid leagueId)
        {
            ViewModel = new ViewModels.Game.SetupGameViewModel(leagueId);
            ViewModel.FillTeams();

            BindingContext = this;

            InitializeComponent();
        }

        public void SaveTeams(object sender, EventArgs args) {

            ChangeResult result = new ChangeResult();

            Models.Team awayTeam = null;
            Models.Team homeTeam = null;

            if (AwayTeamPicker.SelectedIndex >= 0)
            {
                awayTeam = ViewModel.AwayTeams[AwayTeamPicker.SelectedIndex];
            }
            if (HomeTeamPicker.SelectedIndex >= 0)
            {
                homeTeam = ViewModel.HomeTeams[HomeTeamPicker.SelectedIndex];
            }

            if (awayTeam == null) {
                result.IsSuccess = false;
                result.ErrorMessages.Add("Please select an away team.");
            }
            if (homeTeam == null) {
                result.IsSuccess = false;
                result.ErrorMessages.Add("Please select a home team.");
            }

            if (result.IsSuccess && awayTeam.TeamId == homeTeam.TeamId) {
                result.IsSuccess = false;
                result.ErrorMessages.Add("Please select two different teams.");
            }

            if (result.IsSuccess) {
                result = ViewModel.SaveGameTeams(awayTeam, homeTeam);
            }

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
            else
            {
               //navigate to game review page
            }
        }

        public void Cancel(object sender, EventArgs args) {
            Navigation.PopModalAsync();
        }


        public void SetAwayTeamLineup(object sender, EventArgs args)
        {
            if (AwayTeamPicker.SelectedIndex >= 0)
            {
                Navigation.PushAsync(new Team.TeamPlayerLineupPage(ViewModel.AwayTeams[AwayTeamPicker.SelectedIndex].TeamId));
            }
            else
            {
                DisplayAlert("Alert", "Please select an away team.", "OK");
            }
        }

        public void SetHomeTeamLineup(object sender, EventArgs args)
        {

            if (HomeTeamPicker.SelectedIndex >= 0)
            {
                Navigation.PushAsync(new Team.TeamPlayerLineupPage(ViewModel.HomeTeams[HomeTeamPicker.SelectedIndex].TeamId));
            }
            else
            {
                DisplayAlert("Alert", "Please select a home team.", "OK");
            }
        }
       

       
    }
}

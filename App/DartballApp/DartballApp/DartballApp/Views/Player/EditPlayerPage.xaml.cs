using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dartball.BusinessLayer.Shared.Models;
using Xamarin.Forms;

namespace DartballApp.Views.Player
{
    public partial class EditPlayerPage : ContentPage
    {
        ViewModels.Player.EditPlayerViewModel EditPlayerViewModel;


        public EditPlayerPage(Guid? playerId, Guid? leagueId)
        {
            EditPlayerViewModel = new ViewModels.Player.EditPlayerViewModel(playerId, leagueId);
            EditPlayerViewModel.FillPlayer();
            EditPlayerViewModel.FillTeams();
                      
            BindingContext = EditPlayerViewModel;


            InitializeComponent();
        }


        public void Save(object sender, EventArgs args) {
            var result = EditPlayerViewModel.SavePlayer();

            if (!result.IsSuccess) {
                DisplayErrors(result);
            }
            else {
                
                if (PlayerTeamPicker.SelectedIndex >= 0) {
                    result = EditPlayerViewModel.SavePlayerTeam(EditPlayerViewModel.Teams[PlayerTeamPicker.SelectedIndex].TeamId);
                }

                MessagingCenter.Send<EditPlayerPage>(this, "PlayerEdited");
                Navigation.PopModalAsync(animated: true);
            }
        }

        public void DisplayErrors(ChangeResult result) {
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

        public void Cancel(object sender, EventArgs args) {
            Navigation.PopModalAsync(animated: true);
        }


    }
}

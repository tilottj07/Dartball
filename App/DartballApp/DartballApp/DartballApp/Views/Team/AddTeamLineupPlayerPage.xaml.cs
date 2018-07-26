using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DartballApp.Views.Team
{
    public partial class AddTeamLineupPlayerPage : ContentPage
    {
        public ViewModels.Team.AddTeamLineupPlayer ViewModel { get; set; }

        public AddTeamLineupPlayerPage(Guid teamId)
        {
            ViewModel = new ViewModels.Team.AddTeamLineupPlayer(teamId);
            ViewModel.FillPlayersToAddToLineup();

            BindingContext = this;

            InitializeComponent();
        }


        public void AddPlayerToTeamLineup(object sender, SelectedItemChangedEventArgs args)
        {
            Models.Player player = args.SelectedItem as Models.Player;
            if (player != null)
            {
                var result = ViewModel.AddPlayerToLineup(player.PlayerId);
                if (!result.IsSuccess) {

                    StringBuilder sb = new StringBuilder();
                    int index = 0;

                    foreach(var item in result.ErrorMessages) {
                        if (index > 0) sb.Append(" ");
                        sb.Append(item);
                        index++;
                    }
                    DisplayAlert("Alert", sb.ToString(), "OK");
                }
            }
            Navigation.PopModalAsync();
        }

        public void Cancel(object sender, EventArgs args) {
            Navigation.PopModalAsync();
        }
    }
}

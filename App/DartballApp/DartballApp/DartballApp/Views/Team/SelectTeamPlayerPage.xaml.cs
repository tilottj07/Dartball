using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DartballApp.Views.Team
{
    public partial class SelectTeamPlayerPage : ContentPage
    {
        public ViewModels.Team.SelectTeamPlayerViewModel ViewModel { get; set; }

        public SelectTeamPlayerPage(Guid teamId)
        {
            ViewModel = new ViewModels.Team.SelectTeamPlayerViewModel(teamId);
            ViewModel.FillAvailablePlayers();

            BindingContext = this;

            InitializeComponent();
        }


        public void SelectedTeamPlayer(object sender, SelectedItemChangedEventArgs args) {

            Models.Player player = args.SelectedItem as Models.Player;
            if (player != null) {
                var result = ViewModel.AddPlayerToTeam(player);
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

                MessagingCenter.Send(this, "PlayerAddedToTeam");
            }

            Navigation.PopModalAsync();
        }
    }
}

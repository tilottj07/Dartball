using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DartballApp.Views
{
    public partial class EditTeamPlayersPage : ContentPage
    {
        public ViewModels.EditTeamPlayersViewModel ViewModel;

        public EditTeamPlayersPage(Guid teamId)
        {
            ViewModel = new ViewModels.EditTeamPlayersViewModel(teamId);
            ViewModel.FillTeamInfo();
            ViewModel.FillPlayers();

            BindingContext = this;

            InitializeComponent();
        }

        public void AddSelectedPlayer(object sender, SelectedItemChangedEventArgs args)
        {
            Models.Player player = args.SelectedItem as Models.Player;
            if (player != null)
            {
                var result = ViewModel.AddTeamPlayer(player.PlayerId);
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
                else {
                    RefreshTeamPlayers();
                }
            }
        }



        private void RefreshTeamPlayers() {
            BindingContext = null;

            ViewModel.FillTeamPlayers();
            BindingContext = this;
        }
    }
}

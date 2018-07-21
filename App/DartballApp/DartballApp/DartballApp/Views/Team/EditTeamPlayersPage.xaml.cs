using System;
using System.Collections.Generic;
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

        //public void RemovePlayerFromTeam(object sender, selecte)
    }
}

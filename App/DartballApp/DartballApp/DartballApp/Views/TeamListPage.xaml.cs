using System;
using System.Collections.Generic;
using DartballApp.ViewModels;
using Xamarin.Forms;

namespace DartballApp.Views
{
    public partial class TeamListPage : ContentPage
    {
        public TeamListViewModel ViewModel;

        public TeamListPage()
        {

            MessagingCenter.Subscribe<EditTeamPage>(this, "TeamEdited", (sender) => {
                RefreshPage();
            });

            ViewModel = new TeamListViewModel();
            ViewModel.FillTeams();

            BindingContext = this;

            InitializeComponent();
        }


        public void GoToTeamEdit(object sender, SelectedItemChangedEventArgs args) {
            var team = args.SelectedItem as Models.Team;

            Guid? teamId = null;
            if (team != null) teamId = team.TeamId;

            Navigation.PushModalAsync(new EditTeamPage(teamId));
        }

        void RefreshPage()
        {
            BindingContext = null;
            ViewModel.FillTeams();

            BindingContext = this;
        }

    }
}

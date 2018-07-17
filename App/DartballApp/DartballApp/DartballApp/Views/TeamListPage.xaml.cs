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

            MessagingCenter.Subscribe<EditPlayerPage>(this, "PlayerEdited", (sender) => {
                RefreshPage();
            });

            ViewModel = new TeamListViewModel();
            ViewModel.FillTeams();

            BindingContext = this;

            InitializeComponent();
        }


        public void GoToTeamEdit(object sender, SelectedItemChangedEventArgs args) {
            DisplayAlert("Hi", "Comming Soon :)", "OK");
        }

        void RefreshPage()
        {
            BindingContext = null;
            ViewModel.FillTeams();

            BindingContext = this;
        }

    }
}

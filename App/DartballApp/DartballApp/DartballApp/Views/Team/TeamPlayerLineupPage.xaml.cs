using System;
using System.Collections.Generic;
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

            BindingContext = this;
            InitializeComponent();
        }



        public void AddTeamPlayerToLineup(object sender, EventArgs args) {
            Navigation.PushModalAsync(new AddTeamLineupPlayerPage(ViewModel.TeamId));
        }



        public void Done(object sender, EventArgs args) {

            if (ViewModel.HasChanges)
            {
                //TODO: make this save async
                var result = ViewModel.SaveLineup();
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
            Navigation.PopModalAsync();
        }


         


    }
}

using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DartballApp.Views.Team
{
    public partial class TeamTabbedPage : TabbedPage
    {
        public TeamTabbedPage(Guid? teamId)
        {
           

            if (teamId.HasValue)
            {
                NavigationPage editTeamPlayers = new NavigationPage(new EditTeamPlayersPage(teamId.Value)) { Title = "Team Players" };
                Children.Add(editTeamPlayers);
            }

            NavigationPage editTeam = new NavigationPage(new EditTeamPage(teamId)) { Title = "Edit Team" };
            Children.Add(editTeam);


            InitializeComponent();
        }
    }
}

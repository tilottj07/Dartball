using System;
using System.Collections.ObjectModel;
using System.Linq;
using Dartball.BusinessLayer.Team.Implementation;
using Dartball.BusinessLayer.Team.Interface;

namespace DartballApp.ViewModels.Team
{
    public class TeamListViewModel
    {
        ITeamService Service;

        public TeamListViewModel()
        {
            Service = new TeamService();
        }


        public ObservableCollection<Models.Team> Teams { get; set; }



        public void FillTeams() {
            Teams = new ObservableCollection<Models.Team>();

            foreach(var item in Service.GetTeams().OrderBy(x => x.Name)) 
            {
                Teams.Add(new Models.Team(item));
            }
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.Linq;
using Dartball.BusinessLayer.Team.Implementation;
using Dartball.BusinessLayer.Team.Interface;
using DartballApp.Models;

namespace DartballApp.ViewModels
{
    public class TeamListViewModel
    {
        ITeamService Service;

        public TeamListViewModel()
        {
            Service = new TeamService();
        }


        public ObservableCollection<Team> Teams { get; set; }



        public void FillTeams() {
            Teams = new ObservableCollection<Team>();
            foreach(var item in Service.GetTeams().OrderBy(x => x.Name)) {
                Teams.Add(new Team(item));
            }
        }
    }
}

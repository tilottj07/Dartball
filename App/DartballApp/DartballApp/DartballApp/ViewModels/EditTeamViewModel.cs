using System;
using Dartball.BusinessLayer.League.Implementation;
using Dartball.BusinessLayer.League.Interface;
using Dartball.BusinessLayer.Shared.Models;
using Dartball.BusinessLayer.Team.Dto;
using Dartball.BusinessLayer.Team.Implementation;
using Dartball.BusinessLayer.Team.Interface;

namespace DartballApp.ViewModels
{
    public class EditTeamViewModel
    {
        ITeamService Service;

        public EditTeamViewModel()
        {
            Service = new TeamService();
        }

        public Models.Team Team { get; set; }



        public void FillTeam(Guid? teamId)
        {
            if (teamId.HasValue) Team = new Models.Team(Service.GetTeam(teamId.Value));
            else
            {
                //TODO: build in option to choose which league the team belongs to
                ILeagueService league = new LeagueService();

                Guid leagueId;
                var genericLeague = league.GetGenericLeague();
                if (genericLeague != null) leagueId = genericLeague.LeagueId;

                Team = new Models.Team(leagueId);
            }
        }

        public ChangeResult SaveTeam()
        {
            return Service.Save(new TeamDto()
            {
                TeamId = Team.TeamId,
                LeagueId = Team.LeagueId,
                Name = Team.Name,
                Password = Team.Password
            });
        }
    }
}

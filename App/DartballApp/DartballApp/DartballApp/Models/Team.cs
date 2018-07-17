using System;
using Dartball.BusinessLayer.Team.Interface.Models;

namespace DartballApp.Models
{
    public class Team
    {
        public Team() {}

        public Team(ITeam team) {
            if (team != null) {
                TeamId = team.TeamId;
                LeagueId = team.LeagueId;
                Name = team.Name;
                Password = team.Password;
            }
        }

        public Guid TeamId { get; set; }
        public Guid LeagueId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

    }
}

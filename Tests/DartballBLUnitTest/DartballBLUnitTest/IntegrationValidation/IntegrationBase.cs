using Dartball.BusinessLayer.League.Dto;
using Dartball.BusinessLayer.League.Implementation;
using Dartball.BusinessLayer.League.Interface;
using Dartball.BusinessLayer.Team.Dto;
using Dartball.BusinessLayer.Team.Implementation;
using Dartball.BusinessLayer.Team.Interface;
using System;

namespace DartballBLUnitTest.IntegrationValidation
{
    public class IntegrationBase
    {
        private ILeagueService League;
        private ITeamService Team;

        public IntegrationBase()
        {
            League = new LeagueService();
            Team = new TeamService();
        }


        public Guid SeedLeague()
        {
            Guid id = Guid.NewGuid();

            LeagueDto dto = new LeagueDto()
            {
                LeagueId = id,
                Name = "Seed League",
                Password = "1234"
            };

            League.AddNew(dto);

            return id;
        }
        public void DeleteSeededLeague(Guid leagueId)
        {
            League.RemoveLeague(leagueId);
        }


        public Guid SeedTeam()
        {
            Guid id = Guid.NewGuid();
            Guid leagueId = SeedLeague();

            TeamDto dto = new TeamDto()
            {
                TeamId = id,
                LeagueId = leagueId,
                Handicap = 3,
                Name = "Bombers",
                Password = "KaBoom",
                ShouldSync = true
            };

            Team.AddNew(dto);

            return id;
        }
        public void DeleteSeededTeam(Guid teamId)
        {
            Team.Remove(teamId);
        }
    }
}

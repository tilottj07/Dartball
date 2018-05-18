using Dartball.DataLayer.Device.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.DataLayer.Device.Repository
{
    public class TeamRepository : ConnectionBase
    {

        public List<TeamDto> LoadAll()
        {
            List<TeamDto> teams = new List<TeamDto>();

            Connection.Open();
            //teams.AddRange()
            Connection.Close();

            return teams;
        }

        public List<TeamDto> LoadByLeague(Guid leagueAlternateKey)
        {
            List<TeamDto> teams = new List<TeamDto>();


            return teams;
        }




        private const string SELECT_QUERY = 
        @"select TeamId, TeamAlternateKey, LeagueAlternateKey, Name, Password, Handicap, ShouldSync, ChangeDate, DeleteDate 
        from Team ";

    }
}

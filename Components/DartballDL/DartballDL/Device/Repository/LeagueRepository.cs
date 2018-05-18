using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Dartball.DataLayer.Device.Dto;

namespace Dartball.DataLayer.Device.Repository
{
    public class LeagueRepository : ConnectionBase
    {



        public List<LeagueDto> LoadAll()
        {
            List<LeagueDto> leagues = new List<LeagueDto>();
            Connection.Open();

            leagues.AddRange(Connection.Query<LeagueDto>(SELECT_QUERY));

            Connection.Close();

            return leagues;
        }

        public LeagueDto LoadByKey(Guid leagueAlternateKey)
        {
            LeagueDto league = null;
            Connection.Open();

            var result = Connection.Query<LeagueDto>(
                SELECT_QUERY + " where LeagueAlternateKey = @LeagueAlternateKey",
                new { LeagueAlternateKey = leagueAlternateKey.ToString() });

            league = result.FirstOrDefault();

            Connection.Close();

            return league;
        }



        public void AddNew(LeagueDto league)
        {
            InsertLeague(league);
        }
        public void Update(LeagueDto league)
        {
            UpdateLeague(league);
        }

        /// <summary>
        /// data layer will determine add vs update
        /// </summary>
        /// <param name="league"></param>
        public void Save(LeagueDto league)
        {
            if (ExistsInDb(league)) Update(league);
            else AddNew(league);
        }


        private void InsertLeague(LeagueDto league)
        {
            string insertQuery = @"INSERT INTO League
                    (LeagueAlternateKey, Name, Password, DeleteDate)
                    values(@LeagueAlternateKey, @Name, @Password, @DeleteDate)";

            Connection.Open();
            Connection.Query(insertQuery, league);
            Connection.Close();
        }
        private void UpdateLeague(LeagueDto league)
        {
            string updateQuery = @"update League
            set DeleteDate = @DeleteDate,
            Name = @Name,
            Password = @Password
            where LeagueAlternateKey = @LeagueAlternateKey";

            Connection.Open();
            Connection.Query(updateQuery, league);
            Connection.Close();
        }


        public void Delete(Guid leagueAlternateKey)
        {
            string deleteQuery = @"delete from League where LeagueAlternateKey = @LeagueAlternateKey";

            Connection.Open();
            Connection.Query(deleteQuery, new { LeagueAlternateKey = leagueAlternateKey.ToString() });
            Connection.Close();
        }


        public bool ExistsInDb(LeagueDto league)
        {
            Connection.Open();

            var rows = Connection.Query<int>(@"SELECT COUNT(1) as 'Count' FROM League WHERE LeagueAlternateKey = @LeagueAlternateKey", new { LeagueAlternateKey = league.LeagueAlternateKey });
            Connection.Close();

            return rows.First() > 0;
        }


      

        private const string SELECT_QUERY = @"select LeagueId, LeagueAlternateKey, Name, Password, ChangeDate, DeleteDate from League";


    }
}

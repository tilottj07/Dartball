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

        public LeagueDto LoadByName(string name)
        {
            LeagueDto league = null;
            Connection.Open();

            var result = Connection.Query<LeagueDto>(
                SELECT_QUERY + " where Name = @Name",
                new { Name = name });

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
                    (Name, Password, ChangeDate, DeleteDate)
                    values(@Name, @Password, @DeleteDate)";

            Connection.Open();
            Connection.Query(insertQuery, league);
            Connection.Close();
        }
        private void UpdateLeague(LeagueDto league)
        {
            string updateQuery = @"update League
            set DeleteDate = @DeleteDate,
            Password = @Password
            where Name = @Name";

            Connection.Open();
            Connection.Query(updateQuery, league);
            Connection.Close();
        }


        public void Delete(string name)
        {
            string deleteQuery = @"delete from League where Name = @Name";

            Connection.Open();
            Connection.Query(deleteQuery, new { Name = name });
            Connection.Close();
        }


        public bool ExistsInDb(LeagueDto league)
        {
            Connection.Open();

            var rows = Connection.Query<int>(@"SELECT COUNT(1) as 'Count' FROM League WHERE Name = @Name", new { league });
            Connection.Close();

            return rows.First() > 0;
        }


      

        private const string SELECT_QUERY = @"select LeagueId, Name, Password, ChangeDate, DeleteDate from League";


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Dartball.DataLayer.Device.Interface;

namespace Dartball.DataLayer.Device.Repository
{
    public class LeagueRepository : ConnectionBase, ILeagueRepository
    {



        public List<League> LoadAll()
        {
            List<League> leagues = new List<League>();
            using (var cnn = Connection)
            {
                leagues.AddRange(cnn.Query<League>(SELECT_QUERY));
            }

            return leagues;
        }

        public League LoadByName(string name)
        {
            League league = null;
            using (var cnn = Connection)
            {
                var result = cnn.Query<League>(
                    SELECT_QUERY + " where Name = @Name",
                    new { Name = name });

                league = result.FirstOrDefault();
            }

            return league;
        }



        public void AddNew(League league)
        {
            InsertLeague(league);
        }
        public void Update(League league)
        {
            UpdateLeague(league);
        }

        /// <summary>
        /// data layer will determine add vs update
        /// </summary>
        /// <param name="league"></param>
        public void Save(League league)
        {
            if (ExistsInDb(league)) Update(league);
            else AddNew(league);
        }


        private void InsertLeague(League league)
        {
            string insertQuery = @"INSERT INTO League
                    (Name, Password, ChangeDate, DeleteDate)
                    values(@Name, @Password, @DeleteDate)";

            Connection.Execute(insertQuery, new
            {
                league.Name,
                league.Password,
                league.DeleteDate
            });
        }
        private void UpdateLeague(League league)
        {
            string updateQuery = @"update League
            set DeleteDate = @DeleteDate,
            Password = @Password
            where Name = @Name";

            Connection.Execute(updateQuery, new
            {
                league.DeleteDate,
                league.Password,
                league.Name
            });
        }



        public bool ExistsInDb(League league)
        {
            var rows = Connection.Query<int>(@"SELECT COUNT(1) as 'Count' FROM League WHERE Name = @Name", new { league.Name });
            return rows.First() > 0;
        }


        public class League
        {
            public int LeagueId { get; set; }
            public string Name { get; set; }
            public string Password { get; set; }
            public DateTime ChangeDate { get; set; }
            public DateTime? DeleteDate { get; set; }
        }


        private const string SELECT_QUERY = @"select LeagueId, Name, Password, ChangeDate, DeleteDate from League";


    }
}

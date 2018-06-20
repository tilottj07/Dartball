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
            Connection.BeginTransaction();

            leagues.AddRange(Connection.Query<LeagueDto>(SELECT_QUERY));

            Connection.Commit();

            return leagues;
        }

        public LeagueDto LoadByKey(Guid leagueId)
        {
            LeagueDto league = null;
            Connection.BeginTransaction();

            var result = Connection.Query<LeagueDto>(
                SELECT_QUERY + " where LeagueId = @LeagueId",
                new { LeagueId = leagueId.ToString() });

            league = result.FirstOrDefault();

            Connection.Commit();

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
                    (LeagueId, Name, Password, DeleteDate)
                    values(@LeagueId, @Name, @Password, @DeleteDate)";

            Connection.BeginTransaction();
            Connection.Execute(insertQuery, league);
            Connection.Commit();
        }
        private void UpdateLeague(LeagueDto league)
        {
            string updateQuery = @"update League
            set DeleteDate = @DeleteDate,
            Name = @Name,
            Password = @Password
            where LeagueId = @LeagueId";

            Connection.BeginTransaction();
            Connection.Execute(updateQuery, league);
            Connection.Commit();
        }


        public void Delete(Guid leagueId)
        {
            string deleteQuery = @"delete from League where LeagueId = @LeagueId";

            Connection.BeginTransaction();
            Connection.Execute(deleteQuery, new { LeagueId = leagueId.ToString() });
            Connection.Commit();
        }


        public bool ExistsInDb(LeagueDto league)
        {
            Connection.BeginTransaction();

            var rows = Connection.Query<int>(@"SELECT COUNT(1) as 'Count' FROM League WHERE LeagueId = @LeagueId", new { LeagueId = league.LeagueId });
            Connection.Commit();

            return rows.First() > 0;
        }


      

        private const string SELECT_QUERY = @"select LeagueId, LeagueId, Name, Password, ChangeDate, DeleteDate from League";


    }
}

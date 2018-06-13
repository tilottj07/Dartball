using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using SQLite;
using Dartball.DataLayer.Device.Dto;
using System.Linq;

namespace Dartball.DataLayer.Device.Repository
{
    public class TeamPlayerLineupRepository : ConnectionBase
    {

        public List<TeamPlayerLineupDto> LoadAll()
        {
            List<TeamPlayerLineupDto> teamPlayerLineups = new List<TeamPlayerLineupDto>();

            Connection.BeginTransaction();
            teamPlayerLineups.AddRange(Connection.Query<TeamPlayerLineupDto>(SELECT_QUERY));
            Connection.Commit();

            return teamPlayerLineups;
        }

        public TeamPlayerLineupDto LoadByCompositeKey(Guid playerAlternateKey, Guid teamAlternateKey)
        {
            TeamPlayerLineupDto teamPlayerLineup = null;
            Connection.BeginTransaction();

            var result = Connection.Query<TeamPlayerLineupDto>(
                SELECT_QUERY + " WHERE PlayerAlternateKey = @PlayerAlternateKey " +
                "AND TeamAlternateKey = @TeamAlternateKey",
                new { PlayerAlternateKey = playerAlternateKey.ToString(), TeamAlternateKey = teamAlternateKey.ToString() });

            teamPlayerLineup = result.FirstOrDefault();

            Connection.Commit();

            return teamPlayerLineup;
        }

        public List<TeamPlayerLineupDto> LoadByTeamAlternateKey(Guid teamAlternateKey)
        {
            List<TeamPlayerLineupDto> teamPlayerLineups = new List<TeamPlayerLineupDto>();

            Connection.BeginTransaction();
            teamPlayerLineups.AddRange(Connection.Query<TeamPlayerLineupDto>(
                SELECT_QUERY + " WHERE TeamAlternateKey = @TeamAlternateKey ",
                new { TeamAlternateKey = teamAlternateKey.ToString() }));
            Connection.Commit();

            return teamPlayerLineups;
        }

        public List<TeamPlayerLineupDto> LoadByPlayerAlternateKey(Guid playerAlternateKey)
        {
            List<TeamPlayerLineupDto> teamPlayerLineups = new List<TeamPlayerLineupDto>();

            Connection.BeginTransaction();
            var result = Connection.Query<TeamPlayerLineupDto>(
                SELECT_QUERY + " WHERE PlayerAlternateKey = @PlayerAlternateKey ",
                new { PlayerAlternateKey = playerAlternateKey.ToString() });
            Connection.Commit();

            return teamPlayerLineups;
        }


        public void AddNew(TeamPlayerLineupDto teamPlayerLineup)
        {
            InsertTeamPlayerLineup(teamPlayerLineup);
        }
        public void Update(TeamPlayerLineupDto teamPlayerLineup)
        {
            UpdateTeamPlayerLineup(teamPlayerLineup);
        }

        /// <summary>
        /// data layer will determine add vs update
        /// </summary>
        /// <param name="league"></param>
        public void Save(TeamPlayerLineupDto teamPlayerLineup)
        {
            if (ExistsInDb(teamPlayerLineup)) Update(teamPlayerLineup);
            else AddNew(teamPlayerLineup);
        }


        private void InsertTeamPlayerLineup(TeamPlayerLineupDto teamPlayerLineup)
        {
            string insertQuery = @"INSERT INTO TeamPlayerLineup
                    (TeamPlayerLineupAlternateKey, TeamAlternateKey, PlayerAlternateKey, BattingOrder, DeleteDate)
                    VALUES(
                        @TeamPlayerLineupAlternateKey, 
                        @TeamAlternateKey, 
                        @PlayerAlternateKey, 
                        @BattingOrder, 
                        @DeleteDate)";

            Connection.BeginTransaction();
            Connection.Execute(insertQuery, teamPlayerLineup);
            Connection.Commit();
        }
        private void UpdateTeamPlayerLineup(TeamPlayerLineupDto teamPlayerLineup)
        {
            string updateQuery = @"UPDATE TeamPlayerLineup
            SET TeamPlayerLineupAlternateKey = @TeamPlayerLineupAlternateKey,
            PlayerAlternateKey = @PlayerAlternateKey,
            TeamAlternateKey = @TeamAlternateKey,
            BattingOrder = @BattingOrder, 
            DeleteDate = @DeleteDate 
            WHERE PlayerAlternateKey = @PlayerAlternateKey AND TeamAlternateKey = @TeamAlternateKey";

            Connection.BeginTransaction();
            Connection.Execute(updateQuery, teamPlayerLineup);
            Connection.Commit();
        }


        public void Delete(Guid playerAlternateKey, Guid teamAlternateKey)
        {
            string deleteQuery = @"DELETE FROM TeamPlayerLineup WHERE PlayerAlternateKey = @PlayerAlternateKey AND TeamAlternateKey = @TeamAlternateKey";

            Connection.BeginTransaction();
            Connection.Execute(deleteQuery, new { PlayerAlternateKey = playerAlternateKey.ToString(), TeamAlternateKey = teamAlternateKey.ToString() });
            Connection.Commit();
        }


        public bool ExistsInDb(TeamPlayerLineupDto teamPlayerLineup)
        {
            Connection.BeginTransaction();

            var rows = Connection.Query<int>(@"SELECT COUNT(1) as 'Count' FROM TeamPlayerLineup 
            WHERE PlayerAlternateKey = @PlayerAlternateKey AND TeamAlternateKey = @TeamAlternateKey",
            new { teamPlayerLineup.PlayerAlternateKey, teamPlayerLineup.TeamAlternateKey });
            Connection.Commit();

            return rows.First() > 0;
        }



        private const string SELECT_QUERY =
            @"SELECT 
            TeamPlayerLineupId, 
            TeamPlayerLineupAlternateKey, 
            TeamAlternateKey, 
            PlayerAlternateKey, 
            BattingOrder,
            ChangeDate, 
            DeleteDate
            FROM TeamPlayerLineup ";

    }
}
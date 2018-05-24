using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data.SQLite;
using Dartball.DataLayer.Device.Dto;
using System.Linq;

namespace Dartball.DataLayer.Device.Repository
{
    public class TeamPlayerLineupRepository : ConnectionBase
    {

        public List<TeamPlayerLineupDto> LoadAll()
        {
            List<TeamPlayerLineupDto> teamPlayerLineups = new List<TeamPlayerLineupDto>();

            Connection.Open();
            teamPlayerLineups.AddRange(Connection.Query<TeamPlayerLineupDto>(SELECT_QUERY));
            Connection.Close();

            return teamPlayerLineups;
        }

        public TeamPlayerLineupDto LoadByCompositeKey(Guid playerAlternateKey, Guid teamAlternateKey)
        {
            TeamPlayerLineupDto teamPlayerLineup = null;
            Connection.Open();

            var result = Connection.Query<TeamPlayerLineupDto>(
                SELECT_QUERY + " WHERE PlayerAlternateKey = @PlayerAlternateKey " +
                "AND TeamAlternateKey = @TeamAlternateKey",
                new { PlayerAlternateKey = playerAlternateKey.ToString(), TeamAlternateKey = teamAlternateKey.ToString() });

            teamPlayerLineup = result.FirstOrDefault();

            Connection.Close();

            return teamPlayerLineup;
        }

        public List<TeamPlayerLineupDto> LoadByTeamAlternateKey(Guid teamAlternateKey)
        {
            List<TeamPlayerLineupDto> teamPlayerLineups = new List<TeamPlayerLineupDto>();

            Connection.Open();
            teamPlayerLineups.AddRange(Connection.Query<TeamPlayerLineupDto>(
                SELECT_QUERY + " WHERE TeamAlternateKey = @TeamAlternateKey ",
                new { TeamAlternateKey = teamAlternateKey.ToString() }));
            Connection.Close();

            return teamPlayerLineups;
        }

        public List<TeamPlayerLineupDto> LoadByPlayerAlternateKey(Guid playerAlternateKey)
        {
            List<TeamPlayerLineupDto> teamPlayerLineups = new List<TeamPlayerLineupDto>();

            Connection.Open();
            var result = Connection.Query<TeamPlayerLineupDto>(
                SELECT_QUERY + " WHERE PlayerAlternateKey = @PlayerAlternateKey ",
                new { PlayerAlternateKey = playerAlternateKey.ToString() });
            Connection.Close();

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

            Connection.Open();
            Connection.Query(insertQuery, teamPlayerLineup);
            Connection.Close();
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

            Connection.Open();
            Connection.Query(updateQuery, teamPlayerLineup);
            Connection.Close();
        }


        public void Delete(Guid playerAlternateKey, Guid teamAlternateKey)
        {
            string deleteQuery = @"DELETE FROM TeamPlayerLineup WHERE PlayerAlternateKey = @PlayerAlternateKey AND TeamAlternateKey = @TeamAlternateKey";

            Connection.Open();
            Connection.Query(deleteQuery, new { PlayerAlternateKey = playerAlternateKey.ToString(), TeamAlternateKey = teamAlternateKey.ToString() });
            Connection.Close();
        }


        public bool ExistsInDb(TeamPlayerLineupDto teamPlayerLineup)
        {
            Connection.Open();

            var rows = Connection.Query<int>(@"SELECT COUNT(1) as 'Count' FROM TeamPlayerLineup 
            WHERE PlayerAlternateKey = @PlayerAlternateKey AND TeamAlternateKey = @TeamAlternateKey",
            new { teamPlayerLineup.PlayerAlternateKey, teamPlayerLineup.TeamAlternateKey });
            Connection.Close();

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
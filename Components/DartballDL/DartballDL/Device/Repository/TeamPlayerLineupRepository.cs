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

        public TeamPlayerLineupDto LoadByCompositeKey(Guid playerId, Guid teamId)
        {
            TeamPlayerLineupDto teamPlayerLineup = null;
            Connection.BeginTransaction();

            var result = Connection.Query<TeamPlayerLineupDto>(
                SELECT_QUERY + " WHERE PlayerId = @PlayerId " +
                "AND TeamId = @TeamId",
                new { PlayerId = playerId.ToString(), TeamId = teamId.ToString() });

            teamPlayerLineup = result.FirstOrDefault();

            Connection.Commit();

            return teamPlayerLineup;
        }

        public List<TeamPlayerLineupDto> LoadByTeamId(Guid teamId)
        {
            List<TeamPlayerLineupDto> teamPlayerLineups = new List<TeamPlayerLineupDto>();

            Connection.BeginTransaction();
            teamPlayerLineups.AddRange(Connection.Query<TeamPlayerLineupDto>(
                SELECT_QUERY + " WHERE TeamId = @TeamId ",
                new { TeamId = teamId.ToString() }));
            Connection.Commit();

            return teamPlayerLineups;
        }

        public List<TeamPlayerLineupDto> LoadByPlayerId(Guid playerId)
        {
            List<TeamPlayerLineupDto> teamPlayerLineups = new List<TeamPlayerLineupDto>();

            Connection.BeginTransaction();
            var result = Connection.Query<TeamPlayerLineupDto>(
                SELECT_QUERY + " WHERE PlayerId = @PlayerId ",
                new { PlayerId = playerId.ToString() });
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
                    (TeamPlayerLineupId, TeamId, PlayerId, BattingOrder, DeleteDate)
                    VALUES(
                        @TeamPlayerLineupId, 
                        @TeamId, 
                        @PlayerId, 
                        @BattingOrder, 
                        @DeleteDate)";

            Connection.BeginTransaction();
            Connection.Execute(insertQuery, teamPlayerLineup);
            Connection.Commit();
        }
        private void UpdateTeamPlayerLineup(TeamPlayerLineupDto teamPlayerLineup)
        {
            string updateQuery = @"UPDATE TeamPlayerLineup
            SET TeamPlayerLineupId = @TeamPlayerLineupId,
            PlayerId = @PlayerId,
            TeamId = @TeamId,
            BattingOrder = @BattingOrder, 
            DeleteDate = @DeleteDate 
            WHERE PlayerId = @PlayerId AND TeamId = @TeamId";

            Connection.BeginTransaction();
            Connection.Execute(updateQuery, teamPlayerLineup);
            Connection.Commit();
        }


        public void Delete(Guid playerId, Guid teamId)
        {
            string deleteQuery = @"DELETE FROM TeamPlayerLineup WHERE PlayerId = @PlayerId AND TeamId = @TeamId";

            Connection.BeginTransaction();
            Connection.Execute(deleteQuery, new { PlayerId = playerId.ToString(), TeamId = teamId.ToString() });
            Connection.Commit();
        }


        public bool ExistsInDb(TeamPlayerLineupDto teamPlayerLineup)
        {
            Connection.BeginTransaction();

            var rows = Connection.Query<int>(@"SELECT COUNT(1) as 'Count' FROM TeamPlayerLineup 
            WHERE PlayerId = @PlayerId AND TeamId = @TeamId",
            new { teamPlayerLineup.PlayerId, teamPlayerLineup.TeamId });
            Connection.Commit();

            return rows.First() > 0;
        }



        private const string SELECT_QUERY =
            @"SELECT 
            TeamPlayerLineupId, 
            TeamPlayerLineupId, 
            TeamId, 
            PlayerId, 
            BattingOrder,
            ChangeDate, 
            DeleteDate
            FROM TeamPlayerLineup ";

    }
}
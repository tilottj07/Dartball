using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Dapper;
using System.Linq;
using Dartball.DataLayer.Device.Dto;

namespace Dartball.DataLayer.Device.Repository
{
    public class PlayerTeamRepository : ConnectionBase
    {

        public List<PlayerTeamDto> LoadAll()
        {
            List<PlayerTeamDto> playerTeams = new List<PlayerTeamDto>();

            Connection.BeginTransaction();
            playerTeams.AddRange(Connection.Query<PlayerTeamDto>(SELECT_QUERY));
            Connection.Commit();

            return playerTeams;
        }

        public PlayerTeamDto LoadByCompositeKey(Guid playerId, Guid teamId)
        {
            PlayerTeamDto playerTeam = null;
            Connection.BeginTransaction();

            var result = Connection.Query<PlayerTeamDto>(
                SELECT_QUERY + " WHERE PlayerId = @PlayerId " +
                "AND TeamId = @TeamId",
                new { PlayerId = playerId.ToString(), TeamId = teamId.ToString() });

            playerTeam = result.FirstOrDefault();

            Connection.Commit();

            return playerTeam;
        }

        public List<PlayerTeamDto> LoadByTeamId(Guid teamId)
        {
            List<PlayerTeamDto> playerTeams = new List<PlayerTeamDto>();

            Connection.BeginTransaction();
            playerTeams.AddRange(Connection.Query<PlayerTeamDto>(
                SELECT_QUERY + " WHERE TeamId = @TeamId ",
                new { TeamId = teamId.ToString() }));
            Connection.Commit();

            return playerTeams;
        }

        public List<PlayerTeamDto> LoadByPlayerId(Guid playerId)
        {
            List<PlayerTeamDto> playerTeams = new List<PlayerTeamDto>();

            Connection.BeginTransaction();
            playerTeams.AddRange(Connection.Query<PlayerTeamDto>(
                SELECT_QUERY + " WHERE PlayerId = @PlayerId ",
                new { PlayerId = playerId.ToString() }));
            Connection.Commit();

            return playerTeams;
        }


        public void AddNew(PlayerTeamDto playerTeam)
        {
            InsertPlayerTeam(playerTeam);
        }
        public void Update(PlayerTeamDto playerTeam)
        {
            UpdatePlayerTeam(playerTeam);
        }

        /// <summary>
        /// data layer will determine add vs update
        /// </summary>
        /// <param name="playerTeam"></param>
        public void Save(PlayerTeamDto playerTeam)
        {
            if (ExistsInDb(playerTeam)) Update(playerTeam);
            else AddNew(playerTeam);
        }


        private void InsertPlayerTeam(PlayerTeamDto playerTeam)
        {
            string insertQuery = @"INSERT INTO PlayerTeam
                    (PlayerTeamId, PlayerId, TeamId, DeleteDate)
                    VALUES(
                        @PlayerTeamId, 
                        @PlayerId, 
                        @TeamId, 
                        @DeleteDate)";

            Connection.BeginTransaction();
            Connection.Execute(insertQuery, playerTeam);
            Connection.Commit();
        }
        private void UpdatePlayerTeam(PlayerTeamDto playerTeam)
        {
            string updateQuery = @"UPDATE PlayerTeam
            SET PlayerTeamId = @PlayerTeamId,
            PlayerId = @PlayerId,
            TeamId = @TeamId,
            DeleteDate = @DeleteDate 
            WHERE PlayerId = @PlayerId AND TeamId = @TeamId";

            Connection.BeginTransaction();
            Connection.Execute(updateQuery, playerTeam);
            Connection.Commit();
        }


        public void Delete(Guid playerId, Guid teamId)
        {
            string deleteQuery = @"DELETE FROM PlayerTeam WHERE PlayerId = @PlayerId AND TeamId = @TeamId";

            Connection.BeginTransaction();
            Connection.Execute(deleteQuery, new { PlayerId = playerId.ToString(), TeamId = teamId.ToString() });
            Connection.Commit();
        }


        public bool ExistsInDb(PlayerTeamDto playerTeam)
        {
            Connection.BeginTransaction();

            var rows = Connection.Query<int>(@"SELECT COUNT(1) as 'Count' FROM PlayerTeam 
            WHERE PlayerId = @PlayerId AND TeamId = @TeamId", 
            new { playerTeam.PlayerId, playerTeam.TeamId });
            Connection.Commit();

            return rows.First() > 0;
        }



        private const string SELECT_QUERY =
        @"SELECT PlayerTeamId, PlayerTeamId, PlayerId, TeamId, ChangeDate, DeleteDate
        FROM PlayerTeam ";

    }
}
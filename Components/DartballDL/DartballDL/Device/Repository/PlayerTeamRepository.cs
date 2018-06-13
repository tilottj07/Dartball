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

        public PlayerTeamDto LoadByCompositeKey(Guid playerAlternateKey, Guid teamAlternateKey)
        {
            PlayerTeamDto playerTeam = null;
            Connection.BeginTransaction();

            var result = Connection.Query<PlayerTeamDto>(
                SELECT_QUERY + " WHERE PlayerAlternateKey = @PlayerAlternateKey " +
                "AND TeamAlternateKey = @TeamAlternateKey",
                new { PlayerAlternateKey = playerAlternateKey.ToString(), TeamAlternateKey = teamAlternateKey.ToString() });

            playerTeam = result.FirstOrDefault();

            Connection.Commit();

            return playerTeam;
        }

        public List<PlayerTeamDto> LoadByTeamAlternateKey(Guid teamAlternateKey)
        {
            List<PlayerTeamDto> playerTeams = new List<PlayerTeamDto>();

            Connection.BeginTransaction();
            playerTeams.AddRange(Connection.Query<PlayerTeamDto>(
                SELECT_QUERY + " WHERE TeamAlternateKey = @TeamAlternateKey ",
                new { TeamAlternateKey = teamAlternateKey.ToString() }));
            Connection.Commit();

            return playerTeams;
        }

        public List<PlayerTeamDto> LoadByPlayerAlternateKey(Guid playerAlternateKey)
        {
            List<PlayerTeamDto> playerTeams = new List<PlayerTeamDto>();

            Connection.BeginTransaction();
            playerTeams.AddRange(Connection.Query<PlayerTeamDto>(
                SELECT_QUERY + " WHERE PlayerAlternateKey = @PlayerAlternateKey ",
                new { PlayerAlternateKey = playerAlternateKey.ToString() }));
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
                    (PlayerTeamAlternateKey, PlayerAlternateKey, TeamAlternateKey, DeleteDate)
                    VALUES(
                        @PlayerTeamAlternateKey, 
                        @PlayerAlternateKey, 
                        @TeamAlternateKey, 
                        @DeleteDate)";

            Connection.BeginTransaction();
            Connection.Execute(insertQuery, playerTeam);
            Connection.Commit();
        }
        private void UpdatePlayerTeam(PlayerTeamDto playerTeam)
        {
            string updateQuery = @"UPDATE PlayerTeam
            SET PlayerTeamAlternateKey = @PlayerTeamAlternateKey,
            PlayerAlternateKey = @PlayerAlternateKey,
            TeamAlternateKey = @TeamAlternateKey,
            DeleteDate = @DeleteDate 
            WHERE PlayerAlternateKey = @PlayerAlternateKey AND TeamAlternateKey = @TeamAlternateKey";

            Connection.BeginTransaction();
            Connection.Execute(updateQuery, playerTeam);
            Connection.Commit();
        }


        public void Delete(Guid playerAlternateKey, Guid teamAlternateKey)
        {
            string deleteQuery = @"DELETE FROM PlayerTeam WHERE PlayerAlternateKey = @PlayerAlternateKey AND TeamAlternateKey = @TeamAlternateKey";

            Connection.BeginTransaction();
            Connection.Execute(deleteQuery, new { PlayerAlternateKey = playerAlternateKey.ToString(), TeamAlternateKey = teamAlternateKey.ToString() });
            Connection.Commit();
        }


        public bool ExistsInDb(PlayerTeamDto playerTeam)
        {
            Connection.BeginTransaction();

            var rows = Connection.Query<int>(@"SELECT COUNT(1) as 'Count' FROM PlayerTeam 
            WHERE PlayerAlternateKey = @PlayerAlternateKey AND TeamAlternateKey = @TeamAlternateKey", 
            new { playerTeam.PlayerAlternateKey, playerTeam.TeamAlternateKey });
            Connection.Commit();

            return rows.First() > 0;
        }



        private const string SELECT_QUERY =
        @"SELECT PlayerTeamId, PlayerTeamAlternateKey, PlayerAlternateKey, TeamAlternateKey, ChangeDate, DeleteDate
        FROM PlayerTeam ";

    }
}
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

        public List<PlayerTeamDto> LoadAll()
        {
            List<PlayerTeamDto> playerTeams = new List<PlayerTeamDto>();

            Connection.Open();
            playerTeams.AddRange(Connection.Query<PlayerTeamDto>(SELECT_QUERY));
            Connection.Close();

            return playerTeams;
        }

        public PlayerTeamDto LoadByCompositeKey(Guid playerAlternateKey, Guid teamAlternateKey)
        {
            PlayerTeamDto playerTeam = null;
            Connection.Open();

            var result = Connection.Query<PlayerTeamDto>(
                SELECT_QUERY + " WHERE PlayerAlternateKey = @PlayerAlternateKey " +
                "AND TeamAlternateKey = @TeamAlternateKey",
                new { PlayerAlternateKey = playerAlternateKey.ToString(), TeamAlternateKey = teamAlternateKey.ToString() });

            playerTeam = result.FirstOrDefault();

            Connection.Close();

            return playerTeam;
        }

        public List<PlayerTeamDto> LoadByTeamAlternateKey(Guid teamAlternateKey)
        {
            List<PlayerTeamDto> playerTeams = new List<PlayerTeamDto>();

            Connection.Open();
            playerTeams.AddRange(Connection.Query<PlayerTeamDto>(
                SELECT_QUERY + " WHERE TeamAlternateKey = @TeamAlternateKey ",
                new { TeamAlternateKey = teamAlternateKey.ToString() }));
            Connection.Close();

            return playerTeams;
        }

        public List<PlayerTeamDto> LoadByPlayerAlternateKey(Guid playerAlternateKey)
        {
            List<PlayerTeamDto> playerTeams = new List<PlayerTeamDto>();

            Connection.Open();
            var result = Connection.Query<PlayerTeamDto>(
                SELECT_QUERY + " WHERE PlayerAlternateKey = @PlayerAlternateKey ",
                new { PlayerAlternateKey = playerAlternateKey.ToString() });
            Connection.Close();

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
        /// <param name="league"></param>
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

            Connection.Open();
            Connection.Query(insertQuery, playerTeam);
            Connection.Close();
        }
        private void UpdatePlayerTeam(PlayerTeamDto playerTeam)
        {
            string updateQuery = @"UPDATE PlayerTeam
            SET PlayerTeamAlternateKey = @PlayerTeamAlternateKey,
            PlayerAlternateKey = @PlayerAlternateKey,
            TeamAlternateKey = @TeamAlternateKey,
            DeleteDate = @DeleteDate 
            WHERE PlayerAlternateKey = @PlayerAlternateKey AND TeamAlternateKey = @TeamAlternateKey";

            Connection.Open();
            Connection.Query(updateQuery, playerTeam);
            Connection.Close();
        }


        public void Delete(Guid playerAlternateKey, Guid teamAlternateKey)
        {
            string deleteQuery = @"DELETE FROM PlayerTeam WHERE PlayerAlternateKey = @PlayerAlternateKey AND TeamAlternateKey = @TeamAlternateKey";

            Connection.Open();
            Connection.Query(deleteQuery, new { PlayerAlternateKey = playerAlternateKey.ToString(), TeamAlternateKey = teamAlternateKey.ToString() });
            Connection.Close();
        }


        public bool ExistsInDb(PlayerTeamDto playerTeam)
        {
            Connection.Open();

            var rows = Connection.Query<int>(@"SELECT COUNT(1) as 'Count' FROM PlayerTeam 
            WHERE PlayerAlternateKey = @PlayerAlternateKey AND TeamAlternateKey = @TeamAlternateKey",
            new { playerTeam.PlayerAlternateKey, playerTeam.TeamAlternateKey });
            Connection.Close();

            return rows.First() > 0;
        }



        private const string SELECT_QUERY =
        @"SELECT PlayerTeamId, PlayerTeamAlternateKey, PlayerAlternateKey, TeamAlternateKey, ChangeDate, DeleteDate
        FROM PlayerTeam ";

    }
}
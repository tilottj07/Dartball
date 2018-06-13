using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using SQLite;
using System.Linq;
using Dartball.DataLayer.Device.Dto;

namespace Dartball.DataLayer.Device.Repository
{
    public class GameTeamRepository : ConnectionBase
    {

        public List<GameTeamDto> LoadAll()
        {
            List<GameTeamDto> gameTeams = new List<GameTeamDto>();

            Connection.BeginTransaction();
            gameTeams.AddRange(Connection.Query<GameTeamDto>(SELECT_QUERY));
            Connection.Commit();

            return gameTeams;
        }

        public GameTeamDto LoadByCompositeKey(Guid gameAlternateKey, Guid teamAlternateKey)
        {
            GameTeamDto gameTeam = null;
            Connection.BeginTransaction();

            var result = Connection.Query<GameTeamDto>(
                SELECT_QUERY + " WHERE GameAlternateKey = @GameAlternateKey " +
                "AND TeamAlternateKey = @TeamAlternateKey",
                new { GameAlternateKey = gameAlternateKey.ToString(), TeamAlternateKey = teamAlternateKey.ToString() });

            gameTeam = result.FirstOrDefault();

            Connection.Commit();

            return gameTeam;
        }

        public List<GameTeamDto> LoadByTeamAlternateKey(Guid teamAlternateKey)
        {
            List<GameTeamDto> gameTeams = new List<GameTeamDto>();

            Connection.BeginTransaction();
            gameTeams.AddRange(Connection.Query<GameTeamDto>(
                SELECT_QUERY + " WHERE TeamAlternateKey = @TeamAlternateKey ",
                new { TeamAlternateKey = teamAlternateKey.ToString() }));
            Connection.Commit();

            return gameTeams;
        }

        public List<GameTeamDto> LoadByGameAlternateKey(Guid gameAlternateKey)
        {
            List<GameTeamDto> gameTeams = new List<GameTeamDto>();

            Connection.BeginTransaction();
            gameTeams.AddRange(Connection.Query<GameTeamDto>(
                SELECT_QUERY + " WHERE GameAlternateKey = @GameAlternateKey ",
                new { GameAlternateKey = gameAlternateKey.ToString() }));
            Connection.Commit();

            return gameTeams;
        }


        public void AddNew(GameTeamDto gameTeam)
        {
            InsertGameTeam(gameTeam);
        }
        public void Update(GameTeamDto gameTeam)
        {
            UpdateGameTeam(gameTeam);
        }

        /// <summary>
        /// data layer will determine add vs update
        /// </summary>
        /// <param name="league"></param>
        public void Save(GameTeamDto playerTeam)
        {
            if (ExistsInDb(playerTeam)) Update(playerTeam);
            else AddNew(playerTeam);
        }


        private void InsertGameTeam(GameTeamDto gameTeam)
        {
            string insertQuery = @"INSERT INTO GameTeam
                    (GameTeamAlternateKey, GameAlternateKey, TeamAlternateKey, DeleteDate)
                    VALUES(
                        @GameTeamAlternateKey, 
                        @GameAlternateKey, 
                        @TeamAlternateKey, 
                        @DeleteDate)";

            Connection.BeginTransaction();
            Connection.Execute(insertQuery, gameTeam);
            Connection.Commit();
        }
        private void UpdateGameTeam(GameTeamDto gameTeam)
        {
            string updateQuery = @"UPDATE GameTeam
            SET GameTeamAlternateKey = @GameTeamAlternateKey,
            DeleteDate = @DeleteDate 
            WHERE GameAlternateKey = @GameAlternateKey AND TeamAlternateKey = @TeamAlternateKey";

            Connection.BeginTransaction();
            Connection.Execute(updateQuery, gameTeam);
            Connection.Commit();
        }


        public void Delete(Guid gameAlternateKey, Guid teamAlternateKey)
        {
            string deleteQuery = @"DELETE FROM GameTeam WHERE GameAlternateKey = @GameAlternateKey AND TeamAlternateKey = @TeamAlternateKey";

            Connection.BeginTransaction();
            Connection.Execute(deleteQuery, new { GameAlternateKey = gameAlternateKey.ToString(), TeamAlternateKey = teamAlternateKey.ToString() });
            Connection.Commit();
        }


        public bool ExistsInDb(GameTeamDto gameTeam)
        {
            Connection.BeginTransaction();

            var rows = Connection.Query<int>(@"SELECT COUNT(1) as 'Count' FROM GameTeam 
            WHERE GameAlternateKey = @GameAlternateKey AND TeamAlternateKey = @TeamAlternateKey",
            new { gameTeam.GameAlternateKey, gameTeam.TeamAlternateKey });
            Connection.Commit();

            return rows.First() > 0;
        }



        private const string SELECT_QUERY =
        @"SELECT 
            GameTeamId, 
            GameTeamAlternateKey, 
            GameAlternateKey, 
            TeamAlternateKey, 
            ChangeDate, 
            DeleteDate
        FROM GameTeam ";

    }
}
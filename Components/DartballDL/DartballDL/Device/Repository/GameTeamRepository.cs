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

        public GameTeamDto LoadByCompositeKey(Guid gameId, Guid teamId)
        {
            GameTeamDto gameTeam = null;
            Connection.BeginTransaction();

            var result = Connection.Query<GameTeamDto>(
                SELECT_QUERY + " WHERE GameId = @GameId " +
                "AND TeamId = @TeamId",
                new { GameId = gameId.ToString(), TeamId = teamId.ToString() });

            gameTeam = result.FirstOrDefault();

            Connection.Commit();

            return gameTeam;
        }

        public List<GameTeamDto> LoadByTeamId(Guid teamId)
        {
            List<GameTeamDto> gameTeams = new List<GameTeamDto>();

            Connection.BeginTransaction();
            gameTeams.AddRange(Connection.Query<GameTeamDto>(
                SELECT_QUERY + " WHERE TeamId = @TeamId ",
                new { TeamId = teamId.ToString() }));
            Connection.Commit();

            return gameTeams;
        }

        public List<GameTeamDto> LoadByGameId(Guid gameId)
        {
            List<GameTeamDto> gameTeams = new List<GameTeamDto>();

            Connection.BeginTransaction();
            gameTeams.AddRange(Connection.Query<GameTeamDto>(
                SELECT_QUERY + " WHERE GameId = @GameId ",
                new { GameId = gameId.ToString() }));
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
                    (GameTeamId, GameId, TeamId, DeleteDate)
                    VALUES(
                        @GameTeamId, 
                        @GameId, 
                        @TeamId, 
                        @DeleteDate)";

            Connection.BeginTransaction();
            Connection.Execute(insertQuery, gameTeam);
            Connection.Commit();
        }
        private void UpdateGameTeam(GameTeamDto gameTeam)
        {
            string updateQuery = @"UPDATE GameTeam
            SET GameTeamId = @GameTeamId,
            DeleteDate = @DeleteDate 
            WHERE GameId = @GameId AND TeamId = @TeamId";

            Connection.BeginTransaction();
            Connection.Execute(updateQuery, gameTeam);
            Connection.Commit();
        }


        public void Delete(Guid gameId, Guid teamId)
        {
            string deleteQuery = @"DELETE FROM GameTeam WHERE GameId = @GameId AND TeamId = @TeamId";

            Connection.BeginTransaction();
            Connection.Execute(deleteQuery, new { GameId = gameId.ToString(), TeamId = teamId.ToString() });
            Connection.Commit();
        }


        public bool ExistsInDb(GameTeamDto gameTeam)
        {
            Connection.BeginTransaction();

            var rows = Connection.Query<int>(@"SELECT COUNT(1) as 'Count' FROM GameTeam 
            WHERE GameId = @GameId AND TeamId = @TeamId",
            new { gameTeam.GameId, gameTeam.TeamId });
            Connection.Commit();

            return rows.First() > 0;
        }



        private const string SELECT_QUERY =
        @"SELECT 
            GameTeamId, 
            GameTeamId, 
            GameId, 
            TeamId, 
            ChangeDate, 
            DeleteDate
        FROM GameTeam ";

    }
}
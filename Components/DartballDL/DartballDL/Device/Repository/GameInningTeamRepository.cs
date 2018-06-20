using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Dapper;
using System.Linq;
using Dartball.DataLayer.Device.Dto;

namespace Dartball.DataLayer.Device.Repository
{
    public class GameInningTeamRepository : ConnectionBase
    {

        public List<GameInningTeamDto> LoadAll()
        {
            List<GameInningTeamDto> gameInningTeams = new List<GameInningTeamDto>();

            Connection.BeginTransaction();
            gameInningTeams.AddRange(Connection.Query<GameInningTeamDto>(SELECT_QUERY));
            Connection.Commit();

            return gameInningTeams;
        }

        public GameInningTeamDto LoadByCompositeKey(Guid gameTeamId, Guid gameInningId)
        {
            GameInningTeamDto gameInningTeam = null;
            Connection.BeginTransaction();

            var result = Connection.Query<GameInningTeamDto>(
                SELECT_QUERY + " WHERE GameTeamId = @GameTeamId " +
                "AND GameInningId = @GameInningId",
                new { GameTeamId = gameTeamId.ToString(), GameInningId = gameInningId.ToString() });

            gameInningTeam = result.FirstOrDefault();

            Connection.Commit();

            return gameInningTeam;
        }

        public List<GameInningTeamDto> LoadByGameTeamId(Guid gameTeamId)
        {
            List<GameInningTeamDto> gameInningTeams = new List<GameInningTeamDto>();

            Connection.BeginTransaction();
            gameInningTeams.AddRange(Connection.Query<GameInningTeamDto>(
                SELECT_QUERY + " WHERE GameTeamId = @GameTeamId ",
                new { GameTeamId = gameTeamId.ToString() }));
            Connection.Commit();

            return gameInningTeams;
        }

        public List<GameInningTeamDto> LoadByGameInningId(Guid gameInningId)
        {
            List<GameInningTeamDto> gameInningTeams = new List<GameInningTeamDto>();

            Connection.BeginTransaction();
            gameInningTeams.AddRange(Connection.Query<GameInningTeamDto>(
                SELECT_QUERY + " WHERE GameInningId = @GameInningId ",
                new { GameInningId = gameInningId.ToString() }));
            Connection.Commit();

            return gameInningTeams;
        }


        public void AddNew(GameInningTeamDto gameInningTeam)
        {
            InsertGameInningTeam(gameInningTeam);
        }
        public void Update(GameInningTeamDto gameInningTeam)
        {
            UpdateGameInningTeam(gameInningTeam);
        }

        /// <summary>
        /// data layer will determine add vs update
        /// </summary>
        /// <param name="gameInningTeam"></param>
        public void Save(GameInningTeamDto gameInningTeam)
        {
            if (ExistsInDb(gameInningTeam)) Update(gameInningTeam);
            else AddNew(gameInningTeam);
        }


        private void InsertGameInningTeam(GameInningTeamDto gameInningTeam)
        {
            string insertQuery = @"INSERT INTO GameInningTeam
                    (GameInningTeamId, GameInningId, GameTeamId, Score, 
                    Outs, IsRunnerOnFirst, IsRunnerOnSecond, IsRunnerOnThird, DeleteDate)
                    VALUES(
                        @GameInningTeamId, 
                        @GameInningId, 
                        @GameTeamId, 
                        @Score,
                        @Outs,
                        @IsRunnerOnFirst,
                        @IsRunnerOnSecond, 
                        @IsRunnerOnThird, 
                        @DeleteDate)";

            Connection.BeginTransaction();
            Connection.Execute(insertQuery, gameInningTeam);
            Connection.Commit();
        }
        private void UpdateGameInningTeam(GameInningTeamDto gameInningTeam)
        {
            string updateQuery = @"UPDATE GameInningTeam
            SET GameInningTeamId = @GameInningTeamId,
            Score = @Score, 
            Outs = @Outs,
            IsRunnerOnFirst = @IsRunnerOnFirst,
            IsRunnerOnSecond = @IsRunnerOnSecond,
            IsRunnerOnThird = @IsRunnerOnThird,
            DeleteDate = @DeleteDate
            WHERE GameTeamId = @GameTeamId AND GameInningId = @GameInningId";

            Connection.BeginTransaction();
            Connection.Execute(updateQuery, gameInningTeam);
            Connection.Commit();
        }


        public void Delete(Guid gameTeamId, Guid gameInningId)
        {
            string deleteQuery = @"DELETE FROM GameInningTeam WHERE GameTeamId = @GameTeamId AND GameInningId = @GameInningId";

            Connection.BeginTransaction();
            Connection.Execute(deleteQuery, new { GameTeamId = gameTeamId.ToString(), GameInningId = gameInningId.ToString() });
            Connection.Commit();
        }


        public bool ExistsInDb(GameInningTeamDto gameInningTeam)
        {
            Connection.BeginTransaction();

            var rows = Connection.Query<int>(@"SELECT COUNT(1) as 'Count' FROM GameInningTeam 
            WHERE GameTeamId = @GameTeamId AND GameInningId = @GameInningId",
            new { gameInningTeam.GameTeamId, gameInningTeam.GameInningId });
            Connection.Commit();

            return rows.First() > 0;
        }



        private const string SELECT_QUERY =
        @"SELECT 
            GameInningTeamId, 
            GameInningTeamId, 
            GameInningId, 
            GameTeamId, 
            Score, 
            Outs, 
            IsRunnerOnFirst, 
            IsRunnerOnSecond, 
            IsRunnerOnThird, 
            ChangeDate,
            DeleteDate
        FROM GameInningTeam ";

    }
}
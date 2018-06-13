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

        public GameInningTeamDto LoadByCompositeKey(Guid gameTeamAlternateKey, Guid gameInningAlternateKey)
        {
            GameInningTeamDto gameInningTeam = null;
            Connection.BeginTransaction();

            var result = Connection.Query<GameInningTeamDto>(
                SELECT_QUERY + " WHERE GameTeamAlternateKey = @GameTeamAlternateKey " +
                "AND GameInningAlternateKey = @GameInningAlternateKey",
                new { GameTeamAlternateKey = gameTeamAlternateKey.ToString(), GameInningAlternateKey = gameInningAlternateKey.ToString() });

            gameInningTeam = result.FirstOrDefault();

            Connection.Commit();

            return gameInningTeam;
        }

        public List<GameInningTeamDto> LoadByGameTeamAlternateKey(Guid gameTeamAlternateKey)
        {
            List<GameInningTeamDto> gameInningTeams = new List<GameInningTeamDto>();

            Connection.BeginTransaction();
            gameInningTeams.AddRange(Connection.Query<GameInningTeamDto>(
                SELECT_QUERY + " WHERE GameTeamAlternateKey = @GameTeamAlternateKey ",
                new { GameTeamAlternateKey = gameTeamAlternateKey.ToString() }));
            Connection.Commit();

            return gameInningTeams;
        }

        public List<GameInningTeamDto> LoadByGameInningAlternateKey(Guid gameInningAlternateKey)
        {
            List<GameInningTeamDto> gameInningTeams = new List<GameInningTeamDto>();

            Connection.BeginTransaction();
            gameInningTeams.AddRange(Connection.Query<GameInningTeamDto>(
                SELECT_QUERY + " WHERE GameInningAlternateKey = @GameInningAlternateKey ",
                new { GameInningAlternateKey = gameInningAlternateKey.ToString() }));
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
                    (GameInningTeamAlternateKey, GameInningAlternateKey, GameTeamAlternateKey, Score, 
                    Outs, IsRunnerOnFirst, IsRunnerOnSecond, IsRunnerOnThird, DeleteDate)
                    VALUES(
                        @GameInningTeamAlternateKey, 
                        @GameInningAlternateKey, 
                        @GameTeamAlternateKey, 
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
            SET GameInningTeamAlternateKey = @GameInningTeamAlternateKey,
            Score = @Score, 
            Outs = @Outs,
            IsRunnerOnFirst = @IsRunnerOnFirst,
            IsRunnerOnSecond = @IsRunnerOnSecond,
            IsRunnerOnThird = @IsRunnerOnThird,
            DeleteDate = @DeleteDate
            WHERE GameTeamAlternateKey = @GameTeamAlternateKey AND GameInningAlternateKey = @GameInningAlternateKey";

            Connection.BeginTransaction();
            Connection.Execute(updateQuery, gameInningTeam);
            Connection.Commit();
        }


        public void Delete(Guid gameTeamAlternateKey, Guid gameInningAlternateKey)
        {
            string deleteQuery = @"DELETE FROM GameInningTeam WHERE GameTeamAlternateKey = @GameTeamAlternateKey AND GameInningAlternateKey = @GameInningAlternateKey";

            Connection.BeginTransaction();
            Connection.Execute(deleteQuery, new { GameTeamAlternateKey = gameTeamAlternateKey.ToString(), GameInningAlternateKey = gameInningAlternateKey.ToString() });
            Connection.Commit();
        }


        public bool ExistsInDb(GameInningTeamDto gameInningTeam)
        {
            Connection.BeginTransaction();

            var rows = Connection.Query<int>(@"SELECT COUNT(1) as 'Count' FROM GameInningTeam 
            WHERE GameTeamAlternateKey = @GameTeamAlternateKey AND GameInningAlternateKey = @GameInningAlternateKey",
            new { gameInningTeam.GameTeamAlternateKey, gameInningTeam.GameInningAlternateKey });
            Connection.Commit();

            return rows.First() > 0;
        }



        private const string SELECT_QUERY =
        @"SELECT 
            GameInningTeamId, 
            GameInningTeamAlternateKey, 
            GameInningAlternateKey, 
            GameTeamAlternateKey, 
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
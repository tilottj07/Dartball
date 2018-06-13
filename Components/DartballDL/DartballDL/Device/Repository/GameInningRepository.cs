using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using SQLite;
using System.Linq;
using Dartball.DataLayer.Device.Dto;

namespace Dartball.DataLayer.Device.Repository
{
    public class GameInningRepository : ConnectionBase
    {

        public List<GameInningDto> LoadAll()
        {
            List<GameInningDto> gameInnings = new List<GameInningDto>();

            Connection.BeginTransaction();
            gameInnings.AddRange(Connection.Query<GameInningDto>(SELECT_QUERY));
            Connection.Commit();

            return gameInnings;
        }

        public GameInningDto LoadByCompositeKey(Guid gameAlternateKey, int inningNumber)
        {
            GameInningDto gameInning = null;
            Connection.BeginTransaction();

            var result = Connection.Query<GameInningDto>(
                SELECT_QUERY + " WHERE GameAlternateKey = @GameAlternateKey " +
                "AND InningNumber = @InningNumber",
                new { GameAlternateKey = gameAlternateKey.ToString(), InningNumber = inningNumber });

            gameInning = result.FirstOrDefault();

            Connection.Commit();

            return gameInning;
        }

        public List<GameInningDto> LoadByGameAlternateKey(Guid gameAlternateKey)
        {
            List<GameInningDto> gameInnings = new List<GameInningDto>();

            Connection.BeginTransaction();
            gameInnings.AddRange(Connection.Query<GameInningDto>(
                SELECT_QUERY + " WHERE GameAlternateKey = @GameAlternateKey ",
                new { GameAlternateKey = gameAlternateKey.ToString() }));
            Connection.Commit();

            return gameInnings;
        }

   


        public void AddNew(GameInningDto gameInning)
        {
            InsertGameInning(gameInning);
        }
        public void Update(GameInningDto gameInning)
        {
            UpdateGameInning(gameInning);
        }

        /// <summary>
        /// data layer will determine add vs update
        /// </summary>
        /// <param name="gameInning"></param>
        public void Save(GameInningDto gameInning)
        {
            if (ExistsInDb(gameInning)) Update(gameInning);
            else AddNew(gameInning);
        }


        private void InsertGameInning(GameInningDto gameInning)
        {
            string insertQuery = @"INSERT INTO GameInning
                    (GameInningAlternateKey, GameAlternateKey, InningNumber, DeleteDate)
                    VALUES(
                        @GameInningAlternateKey, 
                        @GameAlternateKey, 
                        @InningNumber, 
                        @DeleteDate)";

            Connection.BeginTransaction();
            Connection.Execute(insertQuery, gameInning);
            Connection.Commit();
        }
        private void UpdateGameInning(GameInningDto gameInning)
        {
            string updateQuery = @"UPDATE GameInning
            SET GameInningAlternateKey = @GameInningAlternateKey,
            DeleteDate = @DeleteDate 
            WHERE GameAlternateKey = @GameAlternateKey AND InningNumber = @InningNumber";

            Connection.BeginTransaction();
            Connection.Execute(updateQuery, gameInning);
            Connection.Commit();
        }


        public void Delete(Guid gameAlternateKey, int innningNumber)
        {
            string deleteQuery = @"DELETE FROM GameInning WHERE GameAlternateKey = @GameAlternateKey AND InningNumber = @InningNumber";

            Connection.BeginTransaction();
            Connection.Execute(deleteQuery, new { GameAlternateKey = gameAlternateKey.ToString(), InningNumber = innningNumber });
            Connection.Commit();
        }


        public bool ExistsInDb(GameInningDto gameInning)
        {
            Connection.BeginTransaction();

            var rows = Connection.Query<int>(@"SELECT COUNT(1) as 'Count' FROM GameInning 
            WHERE GameAlternateKey = @GameAlternateKey AND InningNumber = @InningNumber",
            new { gameInning.GameAlternateKey, gameInning.InningNumber });
            Connection.Commit();

            return rows.First() > 0;
        }



        private const string SELECT_QUERY =
        @"SELECT 
            GameInningId, 
            GameInningAlternateKey, 
            GameAlternateKey, 
            InningNumber, 
            ChangeDate, 
            DeleteDate
        FROM GameInning ";

    }
}
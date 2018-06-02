using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.SQLite;
using Dapper;
using Dartball.DataLayer.Device.Dto;

namespace Dartball.DataLayer.Device.Repository
{
    public class GameInningTeamBatterRepository : ConnectionBase
    {

        public List<GameInningTeamBatterDto> LoadAll()
        {
            List<GameInningTeamBatterDto> gameInningTeamBatters = new List<GameInningTeamBatterDto>();

            Connection.Open();
            gameInningTeamBatters.AddRange(Connection.Query<GameInningTeamBatterDto>(SELECT_QUERY));
            Connection.Close();

            return gameInningTeamBatters;
        }

        public GameInningTeamBatterDto LoadByCompositeKey(Guid gameInningTeamAlternateKey, int sequence)
        {
            GameInningTeamBatterDto gameInningTeamBatter = null;
            Connection.Open();

            var result = Connection.Query<GameInningTeamBatterDto>(
                SELECT_QUERY + " WHERE GameInningTeamAlternateKey = @GameInningTeamAlternateKey " +
                "AND Sequence = @Sequence",
                new { GameInningTeamAlternateKey = gameInningTeamAlternateKey.ToString(), Sequence = sequence });

            gameInningTeamBatter = result.FirstOrDefault();

            Connection.Close();

            return gameInningTeamBatter;
        }

        public List<GameInningTeamBatterDto> LoadByGameInningTeamAlternateKey(Guid gameInningTeamAlternateKey)
        {
            List<GameInningTeamBatterDto> gameInningTeamBatters = new List<GameInningTeamBatterDto>();

            Connection.Open();
            gameInningTeamBatters.AddRange(Connection.Query<GameInningTeamBatterDto>(
                SELECT_QUERY + " WHERE GameInningTeamAlternateKey = @GameInningTeamAlternateKey ",
                new { GameInningTeamAlternateKey = gameInningTeamAlternateKey.ToString() }));
            Connection.Close();

            return gameInningTeamBatters;
        }

        public List<GameInningTeamBatterDto> LoadByPlayerAlternateKey(Guid playerAlternateKey)
        {
            List<GameInningTeamBatterDto> gameInningTeamBatters = new List<GameInningTeamBatterDto>();

            Connection.Open();
            gameInningTeamBatters.AddRange(Connection.Query<GameInningTeamBatterDto>(
                SELECT_QUERY + " WHERE PlayerAlternateKey = @PlayerAlternateKey ",
                new { PlayerAlternateKey = playerAlternateKey.ToString() }));
            Connection.Close();

            return gameInningTeamBatters;
        }


        public List<GameInningTeamBatterDto> LoadByGameAlternateKeyPlayerAlternateKey(Guid gameAlternateKey, Guid playerAlternateKey)
        {
            List<GameInningTeamBatterDto> gameInningTeamBatters = new List<GameInningTeamBatterDto>();

            Connection.Open();
            gameInningTeamBatters.AddRange(Connection.Query<GameInningTeamBatterDto>(
                @"SELECT
                a.GameInningTeamBatterId,
                a.GameInningTeamBatterAlternateKey,
                a.GameInningTeamAlternateKey,
                a.PlayerAlternateKey,
                a.Sequence,
                a.EventType,
                a.TargetEventType,
                a.RBIs, 
                a.ChangeDate,
                a.DeleteDate
                FROM GameInningTeamBatter a
                INNER JOIN GameInningTeam b
                    ON a.GameInningTeamAlternateKey = b.GameInningTeamAlternateKey
                INNER JOIN GameTeam c
                    ON b.GameTeamAlternateKey = c.GameTeamAlternateKey
                WHERE c.GameAlternateKey = @GameAlternateKey
                AND a.PlayerAlternateKey = @PlayerAlternateKey ",
                new { GameAlternateKey = gameAlternateKey.ToString(), PlayerAlternateKey = playerAlternateKey.ToString() }));
            Connection.Close();

            return gameInningTeamBatters;
        }


        public void AddNew(GameInningTeamBatterDto gameInningTeamBatters)
        {
            InsertGameInningTeamBatter(gameInningTeamBatters);
        }
        public void Update(GameInningTeamBatterDto gameInningTeamBatters)
        {
            UpdateGameInningTeamBatter(gameInningTeamBatters);
        }

        /// <summary>
        /// data layer will determine add vs update
        /// </summary>
        /// <param name="gameInningTeamBatter"></param>
        public void Save(GameInningTeamBatterDto gameInningTeamBatter)
        {
            if (ExistsInDb(gameInningTeamBatter)) Update(gameInningTeamBatter);
            else AddNew(gameInningTeamBatter);
        }


        private void InsertGameInningTeamBatter(GameInningTeamBatterDto gameInningTeamBatter)
        {
            string insertQuery = @"INSERT INTO GameInningTeamBatter
                    (GameInningTeamBatterAlternateKey, GameInningTeamAlternateKey, PlayerAlternateKey, Sequence, 
                    EventType, TargetEventType, RBIs, DeleteDate)
                    VALUES(
                        @GameInningTeamBatterAlternateKey, 
                        @GameInningTeamAlternateKey, 
                        @PlayerAlternateKey, 
                        @Sequence,
                        @EventType,
                        @TargetEventType,
                        @RBIs, 
                        @DeleteDate)";

            Connection.Open();
            Connection.Query(insertQuery, gameInningTeamBatter);
            Connection.Close();
        }
        private void UpdateGameInningTeamBatter(GameInningTeamBatterDto gameInningTeamBatter)
        {
            string updateQuery = @"UPDATE GameInningTeamBatter
            SET GameInningTeamBatterAlternateKey = @GameInningTeamBatterAlternateKey,
            PlayerAlternateKey = @PlayerAlternateKey, 
            EventType = @EventType,
            TargetEventType = @TargetEventType,
            RBIs = @RBIs, 
            DeleteDate = @DeleteDate
            WHERE GameInningTeamAlternateKey = @GameInningTeamAlternateKey AND Sequence = @Sequence";

            Connection.Open();
            Connection.Query(updateQuery, gameInningTeamBatter);
            Connection.Close();
        }


        public void Delete(Guid gameInningTeamAlternateKey, int sequence)
        {
            string deleteQuery = @"DELETE FROM GameInningTeamBatter WHERE GameInningTeamAlternateKey = @GameInningTeamAlternateKey AND Sequence = @Sequence";

            Connection.Open();
            Connection.Query(deleteQuery, new { GameInningTeamAlternateKey = gameInningTeamAlternateKey.ToString(), Sequence = sequence });
            Connection.Close();
        }


        public bool ExistsInDb(GameInningTeamBatterDto gameInningTeamBatter)
        {
            Connection.Open();

            var rows = Connection.Query<int>(@"SELECT COUNT(1) as 'Count' FROM GameInningTeamBatter 
            WHERE GameInningTeamAlternateKey = @GameInningTeamAlternateKey AND Sequence = @Sequence",
            new { gameInningTeamBatter.GameInningTeamAlternateKey, gameInningTeamBatter.Sequence });
            Connection.Close();

            return rows.First() > 0;
        }



        private const string SELECT_QUERY =
        @"SELECT 
            GameInningTeamBatterId, 
            GameInningTeamBatterAlternateKey, 
            GameInningTeamAlternateKey,
            PlayerAlternateKey, 
            Sequence, 
            EventType,
            TargetEventType, 
            RBIs, 
            ChangeDate, 
            DeleteDate 
        FROM GameInningTeamBatter ";

    }
}
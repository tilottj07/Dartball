using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SQLite;
using Dapper;
using Dartball.DataLayer.Device.Dto;

namespace Dartball.DataLayer.Device.Repository
{
    public class GameInningTeamBatterRepository : ConnectionBase
    {

        public List<GameInningTeamBatterDto> LoadAll()
        {
            List<GameInningTeamBatterDto> gameInningTeamBatters = new List<GameInningTeamBatterDto>();

            Connection.BeginTransaction();
            gameInningTeamBatters.AddRange(Connection.Query<GameInningTeamBatterDto>(SELECT_QUERY));
            Connection.Commit();

            return gameInningTeamBatters;
        }

        public GameInningTeamBatterDto LoadByCompositeKey(Guid gameInningTeamId, int sequence)
        {
            GameInningTeamBatterDto gameInningTeamBatter = null;
            Connection.BeginTransaction();

            var result = Connection.Query<GameInningTeamBatterDto>(
                SELECT_QUERY + " WHERE GameInningTeamId = @GameInningTeamId " +
                "AND Sequence = @Sequence",
                new { GameInningTeamId = gameInningTeamId.ToString(), Sequence = sequence });

            gameInningTeamBatter = result.FirstOrDefault();

            Connection.Commit();

            return gameInningTeamBatter;
        }

        public List<GameInningTeamBatterDto> LoadByGameInningTeamId(Guid gameInningTeamId)
        {
            List<GameInningTeamBatterDto> gameInningTeamBatters = new List<GameInningTeamBatterDto>();

            Connection.BeginTransaction();
            gameInningTeamBatters.AddRange(Connection.Query<GameInningTeamBatterDto>(
                SELECT_QUERY + " WHERE GameInningTeamId = @GameInningTeamId ",
                new { GameInningTeamId = gameInningTeamId.ToString() }));
            Connection.Commit();

            return gameInningTeamBatters;
        }

        public List<GameInningTeamBatterDto> LoadByPlayerId(Guid playerId)
        {
            List<GameInningTeamBatterDto> gameInningTeamBatters = new List<GameInningTeamBatterDto>();

            Connection.BeginTransaction();
            gameInningTeamBatters.AddRange(Connection.Query<GameInningTeamBatterDto>(
                SELECT_QUERY + " WHERE PlayerId = @PlayerId ",
                new { PlayerId = playerId.ToString() }));
            Connection.Commit();

            return gameInningTeamBatters;
        }


        public List<GameInningTeamBatterDto> LoadByGameIdPlayerId(Guid gameId, Guid playerId)
        {
            List<GameInningTeamBatterDto> gameInningTeamBatters = new List<GameInningTeamBatterDto>();

            Connection.BeginTransaction();
            gameInningTeamBatters.AddRange(Connection.Query<GameInningTeamBatterDto>(
                @"SELECT
                a.GameInningTeamBatterId,
                a.GameInningTeamBatterId,
                a.GameInningTeamId,
                a.PlayerId,
                a.Sequence,
                a.EventType,
                a.TargetEventType,
                a.RBIs, 
                a.ChangeDate,
                a.DeleteDate
                FROM GameInningTeamBatter a
                INNER JOIN GameInningTeam b
                    ON a.GameInningTeamId = b.GameInningTeamId
                INNER JOIN GameTeam c
                    ON b.GameTeamId = c.GameTeamId
                WHERE c.GameId = @GameId
                AND a.PlayerId = @PlayerId ",
                new { GameId = gameId.ToString(), PlayerId = playerId.ToString() }));
            Connection.Commit();

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
                    (GameInningTeamBatterId, GameInningTeamId, PlayerId, Sequence, 
                    EventType, TargetEventType, RBIs, DeleteDate)
                    VALUES(
                        @GameInningTeamBatterId, 
                        @GameInningTeamId, 
                        @PlayerId, 
                        @Sequence,
                        @EventType,
                        @TargetEventType,
                        @RBIs, 
                        @DeleteDate)";

            Connection.BeginTransaction();
            Connection.Execute(insertQuery, gameInningTeamBatter);
            Connection.Commit();
        }
        private void UpdateGameInningTeamBatter(GameInningTeamBatterDto gameInningTeamBatter)
        {
            string updateQuery = @"UPDATE GameInningTeamBatter
            SET GameInningTeamBatterId = @GameInningTeamBatterId,
            PlayerId = @PlayerId, 
            EventType = @EventType,
            TargetEventType = @TargetEventType,
            RBIs = @RBIs, 
            DeleteDate = @DeleteDate
            WHERE GameInningTeamId = @GameInningTeamId AND Sequence = @Sequence";

            Connection.BeginTransaction();
            Connection.Execute(updateQuery, gameInningTeamBatter);
            Connection.Commit();
        }


        public void Delete(Guid gameInningTeamId, int sequence)
        {
            string deleteQuery = @"DELETE FROM GameInningTeamBatter WHERE GameInningTeamId = @GameInningTeamId AND Sequence = @Sequence";

            Connection.BeginTransaction();
            Connection.Execute(deleteQuery, new { GameInningTeamId = gameInningTeamId.ToString(), Sequence = sequence });
            Connection.Commit();
        }


        public bool ExistsInDb(GameInningTeamBatterDto gameInningTeamBatter)
        {
            Connection.BeginTransaction();

            var rows = Connection.Query<int>(@"SELECT COUNT(1) as 'Count' FROM GameInningTeamBatter 
            WHERE GameInningTeamId = @GameInningTeamId AND Sequence = @Sequence",
            new { gameInningTeamBatter.GameInningTeamId, gameInningTeamBatter.Sequence });
            Connection.Commit();

            return rows.First() > 0;
        }



        private const string SELECT_QUERY =
        @"SELECT 
            GameInningTeamBatterId, 
            GameInningTeamBatterId, 
            GameInningTeamId,
            PlayerId, 
            Sequence, 
            EventType,
            TargetEventType, 
            RBIs, 
            ChangeDate, 
            DeleteDate 
        FROM GameInningTeamBatter ";

    }
}
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using SQLite;
using Dartball.DataLayer.Device.Dto;
using System.Linq;

namespace Dartball.DataLayer.Device.Repository
{
    public class GameRepository : ConnectionBase
    {



        public List<GameDto> LoadAll()
        {
            List<GameDto> games = new List<GameDto>();
            Connection.BeginTransaction();

            games.AddRange(Connection.Query<GameDto>(SELECT_QUERY));

            Connection.Commit();

            return games;
        }

        public GameDto LoadByKey(Guid gameAlternateKey)
        {
            GameDto game = null;
            Connection.BeginTransaction();

            var result = Connection.Query<GameDto>(
                SELECT_QUERY + " where GameAlternateKey = @GameAlternateKey",
                new { GameAlternateKey = gameAlternateKey.ToString() });

            game = result.FirstOrDefault();

            Connection.Commit();

            return game;
        }

        public List<GameDto> LoadByLeagueAlternateKey(Guid leagueAlternateKey)
        {
            List<GameDto> games = new List<GameDto>();
            Connection.BeginTransaction();

            games.AddRange(Connection.Query<GameDto>(
                SELECT_QUERY + " where LeagueAlternateKey = @LeagueAlternateKey",
                new { LeagueAlternateKey = leagueAlternateKey.ToString() }));

            Connection.Commit();

            return games;
        }



        public void AddNew(GameDto game)
        {
            InsertGame(game);
        }
        public void Update(GameDto game)
        {
            UpdateGame(game);
        }

        /// <summary>
        /// data layer will determine add vs update
        /// </summary>
        /// <param name="game"></param>
        public void Save(GameDto game)
        {
            if (ExistsInDb(game)) Update(game);
            else AddNew(game);
        }


        private void InsertGame(GameDto game)
        {
            string insertQuery = @"INSERT INTO Game
                    (GameAlternateKey, LeagueAlternateKey, GameDate, DeleteDate)
                    VALUES(
                            @GameAlternateKey, 
                            @LeagueAlternateKey, 
                            @GameDate, 
                            @DeleteDate)";

            Connection.BeginTransaction();
            Connection.Execute(insertQuery, game);
            Connection.Commit();
        }
        private void UpdateGame(GameDto game)
        {
            string updateQuery = @"UPDATE Game
            SET LeagueAlternateKey = @LeagueAlternateKey,
            GameDate = @GameDate,
            DeleteDate = @DeleteDate
            WHERE GameAlternateKey = @GameAlternateKey";

            Connection.BeginTransaction();
            Connection.Execute(updateQuery, game);
            Connection.Commit();
        }


        public void Delete(Guid gameAlternateKey)
        {
            string deleteQuery = @"DELETE FROM Game WHERE GameAlternateKey = @GameAlternateKey";

            Connection.BeginTransaction();
            Connection.Execute(deleteQuery, new { GameAlternateKey = gameAlternateKey.ToString() });
            Connection.Commit();
        }


        public bool ExistsInDb(GameDto game)
        {
            Connection.BeginTransaction();

            var rows = Connection.Query<int>(@"SELECT COUNT(1) as 'Count' FROM Game WHERE GameAlternateKey = @GameAlternateKey", new { game.GameAlternateKey });
            Connection.Commit();

            return rows.First() > 0;
        }




        private const string SELECT_QUERY = @"SELECT 
                                                GameId, 
                                                GameAlternateKey,
                                                LeagueAlternateKey, 
                                                GameDate, 
                                                ChangeDate, 
                                                DeleteDate
                                            FROM Game";


    }
}
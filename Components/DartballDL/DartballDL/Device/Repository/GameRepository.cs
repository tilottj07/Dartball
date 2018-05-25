using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data.SQLite;
using Dartball.DataLayer.Device.Dto;
using System.Linq;

namespace Dartball.DataLayer.Device.Repository
{
    public class GameRepository : ConnectionBase
    {



        public List<GameDto> LoadAll()
        {
            List<GameDto> games = new List<GameDto>();
            Connection.Open();

            games.AddRange(Connection.Query<GameDto>(SELECT_QUERY));

            Connection.Close();

            return games;
        }

        public GameDto LoadByKey(Guid gameAlternateKey)
        {
            GameDto game = null;
            Connection.Open();

            var result = Connection.Query<GameDto>(
                SELECT_QUERY + " where GameAlternateKey = @GameAlternateKey",
                new { GameAlternateKey = gameAlternateKey.ToString() });

            game = result.FirstOrDefault();

            Connection.Close();

            return game;
        }

        public List<GameDto> LoadByLeagueAlternateKey(Guid leagueAlternateKey)
        {
            List<GameDto> games = new List<GameDto>();
            Connection.Open();

            games.AddRange(Connection.Query<GameDto>(
                SELECT_QUERY + " where LeagueAlternateKey = @LeagueAlternateKey",
                new { LeagueAlternateKey = leagueAlternateKey.ToString() }));

            Connection.Close();

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

            Connection.Open();
            Connection.Query(insertQuery, game);
            Connection.Close();
        }
        private void UpdateGame(GameDto game)
        {
            string updateQuery = @"UPDATE Game
            SET LeagueAlternateKey = @LeagueAlternateKey,
            GameDate = @GameDate,
            DeleteDate = @DeleteDate
            WHERE GameAlternateKey = @GameAlternateKey";

            Connection.Open();
            Connection.Query(updateQuery, game);
            Connection.Close();
        }


        public void Delete(Guid gameAlternateKey)
        {
            string deleteQuery = @"DELETE FROM Game WHERE GameAlternateKey = @GameAlternateKey";

            Connection.Open();
            Connection.Query(deleteQuery, new { GameAlternateKey = gameAlternateKey.ToString() });
            Connection.Close();
        }


        public bool ExistsInDb(GameDto game)
        {
            Connection.Open();

            var rows = Connection.Query<int>(@"SELECT COUNT(1) as 'Count' FROM Game WHERE GameAlternateKey = @GameAlternateKey", new { game.GameAlternateKey });
            Connection.Close();

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
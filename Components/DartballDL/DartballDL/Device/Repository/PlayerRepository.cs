using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using SQLite;
using System.Linq;
using Dartball.DataLayer.Device.Dto;

namespace Dartball.DataLayer.Device.Repository
{
    public class PlayerRepository : ConnectionBase
    {

        public List<PlayerDto> LoadAll()
        {
            List<PlayerDto> teams = new List<PlayerDto>();

            Connection.BeginTransaction();
            teams.AddRange(Connection.Query<PlayerDto>(SELECT_QUERY));
            Connection.Commit();

            return teams;
        }

        public PlayerDto LoadByKey(Guid playerAlternateKey)
        {
            PlayerDto player = null;
            Connection.BeginTransaction();

            var result = Connection.Query<PlayerDto>(
                SELECT_QUERY + " WHERE PlayerAlternateKey = @PlayerAlternateKey",
                new { PlayerAlternateKey = playerAlternateKey.ToString() });

            player = result.FirstOrDefault();

            Connection.Commit();

            return player;
        }

        //public List<PlayerDto> LoadByLeagueKey(Guid leagueAlternateKey)
        //{
        //    List<PlayerDto> teams = new List<PlayerDto>();
        //    Connection.BeginTransaction();

        //    teams.AddRange(Connection.Query<PlayerDto>(
        //        SELECT_QUERY + " WHERE LeagueAlternateKey = @LeagueAlternateKey",
        //        new { LeagueAlternateKey = leagueAlternateKey.ToString() }));


        //    Connection.Commit();

        //    return teams;
        //}



        public void AddNew(PlayerDto player)
        {
            InsertPlayer(player);
        }
        public void Update(PlayerDto player)
        {
            UpdatePlayer(player);
        }

        /// <summary>
        /// data layer will determine add vs update
        /// </summary>
        /// <param name="league"></param>
        public void Save(PlayerDto team)
        {
            if (ExistsInDb(team)) Update(team);
            else AddNew(team);
        }


        private void InsertPlayer(PlayerDto player)
        {
            string insertQuery = @"INSERT INTO Player
                    (PlayerAlternateKey, Name, Photo, EmailAddress, UserName, Password, ShouldSync, DeleteDate)
                    VALUES(
                        @PlayerAlternateKey, 
                        @Name, 
                        @Photo, 
                        @EmailAddress, 
                        @UserName, 
                        @Password, 
                        @ShouldSync, 
                        @DeleteDate)";

            Connection.BeginTransaction();
            Connection.Execute(insertQuery, player);
            Connection.Commit();
        }
        private void UpdatePlayer(PlayerDto player)
        {
            string updateQuery = @"UPDATE Player
            SET Name = @Name,
            Photo = @Photo,
            EmailAddress = @EmailAddress,
            UserName = @UserName,
            Password = @Password, 
            ShouldSync = @ShouldSync
            WHERE PlayerAlternateKey = @PlayerAlternateKey";

            Connection.BeginTransaction();
            Connection.Execute(updateQuery, player);
            Connection.Commit();
        }


        public void Delete(Guid playerAlternateKey)
        {
            string deleteQuery = @"DELETE FROM Player WHERE PlayerAlternateKey = @PlayerAlternateKey";

            Connection.BeginTransaction();
            Connection.Execute(deleteQuery, new { PlayerAlternateKey = playerAlternateKey.ToString() });
            Connection.Commit();
        }


        public bool ExistsInDb(PlayerDto player)
        {
            Connection.BeginTransaction();

            var rows = Connection.Query<int>(@"SELECT COUNT(1) as 'Count' FROM Player WHERE PlayerAlternateKey = @PlayerAlternateKey", new { PlayerAlternateKey = player.PlayerAlternateKey });
            Connection.Commit();

            return rows.First() > 0;
        }



        private const string SELECT_QUERY =
        @"SELECT PlayerId, PlayerAlternateKey, Name, EmailAddress, UserName, Password, ShouldSync, ChangeDate, DeleteDate
        FROM Player ";

    }
}
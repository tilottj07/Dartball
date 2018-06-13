using Dartball.DataLayer.Device.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using SQLite;
using System.Linq;

namespace Dartball.DataLayer.Device.Repository
{
    public class TeamRepository : ConnectionBase
    {

        public List<TeamDto> LoadAll()
        {
            List<TeamDto> teams = new List<TeamDto>();

            Connection.BeginTransaction();
            teams.AddRange(Connection.Query<TeamDto>(SELECT_QUERY));
            Connection.Commit();

            return teams;
        }

        public TeamDto LoadByKey(Guid teamAlternateKey)
        {
            TeamDto team = null;
            Connection.BeginTransaction();

            var result = Connection.Query<TeamDto>(
                SELECT_QUERY + " WHERE TeamAlternateKey = @TeamAlternateKey",
                new { TeamAlternateKey = teamAlternateKey.ToString() });

            team = result.FirstOrDefault();

            Connection.Commit();

            return team;
        }

        public List<TeamDto> LoadByLeagueKey(Guid leagueAlternateKey)
        {
            List<TeamDto> teams = new List<TeamDto>();
            Connection.BeginTransaction();

            teams.AddRange(Connection.Query<TeamDto>(
                SELECT_QUERY + " WHERE LeagueAlternateKey = @LeagueAlternateKey",
                new { LeagueAlternateKey = leagueAlternateKey.ToString() }));


            Connection.Commit();

            return teams;
        }



        public void AddNew(TeamDto team)
        {
            InsertTeam(team);
        }
        public void Update(TeamDto team)
        {
            UpdateTeam(team);
        }

        /// <summary>
        /// data layer will determine add vs update
        /// </summary>
        /// <param name="league"></param>
        public void Save(TeamDto team)
        {
            if (ExistsInDb(team)) Update(team);
            else AddNew(team);
        }


        private void InsertTeam(TeamDto team)
        {
            string insertQuery = @"INSERT INTO Team
                    (TeamAlternateKey, LeagueAlternateKey, Name, Password, Handicap, ShouldSync, DeleteDate)
                    VALUES(
                        @TeamAlternateKey, 
                        @LeagueAlternateKey, 
                        @Name, 
                        @Password, 
                        @Handicap, 
                        @ShouldSync, 
                        @DeleteDate)";

            Connection.BeginTransaction();
            Connection.Execute(insertQuery, team);
            Connection.Commit();
        }
        private void UpdateTeam(TeamDto team)
        {
            string updateQuery = @"UPDATE Team
            SET LeagueAlternateKey = @LeagueAlternateKey,
            Name = @Name,
            Password = @Password,
            Handicap = @Handicap,
            ShouldSync = @ShouldSync
            WHERE TeamAlternateKey = @TeamAlternateKey";

            Connection.BeginTransaction();
            Connection.Execute(updateQuery, team);
            Connection.Commit();
        }


        public void Delete(Guid teamAlternateKey)
        {
            string deleteQuery = @"DELETE FROM Team WHERE TeamAlternateKey = @TeamAlternateKey";

            Connection.BeginTransaction();
            Connection.Execute(deleteQuery, new { TeamAlternateKey = teamAlternateKey.ToString() });
            Connection.Commit();
        }


        public bool ExistsInDb(TeamDto team)
        {
            Connection.BeginTransaction();

            var rows = Connection.Query<int>(@"SELECT COUNT(1) as 'Count' FROM Team WHERE TeamAlternateKey = @TeamAlternateKey", new { TeamAlternateKey = team.TeamAlternateKey });
            Connection.Commit();

            return rows.First() > 0;
        }



        private const string SELECT_QUERY = 
        @"SELECT TeamId, TeamAlternateKey, LeagueAlternateKey, Name, Password, Handicap, ShouldSync, ChangeDate, DeleteDate 
        from Team ";

    }
}

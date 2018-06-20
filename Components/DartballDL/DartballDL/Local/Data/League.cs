using Dartball.DataLayer.Local.Shared;
using System;
using System.Collections.Generic;

namespace Dartball.DataLayer.Local.Data
{
    public class League : ChangeTracker
    {

        public int LeagueId { get; set; }
        public string LeagueId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public List<Team> Teams { get; set; }
        public List<Game> Games { get; set; }

    }
}

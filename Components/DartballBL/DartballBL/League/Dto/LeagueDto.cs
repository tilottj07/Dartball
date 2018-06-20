using Dartball.BusinessLayer.League.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.League.Dto
{
    public class LeagueDto : ILeague
    {

        public Guid LeagueId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

    }
}

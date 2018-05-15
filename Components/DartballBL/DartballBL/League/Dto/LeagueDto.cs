using DartballBL.League.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartballBL.League.Dto
{
    public class LeagueDto : ILeague
    {

        public int LeagueId { get; set; }
        public string Name { get; set; }


    }
}

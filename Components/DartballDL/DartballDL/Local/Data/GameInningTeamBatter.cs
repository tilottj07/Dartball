﻿using Dartball.DataLayer.Local.Shared;
using System;

namespace Dartball.DataLayer.Local.Data
{
    public class GameInningTeamBatter : ChangeTracker
    {

        public int GameInningTeamBatterId { get; set; }
        public string GameInningTeamBatterId { get; set; }
        public string GameInningTeamId { get; set; }
        public string PlayerId { get; set; }
        public int Sequence { get; set; }
        public int EventType { get; set; }
        public int? TargetEventType { get; set; }
        public int RBIs { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public GameInningTeam GameInningTeam { get; set; }
        public Player Player { get; set; }

    }
}

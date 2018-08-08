using System;
using Dartball.BusinessLayer.Game.Interface.Models;

namespace DartballApp.Models
{
    public class GameInningTeamBatter
    {
        public GameInningTeamBatter(Guid gameInningTeamId, Guid playerId, int sequence)
        {
            GameInningTeamId = gameInningTeamId;
            PlayerId = playerId;
            Sequence = sequence;
            HasData = true;
        }

        public GameInningTeamBatter(IGameInningTeamBatter gameInningTeamBatter) {
            if (gameInningTeamBatter != null) {
                GameInningTeamBatterId = gameInningTeamBatter.GameInningTeamBatterId;
                GameInningTeamId = gameInningTeamBatter.GameInningTeamId;
                PlayerId = gameInningTeamBatter.PlayerId;
                Sequence = gameInningTeamBatter.Sequence;
                EventType = gameInningTeamBatter.EventType;
                TargetEventType = gameInningTeamBatter.TargetEventType;
                RBIs = gameInningTeamBatter.RBIs;
                HasData = true;
            }
        }

        public bool HasData { get; set; }

        public Guid GameInningTeamBatterId { get; set; }
        public Guid GameInningTeamId { get; set; }
        public Guid PlayerId { get; set; }
        public int Sequence { get; set; }
        public int EventType { get; set; }
        public int? TargetEventType { get; set; }
        public int RBIs { get; set; }
    }
}

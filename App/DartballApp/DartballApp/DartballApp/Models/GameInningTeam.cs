using System;
using Dartball.BusinessLayer.Game.Interface.Models;

namespace DartballApp.Models
{
    public class GameInningTeam
    {
        public GameInningTeam(Guid gameInningId, Guid gameTeamId)
        {
            GameInningId = gameInningId;
            GameTeamId = gameTeamId;
        }

        public GameInningTeam(IGameInningTeam gameInningTeam) {
            if (gameInningTeam != null) {
                GameInningTeamId = gameInningTeam.GameInningTeamId;
                GameInningId = gameInningTeam.GameInningId;
                GameTeamId = gameInningTeam.GameTeamId;
                Score = gameInningTeam.Score;
                Outs = gameInningTeam.Outs;
                IsRunnerOnFirst = gameInningTeam.IsRunnerOnFirst;
                IsRunnerOnSecond = gameInningTeam.IsRunnerOnSecond;
                IsRunnerOnThird = gameInningTeam.IsRunnerOnThird;
            }
        }

        public Guid GameInningTeamId { get; set; }
        public Guid GameInningId { get; set; }
        public Guid GameTeamId { get; set; }
        public int Score { get; set; }
        public int Outs { get; set; }
        public bool IsRunnerOnFirst { get; set; }
        public bool IsRunnerOnSecond { get; set; }
        public bool IsRunnerOnThird { get; set; }
    }
}

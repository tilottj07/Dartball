using System;
using Dartball.BusinessLayer.Player.Interface.Models;

namespace DartballApp.Models
{
    public class PlayerTeam
    {
        public PlayerTeam() { }

        public PlayerTeam(IPlayerTeam playerTeam)
        {
            if (playerTeam != null) {
                PlayerId = playerTeam.PlayerId;
                TeamId = playerTeam.TeamId;
            }
        }


        public Guid PlayerId { get; set; }
        public Guid TeamId { get; set; }


    }
}

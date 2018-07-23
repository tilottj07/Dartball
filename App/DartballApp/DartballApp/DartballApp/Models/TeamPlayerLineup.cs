using System;
using Dartball.BusinessLayer.Team.Interface.Models;

namespace DartballApp.Models
{
    public class TeamPlayerLineup
    {
        public TeamPlayerLineup(){}

        public TeamPlayerLineup(ITeamPlayerLineup teamPlayerLineup)
        {
            if (teamPlayerLineup != null) {
                TeamId = teamPlayerLineup.TeamId;
                PlayerId = teamPlayerLineup.PlayerId;
                BattingOrder = teamPlayerLineup.BattingOrder;
            }
        }

        public Guid TeamId { get; set; }
        public Guid PlayerId { get; set; }
        public int BattingOrder { get; set; }

    }
}

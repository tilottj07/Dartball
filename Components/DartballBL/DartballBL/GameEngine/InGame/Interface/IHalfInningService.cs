using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.GameEngine.Event.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.GameEngine.InGame.Interface
{
    public interface IHalfInningService
    {

        IHalfInningActions GetHalfInningActions(List<IGameInningTeamBatter> gameInningTeamBatters);
        IHalfInningActions GetHalfInningActions(Guid gameInningTeamId);

    }
}

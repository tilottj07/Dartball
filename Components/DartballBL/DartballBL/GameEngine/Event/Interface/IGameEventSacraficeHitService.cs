using Dartball.BusinessLayer.GameEngine.Event.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.GameEngine.Event.Interface
{
    public interface IGameEventSacraficeHitService
    {

        IHalfInningActions FillSacraficeHitActions(IHalfInningActions actions);

    }
}

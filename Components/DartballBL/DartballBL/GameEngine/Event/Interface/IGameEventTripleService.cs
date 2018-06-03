using Dartball.BusinessLayer.GameEngine.Event.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.GameEngine.Event.Interface
{
    public interface IGameEventTripleService
    {

        IHalfInningActions FillTripleActions(IHalfInningActions actions);

    }
}

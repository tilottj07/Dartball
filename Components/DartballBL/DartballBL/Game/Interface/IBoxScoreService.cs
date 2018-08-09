using System;
using Dartball.BusinessLayer.Game.Interface.Models;

namespace Dartball.BusinessLayer.Game.Interface
{
    public interface IBoxScoreService
    {


        IBoxScore GetBoxScore(Guid gameId);

    }
}

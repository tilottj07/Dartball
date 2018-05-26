using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Interface
{
    public interface IGameInningService
    {

        IGameInning GetGameInning(Guid gameAlternateKey, int inningNumber);
        List<IGameInning> GetGameInnings(Guid gameAlternateKey);
        List<IGameInning> GetAllGameInnings();

        ChangeResult AddNew(IGameInning gameInning);
        ChangeResult AddNew(List<IGameInning> gameInnings);

        ChangeResult Update(IGameInning gameInning);
        ChangeResult Update(List<IGameInning> gameInnings);

        ChangeResult Remove(Guid gameAlternateKey, int inningNumber);

    }
}

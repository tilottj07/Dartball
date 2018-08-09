using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Game.Interface
{
    public interface IGameInningService
    {

        IGameInning GetGameInning(Guid gameInningId);
        IGameInning GetGameInning(Guid gameId, int inningNumber);
        List<IGameInning> GetGameInnings(Guid gameId);
        List<IGameInning> GetAllGameInnings();

        IGameInning GetCurrentGameInning(Guid gameId);
        int GetNextGameInningNumber(Guid gameId);


        ChangeResult Save(IGameInning gameInning);

        ChangeResult AddNew(IGameInning gameInning);
        ChangeResult AddNew(List<IGameInning> gameInnings);

        ChangeResult Update(IGameInning gameInning);
        ChangeResult Update(List<IGameInning> gameInnings);

        ChangeResult Remove(Guid gameId, int inningNumber);

    }
}

using Dartball.BusinessLayer.Player.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Player.Interface
{
    public interface IPlayerService
    {
        IPlayer GetPlayer(Guid playerId);
        List<IPlayer> GetPlayers();
        List<IPlayer> GetPlayers(List<Guid> playerIds);

        ChangeResult Save(IPlayer player);

        ChangeResult AddNew(IPlayer player);
        ChangeResult AddNew(List<IPlayer> players);

        ChangeResult Update(IPlayer player);
        ChangeResult Update(List<IPlayer> players);

        ChangeResult Remove(Guid playerId);

    }
}

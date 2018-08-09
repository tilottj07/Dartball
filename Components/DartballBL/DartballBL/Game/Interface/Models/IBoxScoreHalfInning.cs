using System;
namespace Dartball.BusinessLayer.Game.Interface.Models
{
    public interface IBoxScoreHalfInning
    {
        string TeamName { get; }
        int? Score { get; }

    }
}

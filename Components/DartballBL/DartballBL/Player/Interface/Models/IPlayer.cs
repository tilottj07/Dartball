﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Player.Interface.Models
{
    public interface IPlayer
    {
        int PlayerId { get; }
        Guid PlayerAlternateKey { get; }
        string Name { get; }
        byte[] Photo { get; }
        string EmailAddress { get; }
        string UserName { get; }
        string Password { get; }
        bool ShouldSync { get; }
        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }

    }
}

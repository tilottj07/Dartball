using Dartball.BusinessLayer.Player.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Player.Dto
{
    public class PlayerDto : IPlayer
    {

        public Guid PlayerId { get; set; }
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool ShouldSync { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

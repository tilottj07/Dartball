using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.DataLayer.Device.Dto
{
    public class PlayerDto
    {

        public int PlayerId { get; set; }
        public string PlayerId { get; set; }
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int ShouldSync { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }


    }
}

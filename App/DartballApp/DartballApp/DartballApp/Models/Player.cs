using System;
using System.Collections.Generic;
using System.Text;

namespace DartballApp.Models
{
    public class Player
    {

        public Guid PlayerId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public bool IsAddNew { get; set; }

    }
}

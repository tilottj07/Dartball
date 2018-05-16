using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Shared.Models
{
    public class ChangeResult
    {
        public ChangeResult()
        {
            IsSuccess = true;
            ErrorMessages = new List<string>();
        }

        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }

    }
}

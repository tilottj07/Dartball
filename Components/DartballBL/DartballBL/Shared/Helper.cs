using System;
using System.Collections.Generic;
using System.Text;

namespace Dartball.BusinessLayer.Shared
{
    public class Helper
    {

        public static string CleanString(string val)
        {
            if (string.IsNullOrWhiteSpace(val)) val = string.Empty;
            return val.Trim();
        }


        public static decimal SafeDivide(decimal numerator, decimal denominator)
        {
            decimal val = 0;
            if (denominator != 0)
            {
                val = numerator / denominator;
            }
            return val;
        }


        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}

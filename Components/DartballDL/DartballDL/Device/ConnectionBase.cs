using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Text;

namespace Dartball.DataLayer.Device
{
    public class ConnectionBase
    {
        public ConnectionBase()
        {
            Connection = new SQLiteConnection($"Data Source = C:\\TJT\\Dartball\\Database\\Dartball.db");

        }

        public SQLiteConnection Connection { get; private set; }





    }
}

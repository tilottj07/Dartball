using System;
using SQLite;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Dartball.DataLayer.Device
{
    public class ConnectionBase
    {
        public ConnectionBase()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            path = Path.Combine(path, "Dartball.db3");

            Connection = new SQLiteConnection($"Data Source = {path};");

        }

        protected SQLiteConnection Connection { get; private set; }





    }
}

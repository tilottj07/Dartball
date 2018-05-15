using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartballDL.Device
{
    public class ConnectionBase
    {
        public ConnectionBase()
        {
            Connection = new SQLiteConnection(
                $"Data Source={Environment.CurrentDirectory}\\Database\\Dartball.db.sqlite");


        }

        public SQLiteConnection Connection { get; private set; }


        public void ExecuteNonQuery(string commandText, object param = null)
        {
            // Ensure we have a connection
            if (Connection == null)
            {
                throw new NullReferenceException(
                    "Please provide a connection");
            }


            // Use Dapper to execute the given query
            Connection.Execute(commandText, param);
        }


    }
}

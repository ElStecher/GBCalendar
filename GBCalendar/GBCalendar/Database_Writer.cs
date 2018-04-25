using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace GBCalendar
{
    class Database_Writer
    {

        public void Write(string Command)
        {
            try
            {
                //instanzierung
                Database_Connector Connect = new Database_Connector();
                Connect.OpenConnection();

                MySqlCommand command = Connect.Connection.CreateCommand();
                command.CommandText = Command;
                command.ExecuteNonQuery();
                Connect.CloseConnection();

            }
            catch (Exception)
            {
                throw;
            }
            
        }

    }
}

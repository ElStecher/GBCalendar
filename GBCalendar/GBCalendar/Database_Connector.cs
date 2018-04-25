using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;



namespace GBCalendar
{
    class Database_Connector
    {

        private MySqlConnection connection;

        //Schreibgeschützt
        public MySqlConnection Connection
        {
            get
            {
                return connection;
            }
        }

        //Folgende Eigenschaften dürfen nicht Gelesen werden, daher wurde keine Implementiert
        private string server = "maissen.internet-box.ch";
        private string database = "Prototype_GBCDrive";
        private string user = "App_User";
        private string port = "2100";
        private string password = "!KalenderApp_User100";
        private string connectionString;


        
        public void OpenConnection()
        {
            try
            {
                connectionString = "SERVER=" + server + ";" + "PORT=" + port + ";" + "DATABASE=" +
                database + ";" + "UID=" + user + ";" + "PASSWORD=" + password + ";";
                connection = new MySqlConnection(connectionString);
                connection.Open();

            }
            catch (Exception ex)
            {
                throw new Exception("Fehler bei der Datenbankverbindung: " + ex.Message.ToString());
            }
        }

        public void CloseConnection()
        {
            try
            {
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Fehler bei der Datenbankverbindung: " +  ex.Message.ToString());
            }
            
        }


    }
}

using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace GBCalendar
{
    class Database
    {
        private MySqlConnection connection;
        private string server = "maissen.internet-box.ch";
        private string database = "Prototype_GBCDrive";
        private string user = "App_User";
        private string port = "2100";
        private string password = "!KalenderApp_User100";
        private string connectionString;


        private void OpenConnection()
        {
            connectionString = "SERVER=" + server + ";" + "PORT=" + port + ";" + "DATABASE=" +
            database + ";" + "UID=" + user + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            connection.Open();

        }


        public void Write(string p_command)
        {    
                OpenConnection(); //aufrufen der Connect Funktion
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = p_command;
                command.ExecuteNonQuery();
                connection.Close(); 
        }


        // chunnt no
        public void Read(string p_command)
        { 
            OpenConnection(); //aufrufen der Connect Funktion
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = p_command;
            //READ LOGIK
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Object[] values = new Object[reader.FieldCount];
                int fieldCount = reader.GetValues(values);

                for (int i = 0; i < fieldCount; i++)
                {
                    Console.WriteLine(values[i]);

                }


            }   


        }


    }
}

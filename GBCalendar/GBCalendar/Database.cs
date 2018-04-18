﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Windows.Forms;

namespace TEST
{
    class Database
    {
        private MySqlConnection connection;
        private string server = "maissen.internet-box.ch";
        private string database = "Prototype_GBCDrive";
        private string user = "IDPA";
        private string port = "2100";
        private string password = "2018fadasaIDPA";
        private string connectionString;


        private void OpenConnection()
        {
            try
            {
                connectionString = "SERVER=" + server + ";" + "PORT=" + port + ";" + "DATABASE=" +
                database + ";" + "UID=" + user + ";" + "PASSWORD=" + password + ";";
                connection = new MySqlConnection(connectionString);
                connection.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show("Problems with the Database connection:" + e);
                throw;
            }
        }

        public void write(string p_command)
        {
            try
            {
                OpenConnection(); //aufrufen der Connect Funktion
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = p_command;
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Problems with writing to the Database:" + e);
                throw;
            }
        }


        public ArrayList Read(string p_command)
        {
            try
            {
                OpenConnection(); //aufrufen der Connect Funktion
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = p_command;
                //READ LOGIK

                ArrayList dataArray = new ArrayList();

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

                return dataArray;
            }

            catch (Exception e)
            {
                MessageBox.Show("Datenbankserver nicht erreichbar");
                //return new ArrayList(); //??
            }
        }




    }
}
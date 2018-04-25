using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace GBCalendar
{
    class Database_Reader
    {

        /// <summary>
        /// Reads Classes from Teacher
        /// </summary>

        private List<Class> classlist = new List<Class>();

        public List<Class> ReadClass(int IdPerson)
        {
            try
            {
                //instanzierung
                Database_Connector Connect = new Database_Connector();
                Connect.OpenConnection();

                MySqlCommand command = Connect.Connection.CreateCommand();
                // query liest nur bestimmte Klassen einer Person Aus!
                command.CommandText = "SELECT Classname FROM Class WHERE idclass IN(SELECT Class_idClass FROM Class_has_Person WHERE Person_idPerson LIKE " + IdPerson + ");";

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    classlist.Add(new Class(reader.GetValue(0).ToString()));
                }

                reader.Close();

                //Connection schliessen
                Connect.CloseConnection();

                return classlist;

            }

            catch (Exception ex)
            {
                throw new Exception("Fehler beim lesen der Klassen: " + ex.Message.ToString());
            }
            
        }



        /// <summary>
        /// Reads All Rooms
        /// </summary>

        private List<Room> roomlist = new List<Room>();

        public List<Room> ReadRoom()
        {

            try
            {
                //instanzierung
                Database_Connector Connect = new Database_Connector();
                Connect.OpenConnection();

                MySqlCommand command = Connect.Connection.CreateCommand();
                // query liest nur bestimmte Klassen einer Person Aus!
                command.CommandText = "SELECT Roomname FROM Room;";

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    roomlist.Add(new Room(reader.GetValue(0).ToString()));
                }

                reader.Close();

                //Connection schliessen
                Connect.CloseConnection();

                return roomlist;


            }
            catch (Exception ex)
            {
                throw new Exception("Fehler beim lesen der Räume: " + ex.Message.ToString());
            }


        }

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using MySql.Data.MySqlClient;

namespace GBCalendar
{
    class DatabaseReader
    {
        #region Felder und Eigenschaften der Klasse DatabaseReader
        public List<SchoolClass> ClassList { get; private set; } = new List<SchoolClass>();
        public List<Room> roomList { get; private set; } = new List<Room>();

        private List<Appointment> appointmentList = new List<Appointment>();
        private List<string> userList = new List<string>();
        #endregion
        #region Methoden der Klasse DatabaseReader

        /// <summary>
        /// List alle Klassen für die angemeldete Person aus der Datenbank heraus
        /// </summary>
        /// <param name="IdPerson">ID der angemeldeten Person</param>
        /// <returns>Eine Liste aller Klassen auf welche die angemeldete Person Zugriff hat</returns>
        public List<SchoolClass> ReadClasses(int IdPerson)
        {
            try
            {
                // Instanziierung
                DatabaseConnector Connect = new DatabaseConnector();
                Connect.OpenConnection();

                MySqlCommand command = Connect.Connection.CreateCommand();
                // query liest nur bestimmte Klassen einer Person Aus!
                command.CommandText = "SELECT * FROM Class WHERE idclass IN(SELECT Class_idClass FROM Class_has_Person WHERE Person_idPerson LIKE " + IdPerson + ");";

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ClassList.Add(new SchoolClass((int)reader.GetValue(0), reader.GetValue(1).ToString()));
                }

                foreach(SchoolClass schoolClass in ClassList)
                {
                    schoolClass.AppointmentList = ReadAppointments(schoolClass);
                }

                reader.Close();

                //Connection schliessen
                Connect.CloseConnection();

                return ClassList;
            }
            catch (Exception e)
            {
                throw new Exception("Fehler beim lesen der Klassen: " + e.Message.ToString());
            }
            
        }

        
        /// <summary>
        /// List alle in der Datenbank vorhandenen Räume heraus und fügt diese zu einer Liste hinzu
        /// </summary>
        /// <returns>Liste aller vorhandenen Räume</returns>
        public List<Room> ReadRooms()
        {
            try
            {
                //instanzierung
                DatabaseConnector Connect = new DatabaseConnector();
                Connect.OpenConnection();

                MySqlCommand command = Connect.Connection.CreateCommand();
                // query liest nur bestimmte Klassen einer Person Aus!
                command.CommandText = "SELECT * FROM Room;";

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    roomList.Add(new Room((int)reader.GetValue(0),reader.GetValue(1).ToString()));
                }

                reader.Close();

                //Connection schliessen
                Connect.CloseConnection();

                return roomList;
            }
            catch (Exception ex)
            {
                throw new Exception("Fehler beim lesen der Räume: " + ex.Message.ToString());
            }
        }

        
        public Room ReadRoom(int idRoom)
        {

            try
            {
                string roomName = null;

                //instanzierung
                DatabaseConnector Connect = new DatabaseConnector();
                Connect.OpenConnection();

                MySqlCommand command = Connect.Connection.CreateCommand();
                // query liest nur bestimmte Klassen einer Person Aus!
                command.CommandText = "SELECT * FROM Room WHERE idRoom = " + idRoom + ";";

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    roomName = (string)reader.GetValue(1);
                }

                reader.Close();

                //Connection schliessen
                Connect.CloseConnection();

                Room room = new Room(idRoom, roomName);
                return room;
            }
            catch (Exception ex)
            {
                throw new Exception("Fehler beim lesen der Räume: " + ex.Message.ToString());
            }
        }


        /// <summary>
        /// List alle in der Datenbank Appointments heraus und fügt diese zu einer Liste hinzu
        /// </summary>
        /// <returns>Liste aller vorhandenen Appointments für Klasse</returns>
        public List<Appointment> ReadAppointments(SchoolClass schoolClass)
        {
            try
            {
                //instanzierung
                DatabaseConnector Connect = new DatabaseConnector();
                Connect.OpenConnection();

                MySqlCommand command = Connect.Connection.CreateCommand();
                // query liest nur bestimmte Klassen einer Person Aus!
                //command.CommandText = "SELECT * FROM Appointment WHERE Class_idClass=" + schoolClass.IdClass + ";";
                command.CommandText = "SELECT a.idAppointment, a.Title, a.Person_idPerson, a.Class_idClass, a.Room_idRoom, a.Start_Time, a.End_Time, a.Description, a.AllDayEvent, p.Name, p.Firstname FROM Person AS p, Appointment AS a WHERE p.Role_idRole = 1 AND p.IdPerson = a.Person_idPerson AND  Class_idClass=" + schoolClass.IdClass + ";";

                MySqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    //-- muss über Select abgefangen werden

                    DateTime endTimeObj = reader.GetDateTime(6);

                    if (endTimeObj > DateTime.Now)
                    {
                        string endTime = endTimeObj.ToString("dd.MM.yyyy HH:mm:ss");

                        DateTime startTimeObj = reader.GetDateTime(5);
                        string startTime = startTimeObj.ToString("dd.MM.yyyy HH:mm:ss");
                        
                        // Instanzierung Person
                        Person creator = new Person((int)reader.GetValue(2), (string)reader.GetValue(9), (string)reader.GetValue(10));

                        //Instanzierung Appointment
                        Appointment a = new Appointment((int)reader.GetValue(0), (string)reader.GetValue(1), ReadRoom((int)reader.GetValue(4)),
                          startTime, endTime, (string)reader.GetValue(8), (string)reader.GetValue(7), creator);

                        appointmentList.Add(a);
                    }
                }

                reader.Close();

                //Connection schliessen
                Connect.CloseConnection();

                return appointmentList;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                throw;
            }
        }

        /// <summary>
        /// Überprüft auf einen User in der Datenbank mit den übergebenen Parametern und übergibt diesen User falls vorhanden
        /// </summary>
        /// <param name="email">Mail des Users</param>
        /// <param name="password">Passwort des Users</param>
        /// <param name="idRole">Rolle des USers</param>
        /// <returns>user object</returns>
        public bool AreUserCredentialsCorrect(string email, string password, int idRole)
        {
            int idPerson = 0;
            string firstName = null;
            string lastName = null;
            //instanzierung
            DatabaseConnector Connect = new DatabaseConnector();
            Connect.OpenConnection();

            MySqlCommand command = Connect.Connection.CreateCommand();
            // query liest nur bestimmte Klassen einer Person Aus!
            command.CommandText = "SELECT * FROM Person WHERE Email = '" + email + "' AND Password = '" + password + "' AND Role_idRole = '" + idRole.ToString() + "';";

            MySqlDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                //userList.Add(reader.GetValue(0).ToString());
                idPerson = (int)reader.GetValue(0);
                firstName = (string)reader.GetValue(1);
                lastName = (string)reader.GetValue(2);
            }

            if (idPerson != 0)
            {
                App.UserLoggedIn = new Person(idPerson, firstName, lastName,  email, password, idRole);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}

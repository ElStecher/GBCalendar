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
        private List<string> userList { get; set; } = new List<string>();
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
                    ReadAppointments(schoolClass);
                }

                reader.Close();

                //Connection schliessen
                Connect.CloseConnection();

                return ClassList;
            }
            catch (Exception )
            {
                throw;
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
            catch (Exception e)
            {
                throw new Exception("Fehler beim lesen der Räume: " + e.Message.ToString());
            }
        }

       
        /// <summary>
        /// List alle in der Datenbank Appointments heraus und fügt diese zu einer Liste hinzu
        /// </summary>
        /// <returns>Liste aller vorhandenen Appointments für Klasse</returns>
        public void ReadAppointments(SchoolClass schoolClass)
        {

           

            try
            {
                // Liste säubern
                schoolClass.AppointmentList.Clear();

                // Instanzierung
                DatabaseConnector Connect = new DatabaseConnector();
                Connect.OpenConnection();

                MySqlCommand command = Connect.Connection.CreateCommand();

                // command.CommandText = "SELECT * FROM Appointment WHERE Class_idClass=" + schoolClass.IdClass + ";";
                // Liest alle Appointments aus, welche nach dem heutigen Datum stattfinden und den  dazugehörigen creator, sowie den Raum in dem das Appointment stattfindet
                command.CommandText = "SELECT a.idAppointment, a.Title, a.Person_idPerson, a.Class_idClass, a.Room_idRoom, a.Start_Time, a.End_Time, a.Description, a.AllDayEvent, p.Name, p.Firstname, r.Roomname " +
                    "FROM Person AS p, Appointment AS a, Room AS r " +
                    "WHERE p.Role_idRole = 1 AND p.IdPerson = a.Person_idPerson AND  Class_idClass=" + schoolClass.IdClass + " AND r.idRoom = a.Room_idRoom AND a.Start_Time >= curdate();";

                MySqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    // Zeiten auslesen und Formatieren
                    DateTime endTimeObj = reader.GetDateTime(6);
                    DateTime startTimeObj = reader.GetDateTime(5);
                        
                        
                    // Instanzierung Person
                    Person creator = new Person((int)reader.GetValue(2), (string)reader.GetValue(9), (string)reader.GetValue(10));

                    // Instanzierung Room
                    Room room = new Room((int)reader.GetValue(4), (string)reader.GetValue(11));

                    // Instanzierung Appointment
                    Appointment a = new Appointment((int)reader.GetValue(0), (string)reader.GetValue(1), room,
                        startTimeObj, endTimeObj, (string)reader.GetValue(8), (string)reader.GetValue(7), creator);

                    schoolClass.AppointmentList.Add(a);
                }

                reader.Close();

                //Connection schliessen
                Connect.CloseConnection();

  
            }
            catch (Exception)
            {
               
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

            // Instanzierung
            DatabaseConnector Connect = new DatabaseConnector();
            Connect.OpenConnection();
            try
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
                idPerson = (int)reader.GetValue(0);
                firstName = (string)reader.GetValue(1);
                lastName = (string)reader.GetValue(2);
            }

                if (idPerson != 0)
                {
                    App.UserLoggedIn = new Person(idPerson, firstName, lastName, email, password, idRole);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }    
        #endregion
    }
}

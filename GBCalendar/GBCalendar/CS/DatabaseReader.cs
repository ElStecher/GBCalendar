using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace GBCalendar
{
    class DatabaseReader
    {
        #region Felder und Eigenschaften der Klasse DatabaseReader
        private List<Class> classList = new List<Class>();
        private List<Room> roomList = new List<Room>();
        private List<Appointment> appointmentList = new List<Appointment>();
        private List<string> userList = new List<string>();

        public List<Class> ClassList
        {
            get
            {
                return this.classList;
            }
            private set
            {
                this.classList = ClassList;
            }
        }
        public List<Room> RoomList
        {
            get
            {
                return this.roomList;
            }
            private set
            {
                this.roomList = RoomList;
            }
        }
        #endregion
        // @Fabio ToDo: Schauen wie Singleton-Pattern genau umgesetzt wird und implementieren
        #region Methoden der Klasse DatabaseReader

        /// <summary>
        /// List alle Klassen für die angemeldete Person aus der Datenbank heraus
        /// </summary>
        /// <param name="IdPerson">ID der angemeldeten Person</param>
        /// <returns>Eine Liste aller Klassen auf welche die angemeldete Person Zugriff hat</returns>
        public List<Class> ReadClass(int IdPerson)
        {
            try
            {
                // Instanziierung
                DatabaseConnector Connect = new DatabaseConnector();
                Connect.OpenConnection();

                MySqlCommand command = Connect.Connection.CreateCommand();
                // query liest nur bestimmte Klassen einer Person Aus!
                command.CommandText = "SELECT Classname FROM Class WHERE idclass IN(SELECT Class_idClass FROM Class_has_Person WHERE Person_idPerson LIKE " + IdPerson + ");";

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    classList.Add(new Class(reader.GetValue(0).ToString()));
                }

                reader.Close();

                //Connection schliessen
                Connect.CloseConnection();

                return classList;
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
        public List<Room> ReadRoom()
        {
            try
            {
                //instanzierung
                DatabaseConnector Connect = new DatabaseConnector();
                Connect.OpenConnection();

                MySqlCommand command = Connect.Connection.CreateCommand();
                // query liest nur bestimmte Klassen einer Person Aus!
                command.CommandText = "SELECT Roomname FROM Room;";

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    roomList.Add(new Room(reader.GetValue(0).ToString()));
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



        /// <summary>
        /// List alle in der Datenbank Appointments heraus und fügt diese zu einer Liste hinzu
        /// </summary>
        /// <returns>Liste aller vorhandenen Appointments für Klasse</returns>
        public List<Appointment> ReadAppointments(Class c)
        {
            try
            {
                //instanzierung
                DatabaseConnector Connect = new DatabaseConnector();
                Connect.OpenConnection();

                MySqlCommand command = Connect.Connection.CreateCommand();
                // query liest nur bestimmte Klassen einer Person Aus!
                command.CommandText = "SELECT idAppointment, Title, Person_idPerson, Class_idClass, Room_idRoom, Start_Time, End_Time, Description, Alldayevent FROM Appointment WHERE Class_idClass=" + c.IdClass + ";";

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    //appointmentList.Add(new Appointment(reader.GetValue(1).ToString(),)
      
                }

                reader.Close();

                //Connection schliessen
                Connect.CloseConnection();

                return appointmentList;
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

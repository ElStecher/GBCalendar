using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace GBCalendar
{
    class DatabaseWriter
    {
        #region Methoden der Klasse DatabaseWriter

        /// <summary>
        /// Schreibt ein neu erstelltes Appointment in die Datenbank
        /// </summary>
        /// <param name="appointment">Appointment das in die Datenbank geschrieben werden soll</param>
        /// <returns></returns>
        public int WriteAppointment(Appointment appointment)
        {
            try
            {               
                int Id;

                // Instanzierung
                DatabaseConnector Connect = new DatabaseConnector();
                Connect.OpenConnection();

                MySqlCommand command = Connect.Connection.CreateCommand();

                // Schreibt das Appointment mit den Eigenschaften des übergebenen Appointments in die Datenbank
                command.CommandText = "INSERT INTO Appointment(Title, Class_idClass, Room_idRoom, Start_Time, End_Time, Description,  " +
                    "Alldayevent, Person_idPerson) " + "VALUES('" + appointment.Title + "', " + appointment.SchoolClass.IdClass + ", " + 
                    appointment.Room.IdRoom + ", '" + appointment.StartTime.ToString("yyyy-MM-dd HH:mm") + "', '" 
                    + appointment.EndTime.ToString("yyyy-MM-dd HH:mm") + "', '" + appointment.Description + "', '" + appointment.AllDayEvent + 
                    "', " + appointment.Creator.IdPerson + ")";
                command.ExecuteNonQuery();
                Id = (int)command.LastInsertedId;

                // Verbindung schliessen
                Connect.CloseConnection();

                return Id;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        /// <summary>
        /// Speichert die neuen Informationen des Appointments nachdem der Benutzer die Informationen geändert hat
        /// </summary>
        /// <param name="appointment">Appointment das der Benutzer geändert hat</param>
        public void UpdateAppointment(Appointment appointment)
        {
            try
            {
                // Instanzierung
                DatabaseConnector Connect = new DatabaseConnector();
                Connect.OpenConnection();

                MySqlCommand command = Connect.Connection.CreateCommand();

                // Updatet den Datensatz in der Datenbank
                command.CommandText = "UPDATE Appointment SET Title ='" + appointment.Title + "', Room_idRoom=" + appointment.Room.IdRoom + ", Start_Time='" + appointment.StartTime.ToString("yyyy-MM-dd HH:mm") + 
                    "', End_Time='" + appointment.EndTime.ToString("yyyy-MM-dd HH:mm") + "', Description= '" + appointment.Description + "', Alldayevent='" + appointment.AllDayEvent + 
                    "' WHERE idAppointment=" + appointment.IdAppointment + ";";
                command.ExecuteNonQuery();

                // Verbindung schliessen
                Connect.CloseConnection();

            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Löscht ein gewähltes Appointment aus der Datenbank
        /// </summary>
        /// <param name="appointment">Das zu löschende Appointment</param>
        public void DelteAppointment(Appointment appointment)
        {
            try
            {
                // Instanzierung
                DatabaseConnector Connect = new DatabaseConnector();
                Connect.OpenConnection();

                MySqlCommand command = Connect.Connection.CreateCommand();

                // Löscht den ausgewählten Datensatz aus der Datenbank
                command.CommandText = "DELETE FROM Appointment WHERE idAppointment=" + appointment.IdAppointment.ToString() + ";";
                command.ExecuteNonQuery();

                // Schliesst die Verbindung
                Connect.CloseConnection();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}

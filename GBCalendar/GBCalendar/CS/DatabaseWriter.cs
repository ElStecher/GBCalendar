using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace GBCalendar
{
    class DatabaseWriter
    {
        #region Felder und Eigenschaften der Klasse DatabaseWriter

        #endregion
        // @Fabio ToDo: Schauen wie Singleton-Pattern genau umgesetzt wird und implementieren
        #region Methoden der Klasse DatabaseWriter

        public int WriteAppointment(Appointment appointment)
        {
            try
            {
                //instanzierung
                int Id;
                DatabaseConnector Connect = new DatabaseConnector();
                Connect.OpenConnection();

                MySqlCommand command = Connect.Connection.CreateCommand();
                command.CommandText = "INSERT INTO Appointment(Title, Class_idClass, Room_idRoom, Start_Time, End_Time, Description,  Alldayevent, Person_idPerson) VALUES('" + appointment.Title + "', " + appointment.SchoolClass.IdClass + ", " + appointment.Room.IdRoom + ", '" + appointment.StartTime + "', '" + appointment.EndTime + "', '" + appointment.Description + "', '" + appointment.AllDayEvent + "', " + appointment.Creator.IdPerson + ")";
                command.ExecuteNonQuery();
                Id = (int)command.LastInsertedId;

                Connect.CloseConnection();
                return Id;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        // @Sam ToDo: Methode erstellen um Daten in der Datenbank upzudaten

        public void UpdateAppointment(Appointment appointment)
        {
            try
            {
                //instanzierung
                DatabaseConnector Connect = new DatabaseConnector();
                Connect.OpenConnection();

                MySqlCommand command = Connect.Connection.CreateCommand();
                command.CommandText = "UPDATE Appointment SET Title =" + appointment.Title + ", Class_idClass=" + appointment.SchoolClass.IdClass + ", Room_idRoom=" + appointment.Room.IdRoom + ", Start_Time=" + appointment.StartTime + ", End_Time=" + appointment.EndTime + ", " + appointment.Description + ", Alldayevent=" + appointment.AllDayEvent + ", Category=" + ") WHERE idAppointment=" + appointment.IdAppointment + ";";
                command.ExecuteNonQuery();
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

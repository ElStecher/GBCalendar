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

        public int WriteAppointment(Appointment A)
        {
            try
            {
                //instanzierung
                int Id;
                DatabaseConnector Connect = new DatabaseConnector();
                Connect.OpenConnection();

                MySqlCommand command = Connect.Connection.CreateCommand();
                command.CommandText = "INSERT INTO Appointment(Title, Class_idClass, Room_idRoom, Start_Time, End_Time, Description,  Alldayevent, Person_idPerson) VALUES('" + A.Title + "', " + A.SchoolClass.IdClass + ", " + A.Room.IdRoom + ", '" + A.StartTime + "', '" + A.EndTime + "', '" + A.Description + "', '" + A.AllDayEvent + "', " + A.Creator.IdPerson + ")";
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

        public void UpdateAppointment(Appointment A)
        {
            try
            {
                //instanzierung
                DatabaseConnector Connect = new DatabaseConnector();
                Connect.OpenConnection();

                MySqlCommand command = Connect.Connection.CreateCommand();
                command.CommandText = "UPDATE Appointment SET Title =" + A.Title + ", Class_idClass=" + A.SchoolClass.IdClass + ", Room_idRoom=" + A.Room.IdRoom + ", Start_Time=" + A.StartTime + ", End_Time=" + A.EndTime + ", " + A.Description + ", Alldayevent=" + A.AllDayEvent + ", Category=" + ") WHERE idAppointment=" + A.IdAppointment + ";";
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
